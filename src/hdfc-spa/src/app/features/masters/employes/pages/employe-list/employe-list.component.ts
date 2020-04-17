import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { EmployeeService } from '../../shared/employee.service';
import { Employe, EmployeeList } from '../../shared/employe.model';

@Component({
  selector: "app-employe-list",
  templateUrl: "./employe-list.component.html",
  styles: [],
})
export class EmployeListComponent implements OnInit {
  displayedColumns: string[] = [
    "firstName",
    "employeeNumber",
    "officialEmail",
    "status",
    "star",
  ];
  search: string;
  data: any;
  isLoadingResults: true;
  isRateLimitReached: false;
  constructor(
    private router: Router,
    private employeeService: EmployeeService
  ) { }

  ngOnInit(): void {
    this.getAllData();
  }
  getAllData() {
    this.employeeService.getAllEmployees().subscribe((res) => {
      this.data = res;
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.search = filterValue.trim().toLowerCase();
    this.ngOnInit();
    this.getFilteredData();
  }
  getFilteredData() {
    if (this.search) {
      this.employeeService.getEmployeesList(this.search).subscribe((data) => {
        this.data = data.items;

      });
    }
  }
  goToEditPage(uid: string) {
    this.router.navigate([`/masters/employees/edit/` + uid]);
  }
  goTodetailPage(uid: string) {
    this.router.navigate([`/masters/employees/detail/` + uid]);
  }
}
