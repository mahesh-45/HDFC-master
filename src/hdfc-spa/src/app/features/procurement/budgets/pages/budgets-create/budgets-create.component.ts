import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ValidationErrors, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { BudgetsService } from '../../shared/budgets.service';
// import { Budget, TreeFlatNode, TreeNode, SubBudgets, BudgetSpendLimits } from '../../shared/budgets.model';
import { MatStepper } from '@angular/material/stepper';
import { Observable, of as observableOf } from 'rxjs';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { MatTableDataSource } from '@angular/material/table';
import { TreeFlatNode, TreeNode } from '../../shared/tree.model';
import { SubBudgets } from '../../shared/subbudgets.model';
import { BudgetSpendLimits } from '../../shared/budget-spend-limits.model';
import { Budget } from '../../shared/budgets.model';
import { BudgetHeadsService } from 'app/features/masters/budget-heads/shared/budget-heads.service';
import { BudgetHead } from 'app/features/masters/budget-heads/shared/budget-head';
import { Status } from 'shared/enums/status.enum';
import { CostCodesService } from 'app/features/masters/cost-codes/shared/cost-codes.service';

@Component({
  selector: 'app-budgets-create',
  templateUrl: './budgets-create.component.html',
  styles: []
})
export class BudgetsCreateComponent implements OnInit {
  displayedColumns: string[] = ['ID', 'StartDate', 'EndDate', 'TransactionDate', 'BudgetAmount', 'TransactionType', 'Actions'];
  displayedSpendLimitColumns: string[] = ['ID', 'StartDate', 'EndDate', 'SpendLimit', 'Remarks', 'Actions'];
  currencies = [{ id: 1, name: 'USD' }, { id: 2, name: 'INR' }];
  transactionTypes = ['Capex', 'Opex', 'Revex'];
  departments = [{ id: 1, name: 'Dept1' }, { id: 2, name: 'Dept2' }];
  costCodes = [{ id: 1, name: 'CostCode1' }, { id: 2, name: 'CostCode2' }];
  divisions = [{ id: 1, name: 'DIV1' }, { id: 2, name: 'Div2' }]
  subDivisionsList = [{ id: 1, divisionId: 1, name: 'SUBDIV1' }, { id: 2, divisionId: 2, name: 'SUBDIV2' }]
  budgetCategories = [{ id: 1, name: 'BudgetCategory1' }, { id: 2, name: 'BudgetCategory2' }]
  personInCharges = [{ id: 1, email: 'user1@cormsquare.com' }, { id: 2, email: 'user2@cormsquare.com' }]
  budgetForm: FormGroup;
  currentDate = new Date();
  maxDate: Date;
  minDate = new Date();
  subDivisions: any;
  years = ['NormalYear', 'FinancialYear']
  treeControl: FlatTreeControl<TreeFlatNode>;
  treeFlattener: MatTreeFlattener<TreeNode, TreeFlatNode>;
  dataSource: MatTreeFlatDataSource<TreeNode, TreeFlatNode>;
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('tree', { static: true }) tree;
  serializedDate = new FormControl((new Date()).toISOString());
  private _getLevel = (node: TreeFlatNode) => node.level;
  private _isExpandable = (node: TreeFlatNode) => node.expandable;
  private _getChildren = (node: TreeNode): Observable<TreeNode[]> =>
    observableOf(node.childBudgetHead);
  transformer = (node: TreeNode, level: number) => {
    return new TreeFlatNode(!!(node.childBudgetHead.length > 0), node.name, level, node.id);
  };
  hasChild = (_: number, _nodeData: TreeFlatNode) => _nodeData.expandable;
  budgetHeads: TreeNode[];
  subBudgetsArray: SubBudgets[] = [];
  updatebutton: boolean;
  currentIndex = 0;
  budgetSpendLimitsArray: BudgetSpendLimits[] = [];
  updateBudgetSpendLimitbutton: boolean;
  currentIndex2: any;
  formValue: Budget;
  budgetsById: Budget;
  budgetCostCodes: any[];
  financialYear: boolean;

  minDate1: Date;
  maxDate1: Date;
  minDate2: Date;
  maxDate2: Date;
  dataSourceTable = new MatTableDataSource();
  dataSourceSpendLimit = new MatTableDataSource();
  allBudgetHeads: BudgetHead[] = [];
  constructor(private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private uiService: UiService,
    private budgetsService: BudgetsService,
    private budgetHeadsService: BudgetHeadsService,
    private costCodesService: CostCodesService) { }

  ngOnInit() {
    this.buildBudgetForm();
    this.getCategoryTree();
    this.getBudgetHeads();
    this.budgetForm.controls['budgetType'].setValue('Capex');
    // this.getCostCodes();
  }
  buildBudgetForm() {
    this.budgetForm = this.formBuilder.group({
      id: [0],
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
    else if (event.value === 'Normal') {
      this.budgetForm.controls['startDate'].setValue('');
      this.budgetForm.controls['endDate'].setValue('');
    }
  }
  goBack(stepper: MatStepper) {
    stepper.previous();
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
  setBudgetType(type) {
    console.log(type);
    this.budgetForm.controls['budgetType'].setValue(type);
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

  editItem(event, index) {
    this.updatebutton = true;
    this.currentIndex = index;
    let x = this.budgetForm.controls['subBudgets'];
    x.patchValue(event);
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
  deleteSubBudgets(element, index) {
    this.subBudgetsArray.splice(index, 1);
    this.dataSourceTable.data = this.subBudgetsArray;
  }
  deleteBudgetSpendLimits(element, index) {
    this.budgetSpendLimitsArray.splice(index, 1);
    this.dataSourceSpendLimit.data = this.budgetSpendLimitsArray;
  }

  addSpendLimits() {
    const subBudgets = this.budgetForm.get('budgetSpendLimits').value as BudgetSpendLimits;
    this.budgetSpendLimitsArray.push(subBudgets);
    this.dataSourceSpendLimit.data = this.budgetSpendLimitsArray;
    this.clearBudgetSpendLimits();
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
  editSpendLimit(element, index) {
    this.updateBudgetSpendLimitbutton = true;
    this.currentIndex2 = index;
    let x = this.budgetForm.controls['budgetSpendLimits'];
    x.patchValue(element);
  }
  clearBudgetSpendLimits() {
    this.budgetForm.get('budgetSpendLimits').reset();
    this.updateBudgetSpendLimitbutton = false;
  }
  cancelSpendLimits() {
    this.clearBudgetSpendLimits();
  }

  submitForm() {
    console.log(this.budgetForm.getRawValue() as Budget);
    this.uiService.showConfirmationBox({
      message: 'Are you sure about creating this Budget?',
      onYes: () => this.createBudget()
    });
  }
  createBudget() {
    this.formValue = this.budgetForm.getRawValue() as Budget;
    console.log(this.formValue);
    this.formValue.subBudgets = this.subBudgetsArray;
    this.formValue.budgetSpendLimits = this.budgetSpendLimitsArray;
    this.formValue.budgetCostCodes = this.budgetCostCodes;
    console.log("this.formValue", this.formValue)
    this.budgetsService.createBudget(this.formValue).subscribe(res => {
      if (res) {
        this.uiService.showSuccess(`Budget: ${res.name} is created successfully.`)
        this.router.navigate([`procurement/budgets`])
      }
    })
  }
  goBackToPrevious(stepper) {
    stepper.selectedIndex = 1;
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

}
