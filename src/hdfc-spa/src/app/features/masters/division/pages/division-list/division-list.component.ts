import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Division, DivisionList } from '../../shared/division.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { map, catchError, startWith, switchMap } from 'rxjs/operators';
import { DivisionService } from '../../shared/division.service';
import { merge, of as observableOf } from 'rxjs';
import { Router } from '@angular/router';
import { moveItemInArray, CdkDragDrop } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styles: []
})
export class DivisionListComponent implements AfterViewInit {
  displayedColumns: string[] = [
    "name",
    "code",
    "createdDate",
    "status",
    "star",
  ];
  data: Division[] = [];
  result: Division[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  divisionList: DivisionList;

  search: string;
  constructor(private divisionService: DivisionService, private router: Router, ) { }

  ngAfterViewInit() {
    this.divisionList = new DivisionList();
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          this.divisionList.order = this.sort.direction;
          this.divisionList.sort = this.sort.active;
          this.divisionList.page = this.paginator.pageIndex;
          this.divisionList.pageSize = this.paginator.pageSize;
          this.divisionList.search = this.search ? this.search : null;
          return this.divisionService.getDivisionList(this.divisionList);

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
        console.log("data", data)
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
    this.router.navigate([`/masters/division/edit/` + uid]);
  }
}
