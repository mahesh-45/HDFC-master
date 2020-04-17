// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-employee-create',
//   templateUrl: './employee-create.component.html',
//   styles: []
// })
// export class EmployeeCreateComponent implements OnInit {

//   constructor() { }ng s


//   ngOnInit(): void {
//   }

// }
import { Component, OnInit } from "@angular/core";
import { Employe } from "../../shared/employe.model";
import { FormBuilder, FormGroup } from "@angular/forms";
import { EmployeeService } from '../../shared/employee.service';
import { Router } from '@angular/router';
import { UiService } from 'core/services';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styles: []
})
export class EmployeeCreateComponent implements OnInit {
  employeForm: FormGroup;
  genderType: any[] = [];

  constructor(private fb: FormBuilder, private employeeService: EmployeeService,
    private router: Router,
    private uiService: UiService) { }

  ngOnInit(): void {
    this.buildValue();
    this.buildEmployeForm(new Employe());

  }

  buildEmployeForm(employe: Employe) {
    this.employeForm = this.fb.group({
      firstName: [employe.firstName],
      middleName: [employe.middleName],
      lastName: [employe.lastName],
      title: [employe.title],
      employeeNumber: [employe.employeeNumber],
      gender: [employe.gender],
      panNumber: [employe.panNumber],
      dateOfBirth: [employe.dateOfBirth],
      maritalStatus: [employe.maritalStatus],
      officialEmail: [employe.officialEmail],
      contact1: [employe.contact1],
      contact2: [employe.contact2],
      band: [employe.band],
      locationName: [employe.locationName],
      supervisorEmpCode: [employe.supervisorEmpCode],
      hireDate: [employe.hireDate],
      recordStatus: [employe.recordStatus],
      countryId: [employe.countryId],
      city: [employe.city],
      region: [employe.region],
      actualTerminationDate: [employe.actualTerminationDate],
      employeeStatus: [employe.employeeStatus],
    });
  }
  1
  buildValue() {
    this.genderType = [
      'Male', 'Female'
    ];
  }
  get controls() {
    return this.employeForm.controls;
  }

  submitForm(employe: Employe) {
    this.uiService.showConfirmationBox({
      message: "Are you sure about creating this Employee?",
      onYes: () => this.createCountry(),
    });

  }
  createCountry() {
    const employee = this.extractCountry();
    this.employeeService.createEmployee(employee).subscribe((res) => {
      this.uiService.showSuccess(
        `Employee: ${res.firstName} is created successfully.`
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
}

