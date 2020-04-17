import { Status } from 'shared/enums/status.enum';

export class BudgetHead {
  id = 0;
  uid = '';
  name = '';
  code = '';
  parentId?= 0;
  departmentId = 0;
  costCodeId = 0;
  status = Status.Active;
  createdDate?: Date;
}
export class BudgetHeadList {
  total_count: number;
  items: BudgetHead[];
  sort: string;
  order: string;
  page: number;
  pageSize: number;
  search: any
}

