import { Status } from 'shared/enums/status.enum';
import { SubBudgets } from './subbudgets.model';
import { BudgetSpendLimits } from './budget-spend-limits.model';
import { BudgetCostCodes } from './budget-costCode.model';

export class Budget {
  id = 0;
  uid = '';
  referenceId = '';
  name = '';
  description = '';
  budgetStatus = Status.Active;
  budgetHeadId = 0;
  budgetCategoryId = 0;
  departmentId?= 0;
  startDate: Date;
  endDate: Date;
  transactionDate: Date;
  basicAmount = 0.00;
  taxAmount = 0.00;
  totalAmount: 0.00;
  currencyId = 0;
  budgetType = '';
  divisionId = 0;
  subDivisionId = 0;
  personInChargeId = 0;
  justification = '';
  createdDate?: Date;
  createdBy = 0;
  budgetHeadName = '';
  subBudgets = new Array<SubBudgets>();
  budgetSpendLimits = new Array<BudgetSpendLimits>();
  budgetCostCodes = new Array<BudgetCostCodes>();
}

