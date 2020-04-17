import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';

import { Employe } from '../../shared/employe.model';
import { EmployeeService } from '../../shared/employee.service';

@Component({
  selector: "app-employee-detail",
  templateUrl: "./employee-detail.component.html",
  styles: [],
})
export class EmployeeDetailComponent implements OnInit {

  detailData: any = {};


  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private uiService: UiService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {

    this.getDetailedData();
  }

  getDetailedData() {
    const employeeUid = this.route.snapshot.paramMap.get("uid");
    if (employeeUid) {
      this.employeeService.getEmployee(employeeUid).subscribe((res) => {
        this.detailData = res;

      });
    }
  }
}
