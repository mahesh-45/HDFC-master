import { Status } from 'shared/enums/status.enum';

export class BudgetCategories {

  uid = '';
  name = '';
  code = ''
  status = Status.Active;
  createdDate?: Date;
}
export class BudgetCategoriesList {
  items: BudgetCategories[];
  total_count: number;
  sort: string;
  order: string;
  page: number;
  pageSize: number;
  search: any
}

