import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Status } from 'shared/enums/status.enum';
import { Location } from '@angular/common';
import { BudgetHead } from '../../shared/budget-head';
import { BudgetHeadsService } from '../../shared/budget-heads.service';

@Component({
  selector: 'app-budget-heads-editor',
  templateUrl: './budget-heads-editor.component.html',
  styles: []
})
export class BudgetHeadsEditorComponent implements OnInit {
  budgetHeadForm: FormGroup;
  costCodes = [{ name: 'costCode1', value: 1 },
  { name: 'costCode2', value: 2 }, { name: 'costCode3', value: 3 }
  ];
  Departments = [{ name: 'Dept1', value: 1 }, { name: 'Dept2', value: 2 }, { name: 'Dept3', value: 3 }]
  parentId: number;
  parentUid: string;
  budgetHead: BudgetHead;
  id: number;
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private uiService: UiService,
    private budgetHeadsService: BudgetHeadsService,
    private location: Location
  ) { }

  ngOnInit() {
    const budgetHeadUid = this.activatedRoute.snapshot.paramMap.get('uid');
    console.log("budgetHeadUid", budgetHeadUid)
    this.parentUid = this.activatedRoute.snapshot.paramMap.get('uid');
    this.activatedRoute.params.subscribe(res => {
      console.log("res", res)
      this.id = res.id;
      this.parentId = res.parentId;
      console.log(" this.id ", this.id);
    })
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (budgetHeadUid) {
      this.budgetHeadsService.getBudgetHead(budgetHeadUid).subscribe(res => {
        console.log("edit", res)
        this.buildBudgetHeadForm(res);
      });
    }
    else {
      this.buildBudgetHeadForm(new BudgetHead())
    }
  }

  buildBudgetHeadForm(budgetHead: BudgetHead) {
    this.budgetHeadForm = this.formBuilder.group({
      uid: [budgetHead.uid],
      name: [budgetHead.name, Validators.required],
      code: [budgetHead.code, Validators.required],
      status: [budgetHead.status],
      costCodeId: [budgetHead.costCodeId],
      departmentId: [budgetHead.departmentId],
      parentId: [budgetHead.parentId],
    });
  }

  get f() {
    return this.budgetHeadForm.controls;
  }
  close() {
    this.location.back();
  }
  onSubmit(any) {
    console.log("submit", any)
    if (this.budgetHeadForm.valid) { // Is form valid?
      if (this.f.uid.value) { // Is the form in edit mode
        this.uiService.showConfirmationBox({
          message: 'Are you sure about updating this Budget Head?',
          onYes: () => this.updateBudgetHead()
        });
      } else { // If the form in create mode
        this.uiService.showConfirmationBox({
          message: 'Are you sure about creating this Budget-Head?',
          onYes: () => this.createBudgetHead()
        });
      }
    }
  }
  createBudgetHead(): void {
    const budgetHead = this.extractBudgetHead();
    if (this.parentId) {
      budgetHead.parentId = this.parentId;
    }
    console.log("budgetHead", budgetHead)
    this.budgetHeadsService.createBudgetHead(budgetHead).subscribe(res => {
      console.log("resss", res)
      debugger
      if (res.parentId > 0) {
        this.uiService.showSuccess(`BudgetHead: ${res.name} is created successfully.`);
        this.router.navigate([`/masters/budget-heads/sublist/parentId/` + res.parentId]);
      }
      else {
        this.uiService.showSuccess(`BudgetHead: ${res.name} is created successfully.`);
        this.router.navigate([`/masters/budget-heads`]);
      }
    });
  }
  extractBudgetHead(): BudgetHead {
    const budgetHead: BudgetHead = {
      id: this.budgetHeadForm.value.id,
      uid: this.budgetHeadForm.value.uid,
      name: this.budgetHeadForm.value.name,
      code: this.budgetHeadForm.value.code,
      departmentId: this.budgetHeadForm.value.departmentId,
      costCodeId: this.budgetHeadForm.value.costCodeId,
      parentId: this.budgetHeadForm.value.parentId,
      status:
        this.budgetHeadForm.value.status === Status.Active ? Status.Active : Status.InActive,
    };
    return budgetHead;
  }
  updateBudgetHead(): void {
    const budgetHead = this.extractBudgetHead();
    this.budgetHeadsService.updateBudgetHead(budgetHead).subscribe(res => {
      if (res.parentId > 0) {
        this.uiService.showSuccess(`BudgetHead: ${res.name} is updated successfully.`);
        this.router.navigate([`/masters/budget-heads/sublist/parentId/` + res.parentId]);
      }
      this.uiService.showSuccess(`BudgetHead: ${res.name} is updated successfully.`);
      this.router.navigate([`/masters/budget-heads`]);
    });
  }

}
