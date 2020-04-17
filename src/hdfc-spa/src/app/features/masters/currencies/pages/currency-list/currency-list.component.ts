import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

import { Currency } from '../../shared/currency.model';
import { CurrencyService } from '../../shared/currency.service';

@Component({
  selector: "app-currency-list",
  templateUrl: "./currency-list.component.html",
  styles: [],
})
export class CurrencyListComponent implements OnInit {
  displayedColumns: string[] = ["name", "code", "description", "action"];

  data: Currency[] = [];
  alldata: any;
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  search: string;
  constructor(
    private router: Router,
    private currencyService: CurrencyService
  ) {}

  ngOnInit(): void {
    this.getAllData();
  }
  getAllData() {
    this.currencyService.getCurrencyAll().subscribe((res) => {
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
    this.router.navigate([`/masters/currencies/edit/` + uid]);
  }
  goToDetailsPage(uid: string) {
    this.router.navigate([`/masters/currencies/details/` + uid]);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
  }
}
