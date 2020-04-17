import { Status } from 'shared/enums/status.enum';
import { SubDivisions } from './sub-division.model';

export class Division {
  uid = ''
  name = '';
  code = '';
  status = Status.Active;
  subDivisions = new Array<SubDivisions>();
}
export class DivisionList {
  items: Division[];
  total_count: number;
  sort: string;
  order: string;
  page: number;
  pageSize: number;
  search: any
}