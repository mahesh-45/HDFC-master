import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Employe, EmployeeList } from './employe.model';

@Injectable({
  providedIn: "root",
})
export class EmployeeService {
  constructor(private httpClient: HttpClient) { }

  createEmployee(employee: Employe): Observable<Employe> {
    return this.httpClient.post<Employe>(`/api/Employees`, employee);
  }

  updateEmployee(employee: Employe): Observable<Employe> {
    return this.httpClient.put<Employe>(
      `/api/Employees/${employee.uid}`,
      employee
    );
  }

  getAllEmployees(): Observable<Employe[]> {
    return this.httpClient.get<Employe[]>(`/api/Employees/All`);
  }
  getEmployeesList(

    search: any
  ): Observable<EmployeeList> {
    return this.httpClient.get<EmployeeList>(
      `/api/Employees/list/${search}`
    );
  }

  getEmployee(uid: string): Observable<Employe> {
    return this.httpClient.get<Employe>(`/api/Employees/${uid}`);
  }
}
