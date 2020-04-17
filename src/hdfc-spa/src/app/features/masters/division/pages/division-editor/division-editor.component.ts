import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Division } from '../../shared/division.model';
import { DivisionService } from '../../shared/division.service';
import { MatTableDataSource } from '@angular/material/table';
import { SubDivisions } from '../../shared/sub-division.model';
import { Status } from 'shared/enums/status.enum';

@Component({
  selector: 'app-division-editor',
  templateUrl: './division-editor.component.html',
  styles: []
})
export class DivisionEditorComponent implements OnInit {
  divisionForm: FormGroup;
  subDivisionArray: SubDivisions[] = [];
  updateSubDivisionButton: boolean;
  dataSourceSubDivision = new MatTableDataSource();
  displayedSubDivisions = ['ID', 'Name', 'Code', 'Status', 'Actions']
  currentIndex: any;
  divisionById: Division;
  divisionUid: string;
  constructor(private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private uiService: UiService,
    private divisionService: DivisionService) { }

  ngOnInit() {
    this.divisionUid = this.activatedRoute.snapshot.paramMap.get("uid");
    console.log("this.divisionUid", this.divisionUid)
    if (this.divisionUid) {
      this.buildDivisionForm();
      this.divisionService.getDivision(this.divisionUid).subscribe((res) => {
        this.divisionById = res;
        console.log("getbyid", res)
        this.divisionForm.patchValue(this.divisionById);
        this.subDivisionArray = this.divisionById.subDivisions;
        this.dataSourceSubDivision.data = this.subDivisionArray;
      });
    } else {
      this.buildDivisionForm();
    }
  }
  buildDivisionForm() {
    this.divisionForm = this.formBuilder.group({
      uid: [''],
      name: ['', Validators.required],
      code: ['', Validators.required],
      status: ['', Validators.required],
      subDivision: this.subDivision()
    });
  }
  subDivision() {
    return this.formBuilder.group({
      uid: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      status: ['', Validators.required],
    })
  }

  submitForm() {
    if (this.divisionUid) {
      this.uiService.showConfirmationBox({
        message: "Are you sure about updating this Division ?",
        onYes: () => this.updateDivision(),
      });
    } else {
      this.uiService.showConfirmationBox({
        message: "Are you sure about creating this Division ?",
        onYes: () => this.createDivision(),
      });
    }
  }
  createDivision() {
    const division = this.extractCostCodes();
    this.divisionService.createDivision(division).subscribe((res) => {
      this.uiService.showSuccess(
        `Division: ${res.name} is created successfully.`
      );
      this.router.navigate([`/masters/division`]);
    });
  }

  editSubDivision(event, index) {
    this.updateSubDivisionButton = true;
    this.currentIndex = index;
    let x = this.divisionForm.controls['subDivision'];
    x.patchValue(event);
  }

  deleteSubDivision(event, index) {
    if (event.id > 0) {
      this.divisionService.deleteSubDivision(this.divisionById.uid, event.id).subscribe(res => {
        if (res) {
          this.uiService.showSuccess('BudgetSpendLimit deleted');
        }
      })
    }
    this.subDivisionArray.splice(index, 1);
    this.dataSourceSubDivision.data = this.subDivisionArray;
  }
  extractCostCodes(): Division {
    console.log("formvalue", this.divisionForm.getRawValue())
    const division: Division = {
      uid: this.divisionForm.value.uid,
      name: this.divisionForm.value.name,
      code: this.divisionForm.value.code,
      status:
        this.divisionForm.value.status === Status.Active
          ? Status.Active
          : Status.InActive,
      subDivisions: this.subDivisionArray,
    };
    console.log("division", division)
    return division;
  }


  updateDivision() {
    const division = this.extractCostCodes();
    division.uid = this.divisionUid;
    console.log("update", division)
    this.divisionService.updateDivision(division).subscribe((res) => {
      this.uiService.showSuccess(
        `Division: ${res.name} is updated successfully.`
      );
      this.router.navigate([`/masters/division`]);
    });
  }
  addSubDivisions() {
    const subDivisions = this.divisionForm.get('subDivision').value as SubDivisions;
    this.subDivisionArray.push(subDivisions);
    this.dataSourceSubDivision.data = this.subDivisionArray;
    this.clearSubDivision();
  }
  updateSubDivisions() {
    let updatedSubDivision = this.divisionForm.get('subDivision').value as SubDivisions;
    this.subDivisionArray[this.currentIndex] = updatedSubDivision;
    this.subDivisionArray[this.currentIndex].name = updatedSubDivision.name;
    this.subDivisionArray[this.currentIndex].code = updatedSubDivision.code;
    this.subDivisionArray[this.currentIndex].status = updatedSubDivision.status;
    this.dataSourceSubDivision.data = this.subDivisionArray;
    this.clearSubDivision();
  }
  clearSubDivision() {
    this.divisionForm.get('subDivision').reset();
    this.updateSubDivisionButton = false;
  }
  subDivisionForm() {
    const subDivisionFormValue = this.divisionForm.get('subDivision').value as SubDivisions;
    if (subDivisionFormValue.name != '' &&
      subDivisionFormValue.code != '') {
      return true;
    }
    else {
      return false;
    }
  }
  createValidation() {
    const divisionFormValue = this.divisionForm.getRawValue();
    if (divisionFormValue.name != '' &&
      divisionFormValue.code != '' &&
      divisionFormValue.status != null && this.dataSourceSubDivision.data.length > 0) {
      return true;
    }
    else {
      return false;
    }
  }
}
