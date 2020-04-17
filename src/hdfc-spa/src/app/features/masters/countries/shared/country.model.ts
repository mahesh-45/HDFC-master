import { Status } from 'shared/enums/status.enum';

export class Country {
  uid = "";
  name = "";
  code = "";
  status = Status.Active;
  createdDate?: Date;
}
export class CountryList {
  items: Country[];
  total_count: number;
}
