import { Status } from 'shared/enums/status.enum';

export class Employe {
  uid = "";
  firstName = "";
  middleName = "";
  lastName = "";
  title = "";
  employeeNumber = "";
  gender = "";
  panNumber = "";
  dateOfBirth = "";
  maritalStatus = "";
  officialEmail = "";
  contact1 = "";
  contact2 = "";
  band = "";
  locationName = "";
  supervisorEmpCode = "";
  hireDate = "";
  recordStatus = "";
  countryId = "";
  city = "";
  region = "";
  actualTerminationDate = "";
  employeeStatus = Status.Active;
}

export class EmployeeList {
  items: Employe[];
  total_count: number;
}
