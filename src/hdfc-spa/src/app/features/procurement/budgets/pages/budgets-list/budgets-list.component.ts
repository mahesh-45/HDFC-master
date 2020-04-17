import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetsService } from '../../shared/budgets.service';
import { Budget } from '../../shared/budgets.model';
import { merge, of as observableOf } from 'rxjs';
import { startWith, switchMap, map, catchError } from 'rxjs/operators';
import { UiService } from 'core/services';

@Component({
  selector: 'app-budgets-list',
  templateUrl: './budgets-list.component.html',
  styles: []
})
export class BudgetsListComponent implements AfterViewInit {
  budgets: any[] = [];
  list: Budget[] = [];
  resultsLength = 0;
  search: string;
  isLoadingResults = true;
  isRateLimitReached = false;
  data: any[] = [];
  currencies = [{ id: 1, name: 'USD' }, { id: 2, name: 'INR' }];
  usrdata: any;
  constructor(
    private router: Router,
    private budgetsService: BudgetsService, private uiService: UiService
  ) { }
  ngAfterViewInit() {
    merge()
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.budgetsService.getList(
            this.search ? this.search : null);
        }),
        map(data => {
          this.isLoadingResults = false;
          this.isRateLimitReached = false;
          this.resultsLength = data.total_count;

          return data.items;
        }),
        catchError(() => {
          this.isLoadingResults = false;
          this.isRateLimitReached = true;
          return observableOf([]);
        })
      ).subscribe(data => {
        this.data = data;
        console.log("this.dara", this.data)
        this.data.forEach(element => {
          this.getUser(element);
          element.currency = this.currencies.find(c => c.id === element.currencyId).name;
          // element.createdBy = this.usrdata;
        });
      });
  }

  getUser(user: any): any {
    this.budgetsService.getUserById(user.createdBy).subscribe(c => {
      user.createdByUser = c;
    })
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
    this.ngAfterViewInit();
  }
  goToCreate() {
    this.router.navigate(['/procurement/budgets/create/'])
  }
  goToEdit(uid: string) {
    this.router.navigate([`/procurement/budgets/edit/` + uid])
  }
  goToDetail(uid: string) {
    this.router.navigate([`/procurement/budgets/detail/` + uid])

  }
  openholdBudgetConfirmationBox(budgetId) {
    console.log(budgetId);
    this.uiService.showConfirmationBox({
      message: 'Are you sure to hold Budget?',
      onYes: () => this.holdBudget(budgetId)
    });
  }
  holdBudget(budgetId): void {
    this.budgetsService.holdBudget(budgetId).subscribe(c => {
      console.log(c);
      if (c) {
        this.uiService.showSuccess(c.referenceId + ' is on hold');
        setTimeout(() => {
          this.ngAfterViewInit();
        }, 1000);
      }
    });
  }

}
