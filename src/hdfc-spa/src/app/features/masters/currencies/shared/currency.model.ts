import { Status } from 'shared/enums/status.enum';

export class Currency {
  uid = "";
  code = "";
  name = "";
  description = "";
  status = Status.Active;
  createdDate?: Date;
}
