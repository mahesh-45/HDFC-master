import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BudgetsService } from '../../shared/budgets.service';
import { ActivatedRoute } from '@angular/router';
import { Budget } from '../../shared/budgets.model';
import { BudgetHeadsService } from 'app/features/masters/budget-heads/shared/budget-heads.service';

@Component({
  selector: 'app-budgets-details',
  templateUrl: './budgets-details.component.html',
  styles: []
})
export class BudgetsDetailsComponent implements OnInit {

  budget: any;
  budgetCategories = [{ id: 1, name: 'BudgetCategory1' }, { id: 2, name: 'BudgetCategory2' }]
  displayColumns = ['startDate', 'endDate', 'spendLimit', 'remarks']
  divisions = [{ id: 1, name: 'DIV1' }, { id: 2, name: 'Div2' }]
  subDivisionsList = [{ id: 1, divisionId: 1, name: 'SUBDIV1' }, { id: 2, divisionId: 2, name: 'SUBDIV2' }]
  personInCharges = [{ id: 1, email: 'user1@cormsquare.com' }, { id: 2, email: 'user2@cormsquare.com' }]
  currencies = [{ id: 1, name: 'USD' }, { id: 2, name: 'INR' }];
  costCodes = [{ id: 1, name: 'CostCode1' }, { id: 2, name: 'CostCode2' }];
  departments = [{ id: 1, name: 'Dept1' }, { id: 2, name: 'Dept2' }];
  dataSource = new MatTableDataSource();
  budgetCategoryName: any;
  divisionName: any;
  subDivisionName: any;
  personInChargeName: any;
  currency: string;
  budgetHeads: any[] = [];
  budgetHeadName: any;
  departmentName: string;
  titleName: string;
  referenceId: string;
  createdUserEmail: any;
  budgetuid: string;
  constructor(private budgetsService: BudgetsService,
    private activatedRoute: ActivatedRoute,
    private budgetHeadsService: BudgetHeadsService) {
    this.budgetuid = this.activatedRoute.snapshot.paramMap.get('uid');
  }

  ngOnInit() {
    this.getBudgetHeads();
    this.getBudget();
  }
  getBudget() {
    this.budgetsService.getBudgetByUid(this.budgetuid).subscribe(res => {
      this.budget = res;
      this.budgetCategoryName = this.budgetCategories.find(x => x.id == this.budget.budgetCategoryId).name;
      this.divisionName = this.divisions.find(x => x.id == this.budget.divisionId).name;
      this.subDivisionName = this.subDivisionsList.find(x => x.id == this.budget.subDivisionId).name;
      this.personInChargeName = this.personInCharges.find(x => x.id == this.budget.personInChargeId).email;
      this.currency = this.currencies.find(x => x.id === this.budget.currencyId).name;
      this.budgetHeadName = this.budgetHeads.find(x => x.id === this.budget.budgetHeadId).name;
      this.departmentName = this.departments.find(x => x.id === this.budget.departmentId).name;
      this.dataSource.data = res.budgetSpendLimits;
      if (this.budget.createdBy > 0) {
        this.getRequester(this.budget.createdBy)
      }
    });
  }

  getRequester(userId) {
    this.budgetsService.getUserById(userId).subscribe(res => {
      this.createdUserEmail = res;
    });
  }
  getBudgetHeads() {
    this.budgetHeadsService.getAllBudgetHeads().subscribe(res => {
      this.budgetHeads = res;
    })
  }

}
