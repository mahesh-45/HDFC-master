import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';

import { Employe } from '../../shared/employe.model';
import { EmployeeService } from '../../shared/employee.service';

@Component({
  selector: "app-employee-editor",
  templateUrl: "./employee-editor.component.html",
  styles: [],
})
export class EmployeeEditorComponent implements OnInit {
  employeForm: FormGroup;
  genderType: string[];

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private uiService: UiService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.buildValue();
    const employeeUid = this.route.snapshot.paramMap.get("uid");
    if (employeeUid) {
      this.employeeService.getEmployee(employeeUid).subscribe((res) => {
        this.buildCountryForm(res);
      });
    } else {
      this.buildCountryForm(new Employe());
    }
  }

  buildValue() {
    this.genderType = ["Male", "Female"];
  }

  submitForm() {
    if (this.employeForm.valid) {
      // Is form valid?
      if (this.f.uid.value) {
        // Is the form in edit mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about updating this Country?",
          onYes: () => this.updateCountry(),
        });
      }
    }
  }

  buildCountryForm(employee: Employe) {
    this.employeForm = this.formBuilder.group({
      uid: [employee.uid],
      firstName: [employee.firstName, Validators.required],
      middleName: [employee.middleName, Validators.required],
      lastName: [employee.lastName, Validators.required],
      title: [employee.title, Validators.required],
      employeeNumber: [employee.employeeNumber, Validators.required],
      gender: [employee.gender, Validators.required],
      panNumber: [employee.panNumber, Validators.required],
      contact1: [employee.contact1, Validators.required],
      contact2: [employee.contact2, Validators.required],
      band: [employee.band, Validators.required],
      locationName: [employee.locationName, Validators.required],
      supervisorEmpCode: [employee.supervisorEmpCode, Validators.required],
      hireDate: [employee.hireDate, Validators.required],
      officialEmail: [employee.officialEmail, Validators.required],
      recordStatus: [employee.recordStatus, Validators.required],
      dateOfBirth: [employee.dateOfBirth, Validators.required],
      countryId: [employee.countryId, Validators.required],
      region: [employee.region, Validators.required],
      maritalStatus: [employee.maritalStatus, Validators.required],
      city: [employee.city, Validators.required],
      actualTerminationDate: [employee.actualTerminationDate, Validators.required],
      employeeStatus: [employee.employeeStatus],
    });
  }



  updateCountry() {
    const employee = this.extractCountry();
    this.employeeService.updateEmployee(employee).subscribe((res) => {
      this.uiService.showSuccess(
        `employee: ${res.firstName} is updated successfully.`
      );
      this.router.navigate([`/masters/employees`]);
    });
  }
  extractCountry() {
    const employee: Employe = {
      uid: this.employeForm.value.uid,
      firstName: this.employeForm.value.firstName,
      middleName: this.employeForm.value.middleName,
      lastName: this.employeForm.value.lastName,
      title: this.employeForm.value.title,
      employeeNumber: this.employeForm.value.employeeNumber,
      gender: this.employeForm.value.gender,
      panNumber: this.employeForm.value.panNumber,
      dateOfBirth: this.employeForm.value.dateOfBirth,
      maritalStatus: this.employeForm.value.maritalStatus,
      officialEmail: this.employeForm.value.officialEmail,
      contact1: this.employeForm.value.contact1,
      contact2: this.employeForm.value.contact2,
      band: this.employeForm.value.band,
      locationName: this.employeForm.value.locationName,
      supervisorEmpCode: this.employeForm.value.supervisorEmpCode,
      hireDate: this.employeForm.value.hireDate,
      recordStatus: this.employeForm.value.recordStatus,
      countryId: this.employeForm.value.countryId,
      city: this.employeForm.value.city,
      region: this.employeForm.value.region,
      actualTerminationDate: this.employeForm.value.actualTerminationDate,
      employeeStatus: this.employeForm.value.employeeStatus,
    };
    return employee;
  }
  get f() {
    return this.employeForm.controls;
  }
}
