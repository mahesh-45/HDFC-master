import { Status } from 'shared/enums/status.enum';

export class CostCodes {
  uid = "";
  code = "";
  name = "";
  bhEmpCode = "";
  bh = "";
  adGroup = "";
  adEmpCode = "";
  head = "";
  status = Status.Active;
  createdDate?: Date;
}
export class CostCodeList {
  items: CostCodes[];
  total_count: number;
}
