import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Status } from 'shared/enums/status.enum';

import { CostCodes } from '../../shared/cost-codes.model';
import { CostCodesService } from '../../shared/cost-codes.service';

@Component({
  selector: "app-cost-code-editor",
  templateUrl: "./cost-code-editor.component.html",
  styles: [],
})
export class CostCodeEditorComponent implements OnInit {
  costCodeForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private costCodesService: CostCodesService,
    private route: ActivatedRoute,
    private router: Router,
    private uiService: UiService
  ) {}

  ngOnInit() {
    const costCodeUid = this.route.snapshot.paramMap.get("uid");
    if (costCodeUid) {
      this.costCodesService.getCostCode(costCodeUid).subscribe((res) => {
        this.buildCostCodeForm(res);
        console.log("editDetails", res);
      });
    } else {
      this.buildCostCodeForm(new CostCodes());
    }
  }
  buildCostCodeForm(costCodes: CostCodes) {
    this.costCodeForm = this.fb.group({
      uid: [costCodes.uid],
      Code: [costCodes.code, Validators.required],
      Name: [costCodes.name, Validators.required],
      Business_Head_Employee_Code: [costCodes.bhEmpCode, Validators.required],
      Business_Head: [costCodes.bh, Validators.required],
      Accountant_Dept_Employee_Code: [costCodes.adEmpCode, Validators.required],
      Accountant_Dept: [costCodes.adGroup, Validators.required],
      Head: [costCodes.head, Validators.required],
      status: [costCodes.status],
    });
  }
  submitForm() {
    if (this.costCodeForm.valid) {
      // Is form valid?
      if (this.c.uid.value) {
        // Is the form in edit mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about updating this costCode ?",
          onYes: () => this.updateCostCode(),
        });
      } else {
        // If the form in create mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about creating this costCode ?",
          onYes: () => this.createCostCode(),
        });
      }
    }
  }
  createCostCode() {
    const costCodes = this.extractCostCodes();
    this.costCodesService.createCostCode(costCodes).subscribe((res) => {
      this.uiService.showSuccess(
        `Costcode: ${res.name} is created successfully.`
      );
      this.router.navigate([`/masters/cost-codes`]);
    });
  }

  updateCostCode() {
    const country = this.extractCostCodes();
    this.costCodesService.updateCostCode(country).subscribe((res) => {
      this.uiService.showSuccess(
        `Costcode: ${res.name} is updated successfully.`
      );
      this.router.navigate([`/masters/cost-codes`]);
    });
  }

  get c() {
    return this.costCodeForm.controls;
  }

  extractCostCodes(): CostCodes {
    const costCodes: CostCodes = {
      uid: this.costCodeForm.value.uid,
      name: this.costCodeForm.value.Name,
      code: this.costCodeForm.value.Code,
      bhEmpCode: this.costCodeForm.value.Business_Head_Employee_Code,
      bh: this.costCodeForm.value.Business_Head,
      adGroup: this.costCodeForm.value.Accountant_Dept,
      adEmpCode: this.costCodeForm.value.Accountant_Dept_Employee_Code,
      head: this.costCodeForm.value.Head,
      status:
        this.costCodeForm.value.status === Status.Active
          ? Status.Active
          : Status.InActive,
    };
    return costCodes;
  }
}
