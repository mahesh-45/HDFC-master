import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

import { BudgetCategories, BudgetCategoriesList } from '../../shared/budget-categories';
import { BudgetCategoriesService } from '../../shared/budget-categories.service';

@Component({
  selector: 'app-budget-categories-list',
  templateUrl: './budget-categories-list.component.html',
  styles: []
})
export class BudgetCategoriesListComponent implements AfterViewInit {
  displayedColumns: string[] = [
    "name",
    "code",
    "createdDate",
    "status",
    "star",
  ];
  data: BudgetCategories[] = [];
  result: BudgetCategories[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  budgetCategoriesList: BudgetCategoriesList;

  search: string;
  constructor(private router: Router, private budgetCategoryService: BudgetCategoriesService, ) { }



  ngAfterViewInit() {
    this.budgetCategoriesList = new BudgetCategoriesList();
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          this.budgetCategoriesList.order = this.sort.direction;
          this.budgetCategoriesList.sort = this.sort.active;
          this.budgetCategoriesList.page = this.paginator.pageIndex;
          this.budgetCategoriesList.pageSize = this.paginator.pageSize;
          this.budgetCategoriesList.search = this.search ? this.search : null;
          return this.budgetCategoryService.getBudgetCategoriesList(this.budgetCategoriesList);

        }),

        map((data) => {
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
      )
      .subscribe((data) => {
        this.data = data;
      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
    this.ngAfterViewInit();
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(
      this.displayedColumns,
      event.previousIndex,
      event.currentIndex
    );
  }

  goToEditPage(uid: string) {
    this.router.navigate([`/masters/budget-categories/edit/` + uid]);
  }
}



