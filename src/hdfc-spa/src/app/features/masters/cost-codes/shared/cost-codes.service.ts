import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CostCodeList, CostCodes } from './cost-codes.model';

@Injectable({
  providedIn: "root",
})
export class CostCodesService {
  constructor(private httpClient: HttpClient) {}

  // getCostCode(): Observable<any> {
  //   return this.httpClient.get<any>(`/api/CostCode`);
  // }
  getCostCodeAll(): Observable<any> {
    return this.httpClient.get<any>(`/api/costcode/all`);
  }
  createCostCode(costcode: CostCodes): Observable<CostCodes> {
    return this.httpClient.post<CostCodes>(`/api/CostCode`, costcode);
  }
  updateCostCode(costcode: CostCodes): Observable<CostCodes> {
    return this.httpClient.put<CostCodes>(
      `/api/CostCode/${costcode.uid}`,
      costcode
    );
  }
  getCostCode(uid: string): Observable<CostCodes> {
    return this.httpClient.get<CostCodes>(`/api/CostCode/${uid}`);
  }

  getCountriesList(search: any): Observable<CostCodeList> {
    return this.httpClient.get<CostCodeList>(`/api/CostCode/list/${search}`);
  }
}
