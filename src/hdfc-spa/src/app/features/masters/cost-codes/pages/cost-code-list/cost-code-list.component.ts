import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

import { CostCodes } from '../../shared/cost-codes.model';
import { CostCodesService } from '../../shared/cost-codes.service';

@Component({
  selector: "app-cost-code-list",
  templateUrl: "./cost-code-list.component.html",
  styles: [],
})
export class CostCodeListComponent implements AfterViewInit {
  displayedColumns: string[] = ["code", "name", "head", "action"];

  data: CostCodes[] = [];
  alldata: any;
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  search: string;
  constructor(
    private router: Router,
    private costCodeService: CostCodesService
  ) {}

  ngAfterViewInit() {
    /*  merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.costCodeService.getCountriesList(
            this.search ? this.search : null
          );
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
      });*/

    if (this.search) {
      this.costCodeService.getCountriesList(this.search).subscribe((data) => {
        this.alldata = data.items;
        console.log("search data", data);
      });
    }
  }

  ngOnInit(): void {
    this.getAllData();
  }
  getAllData() {
    this.costCodeService.getCostCodeAll().subscribe((res) => {
      this.alldata = res;
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(
      this.displayedColumns,
      event.previousIndex,
      event.currentIndex
    );
  }

  goToEditPage(uid: string) {
    this.router.navigate([`/masters/cost-codes/edit/` + uid]);
  }
  goToDetailPage(uid: string) {
    this.router.navigate([`/masters/cost-codes/detail/` + uid]);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
    this.ngAfterViewInit();
  }
}
