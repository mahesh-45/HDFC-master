import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { Location } from '@angular/common';
import { BudgetHead, BudgetHeadList } from '../../shared/budget-head';
import { BudgetHeadsService } from '../../shared/budget-heads.service';

@Component({
  selector: 'app-budget-heads-list',
  templateUrl: './budget-heads-list.component.html',
  styles: []
})
export class BudgetHeadsListComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  data: BudgetHead[] = [];
  result: BudgetHead[] = [];
  budgetHeadList: BudgetHeadList;
  displayedColumns: string[] = ['name', 'code', 'createdDate', 'status', 'star'];
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  search: string;
  title: string;
  add = true;
  heading = 'Budget-Heads';
  parentId: number;
  uid: string;
  back = false;
  a: number;
  b: number;
  parentDetails: any;
  show = false;
  dataSource = new MatTableDataSource();

  constructor(
    private budgetHeadsService: BudgetHeadsService,
    private router: Router,
    private activeRoute: ActivatedRoute,
    public uiService: UiService,
    private location: Location
  ) {
    this.activeRoute.params.subscribe(res => {
      this.parentId = res.parentId;
      console.log("parentId", this.parentId);
    })
  }
  createBudgetHead() {
    const parentId = this.activeRoute.snapshot.params['parentId'];
    if (parentId === undefined) {
      this.router.navigate([
        '/masters/budget-heads/create', { id: 0 }
      ]);
    }
    else {
      this.router.navigate(['masters/budget-heads/create', { parentId: Number(parentId), id: 0 }])
    }
  }
  ngAfterViewInit() {
    this.budgetHeadList = new BudgetHeadList();
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          this.budgetHeadList.order = this.sort.direction;
          this.budgetHeadList.sort = this.sort.active;
          this.budgetHeadList.page = this.paginator.pageIndex;
          this.budgetHeadList.pageSize = this.paginator.pageSize;
          this.budgetHeadList.search = this.search ? this.search : null;
          return this.budgetHeadsService.getBudgetHeadsList(this.budgetHeadList);

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
        this.result = data;
        // console.log("result", this.result)
        // console.log("this.parentId", this.parentId)
        if (this.parentId != undefined || this.parentId != null) {
          this.data = this.result.filter(x => x.parentId === Number(this.parentId))
          // console.log("filtered", this.data)
        } else {
          this.data = this.result.filter(x => x.parentId === 0);
        }
        this.title = 'Budget Heads'
        console.log("list", this.data)
      });
  }
  CreateBudgetHead() {
    this.router.navigate([
      '/masters/budget-heads/create/',
    ]);
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
    this.ngAfterViewInit();
  }
  EditBudgetHead(uid: string) {
    this.router.navigate(['/masters/budget-heads/edit/' + uid])
  }
  CreateSubBudgetHead(parentId: number) {
    this.router.navigate([`/masters/budget-heads/edit`, { parentId: parentId, id: 0 }])
  }
  ViewSubBudgetHead(budgetHeadId: number) {
    console.log("budgetHeadId", budgetHeadId)
    this.title = 'Sub Budget Heads'
    this.data = this.result.filter(x => x.parentId === budgetHeadId)
    this.add = false;
  }
  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.displayedColumns, event.previousIndex, event.currentIndex);
  }
  GoBack() {
    this.title = 'Budget Heads'
    this.data = this.result.filter(x => x.parentId === 0);
    this.add = true;
  }

  backClick() {
    this.location.back();
    this.ngAfterViewInit();
  }


  getBudgetHeads() {
    if (this.parentId > 0) {
      this.budgetHeadsService.getBudgetHeadById(this.parentId).subscribe(res => {
        this.heading = 'Sub Budget Heads' + res.name;
        this.parentDetails = res;
        console.log('res', res);
      });
    }
    if (this.parentId === undefined) {
      this.parentId = 0;
      this.heading = 'Item Categories';
    }
    this.budgetHeadsService.list(this.parentId).subscribe(
      res => {
        console.log(res);
        if (res != null) {
          this.show = true;
          this.back = false;
          this.data = res;
        } else {
          this.show = false;
          this.back = true;
          this.dataSource = new MatTableDataSource();
        }
      },
      error => {
        this.show = false;
        this.uiService.showError(error);
      }
    );
  }
  redirectToSubBudgetHead(parentId: number) {
    console.log('redirectToSubBudgetHead clicked', parentId);
    this.router.navigate(['masters/budget-heads/sublist/parentId/' + parentId]);
    this.ngAfterViewInit();
  }
}
