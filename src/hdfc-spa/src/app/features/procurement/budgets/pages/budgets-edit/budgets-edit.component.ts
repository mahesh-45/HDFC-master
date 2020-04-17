import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { BudgetsService } from '../../shared/budgets.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { BudgetSpendLimits } from '../../shared/budget-spend-limits.model';
import { SubBudgets } from '../../shared/subbudgets.model';
import { MatStepper } from '@angular/material/stepper';
import { BudgetHead } from 'app/features/masters/budget-heads/shared/budget-head';
import { TreeFlatNode, TreeNode } from '../../shared/tree.model';
import { Observable, of as observableOf } from 'rxjs';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Budget } from '../../shared/budgets.model';
import { BudgetHeadsService } from 'app/features/masters/budget-heads/shared/budget-heads.service';

@Component({
  selector: 'app-budgets-edit',
  templateUrl: './budgets-edit.component.html',
  styles: []
})
export class BudgetsEditComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  displayedColumns: string[] = ['ID', 'StartDate', 'EndDate', 'TransactionDate', 'BudgetAmount', 'TransactionType', 'Actions'];
  displayedSpendLimitColumns: string[] = ['ID', 'StartDate', 'EndDate', 'SpendLimit', 'Remarks', 'Actions'];
  currencies = [{ id: 1, name: 'USD' }, { id: 2, name: 'INR' }];
  transactionTypes = ['Capex', 'Opex', 'Revex'];
  departments = [{ id: 1, name: 'Dept1' }, { id: 2, name: 'Dept2' }];
  personInCharges = [{ id: 1, email: 'user1@cormsquare.com' }, { id: 2, email: 'user2@cormsquare.com' }]
  subDivisionsList = [{ id: 1, divisionId: 1, name: 'SUBDIV1' }, { id: 2, divisionId: 2, name: 'SUBDIV2' }]
  updateBudgetSpendLimitbutton: boolean;
  costCodes = [{ id: 1, name: 'CostCode1' }, { id: 2, name: 'CostCode2' }];
  divisions = [{ id: 1, name: 'DIV1' }, { id: 2, name: 'Div2' }]
  currentIndex2: any;
  subDivisions: any;
  budgetForm: FormGroup;
  treeControl: FlatTreeControl<TreeFlatNode>;
  treeFlattener: MatTreeFlattener<TreeNode, TreeFlatNode>;
  dataSource: MatTreeFlatDataSource<TreeNode, TreeFlatNode>;
  dataSourceTable = new MatTableDataSource();
  dataSourceSpendLimit = new MatTableDataSource();
  updatebutton: boolean;
  budgetHeads: TreeNode[];
  budgetSpendLimitsArray: BudgetSpendLimits[] = [];
  allBudgetHeads: BudgetHead[] = [];
  budgetsById: Budget;
  formValue: Budget;
  budgetCostCodes: any[];
  budgetCategories = [{ id: 1, name: 'BudgetCategory1' }, { id: 2, name: 'BudgetCategory2' }]
  goBack(stepper: MatStepper) {
    stepper.previous();
  }
  subBudgetsArray: SubBudgets[] = [];
  @ViewChild('tree', { static: true }) tree;
  private _getLevel = (node: TreeFlatNode) => node.level;
  private _isExpandable = (node: TreeFlatNode) => node.expandable;
  private _getChildren = (node: TreeNode): Observable<TreeNode[]> =>
    observableOf(node.childBudgetHead);
  transformer = (node: TreeNode, level: number) => {
    return new TreeFlatNode(!!(node.childBudgetHead.length > 0), node.name, level, node.id);
  };
  hasChild = (_: number, _nodeData: TreeFlatNode) => _nodeData.expandable;
  currentIndex: any;
  currentDate = new Date();
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private uiService: UiService,
    private budgetsService: BudgetsService,
    private formBuilder: FormBuilder,
    private budgetHeadsService: BudgetHeadsService) { }

  ngOnInit() {
    const budgetuid = this.activatedRoute.snapshot.paramMap.get('uid');
    this.getBudgetHeads();
    this.getBudgetByUid(budgetuid);
    this.getCategoryTree();
    this.buildBudgetForm();
  }
  ngAfterViewInit() {
    this.stepper.selectedIndex = 1;
  }
  getBudgetByUid(uid: string) {
    this.budgetsService.getBudgetByUid(uid).subscribe(res => {
      this.budgetsById = res;
      this.subDivisions = this.subDivisionsList.filter(x => x.id === res.subDivisionId);
      //  this.bindCostCodes(res)
      if (this.budgetsById.budgetHeadId != 0) {
        const budgetHeadName = this.allBudgetHeads.find(x => x.id === this.budgetsById.budgetHeadId).name;
        this.budgetForm.controls['budgetHeadName'].setValue(budgetHeadName);
      }
      this.budgetForm.patchValue(this.budgetsById);
      this.subBudgetsArray = this.budgetsById.subBudgets;
      this.dataSourceTable.data = this.subBudgetsArray;
      this.budgetSpendLimitsArray = this.budgetsById.budgetSpendLimits;
      this.budgetCostCodes = this.budgetsById.budgetCostCodes;
      this.dataSourceSpendLimit.data = this.budgetsById.budgetSpendLimits;
    });
  }
  getBudgetHeads() {
    this.budgetHeadsService.getAllBudgetHeads().subscribe(res => {
      this.allBudgetHeads = res;
    })
  }

  onChangingDivision(division) {
    console.log("event", division)
    this.subDivisions = [];
    console.log("division", division)
    this.subDivisions = this.subDivisionsList.filter(x => x.divisionId == division)
  }
  changeYear(event) {
    if (event.value === 'Financial') {
      this.budgetForm.controls['startDate'].setValue(new Date(2020, 3, 1));
      this.budgetForm.controls['endDate'].setValue(new Date(2021, 2, 31));
    }
    else {
      this.budgetForm.controls['startDate'].setValue('');
      this.budgetForm.controls['endDate'].setValue('');
    }
  }
  setBudgetType(type) {
    console.log(type);
    this.budgetForm.controls['budgetType'].setValue(type);
  }
  totalAmount() {
    var first_number = this.budgetForm.controls['basicAmount'].value;
    var second_number = this.budgetForm.controls['taxAmount'].value;
    if (first_number) {
      var result = Number(first_number) + Number(second_number);
      this.budgetForm.controls['totalAmount'].setValue(result);
    }
    else {
      this.budgetForm.controls['totalAmount'].setValue(0.00);
    }
  }
  selectedCostCode(a, b) {
    console.log("a", a, "b", b)
    return a.id == b.costCodeId;
  }
  selectionCostCode(types) {
    this.budgetCostCodes = [];
    types.forEach(element => {
      this.budgetCostCodes.push({
        costCodeId: element.id,
        id: 0,
        budgetId: 0,
      });
    });
  }
  getCategoryTree() {
    this.budgetsService.getAllBudgetHeads().subscribe(
      (res: TreeNode[]) => {
        if (res) {
          this.budgetHeads = res;
          this.dataSource.data = this.budgetHeads;
        }
      },
      error => {
        this.uiService.showError(error);
      }
    );
    this.treeFlattener = new MatTreeFlattener(
      this.transformer,
      this._getLevel,
      this._isExpandable,
      this._getChildren
    );
    this.treeControl = new FlatTreeControl<TreeFlatNode>(
      this._getLevel,
      this._isExpandable
    );
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  }
  setCategory(budget: any) {
    this.budgetForm.controls['budgetHeadName'].setValue(budget.name);
    const budgetHeadId = this.allBudgetHeads.find(x => x.name === budget.name).id;
    this.budgetForm.controls['budgetHeadId'].setValue(budgetHeadId);
  }
  editSpendLimit(element, index) {
    this.updateBudgetSpendLimitbutton = true;
    this.currentIndex2 = index;
    let x = this.budgetForm.controls['budgetSpendLimits'];
    x.patchValue(element);
  }

  deleteBudgetSpendLimits(element, index) {
    if (element.id > 0) {
      this.budgetsService.deleteBudgetSpendLimits(this.budgetsById.uid, element.id).subscribe(res => {
        if (res) {
          this.uiService.showSuccess('BudgetSpendLimit deleted');
        }
      })
    }
    this.budgetSpendLimitsArray.splice(index, 1);
    this.dataSourceSpendLimit.data = this.budgetSpendLimitsArray;
  }
  editItem(event, index) {
    this.updatebutton = true;
    this.currentIndex = index;
    let x = this.budgetForm.controls['subBudgets'];
    x.patchValue(event);
  }
  buildBudgetForm() {
    this.budgetForm = this.formBuilder.group({
      id: [0],
      uid: [''],
      name: ['', Validators.required],
      description: ['', Validators.required],
      budgetStatus: [''],
      budgetHeadId: ['', Validators.required],
      budgetCategoryId: ['', Validators.required],
      departmentId: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      transactionDate: [{ value: new Date(), disabled: true }, Validators.required],
      basicAmount: ['', Validators.required],
      taxAmount: [''],
      totalAmount: [{ value: 0.00, disabled: true }],
      currencyId: ['', Validators.required],
      budgetType: ['', Validators.required],
      divisionId: ['', Validators.required],
      subDivisionId: [0],
      personInChargeId: [0],
      justification: [''],
      budgetHeadName: [''],
      budgetCostCodes: ['', Validators.required],
      budgetSpendLimits: this.budgetSpendLimits()
    });
  }

  budgetSpendLimits() {
    return this.formBuilder.group({
      id: [0],
      startDate: [''],
      endDate: [''],
      spendLimit: [''],
      budgetId: [0],
      remarks: [''],
    })
  }

  updateValidation() {
    const budgetForm = this.budgetForm.getRawValue();
    if (budgetForm.name != '' &&
      budgetForm.code != '' &&
      Number(budgetForm.currencyId) != 0) {
      return true;
    }
    else {
      return false;
    }
  }

  cancelSpendLimits() {
    this.clearBudgetSpendLimits();
  }

  addSpendLimits() {
    debugger
    const subBudgets = this.budgetForm.get('budgetSpendLimits').value as BudgetSpendLimits;
    this.budgetSpendLimitsArray.push(subBudgets);
    this.dataSourceSpendLimit.data = this.budgetSpendLimitsArray;
    this.clearBudgetSpendLimits();
  }
  clearBudgetSpendLimits() {
    this.budgetForm.get('budgetSpendLimits').reset();
    this.updateBudgetSpendLimitbutton = false;
  }
  updateSpendLimits() {
    let updatedSpendLimits = this.budgetForm.get('budgetSpendLimits').value as BudgetSpendLimits;
    this.budgetSpendLimitsArray[this.currentIndex2] = updatedSpendLimits;
    this.budgetSpendLimitsArray[this.currentIndex2].startDate = updatedSpendLimits.startDate;
    this.budgetSpendLimitsArray[this.currentIndex2].endDate = updatedSpendLimits.endDate;
    this.budgetSpendLimitsArray[this.currentIndex2].spendLimit = updatedSpendLimits.spendLimit;
    this.dataSourceSpendLimit.data = this.budgetSpendLimitsArray;
    this.clearBudgetSpendLimits();
  }

  budgetSpendLimitValidation() {
    const values = this.budgetForm.get('budgetSpendLimits').value;
    if (
      values.startDate !== '' && values.endDate !== '' && Number(values.spendLimit) !== 0 && values.remarks != ''
    ) {
      return true;
    } else {
      return false;
    }
  }

  submit() {
    this.uiService.showConfirmationBox({
      message: 'Are you sure about updating this Budget?',
      onYes: () => this.updateBudget()
    });
  }
  updateBudget() {
    this.formValue = this.budgetForm.getRawValue() as Budget;
    this.formValue.budgetCostCodes = this.budgetCostCodes;
    this.formValue.subBudgets = this.subBudgetsArray;
    this.formValue.budgetSpendLimits = this.budgetSpendLimitsArray;
    console.log("onUpdate", this.formValue);
    this.budgetsService.updateBudget(this.formValue).subscribe(res => {
      if (res) {
        this.uiService.showSuccess(`Budget: ${res.name} is updated successfully.`)
        this.router.navigate([`procurement/budgets`])
      }
    })
  }
  goBackToPrevious(stepper) {
    stepper.selectedIndex = 1;
  }
}
