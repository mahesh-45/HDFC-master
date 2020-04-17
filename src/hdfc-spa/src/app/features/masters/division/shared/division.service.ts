import { Injectable } from '@angular/core';
import { DivisionEditorComponent } from '../pages/division-editor/division-editor.component';
import { Division, DivisionList } from './division.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DivisionService {

  constructor(private httpClient: HttpClient) { }
  getDivision(uid: string): Observable<Division> {
    return this.httpClient.get<Division>(`/api/Divisions/${uid}`)
  }

  createDivision(division: Division): Observable<Division> {
    return this.httpClient.post<Division>(`/api/Divisions`, division)
  }
  updateDivision(division: Division): Observable<Division> {
    return this.httpClient.put<Division>(
      `/api/Divisions/`,
      division
    );
  }
  deleteSubDivision(uid: string, subDivisionId: number): Observable<any> {
    return this.httpClient.delete<Division>(
      `/api/Divisions/${uid}/deleteSubDivision/${subDivisionId}`
    );
  }
  getDivisionList(divisionList: DivisionList): Observable<DivisionList> {
    return this.httpClient.post<DivisionList>(`/api/Divisions/search`, divisionList);
  }
}
