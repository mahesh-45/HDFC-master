import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { BudgetHead, BudgetHeadList } from './budget-head';

@Injectable({
  providedIn: 'root'
})
export class BudgetHeadsService {
  constructor(private httpClient: HttpClient) { }

  private reloadListSource = new BehaviorSubject(0);
  reloadListEvent = this.reloadListSource.asObservable();

  reloadList() {
    debugger;
    console.log('running');
    this.reloadListSource.next(1);
  }
  getAllBudgetHeads(): Observable<BudgetHead[]> {
    return this.httpClient.get<BudgetHead[]>(`/api/BudgetHeads`);
  }
  getBudgetHeadsList(budgetHeadList: BudgetHeadList): Observable<BudgetHeadList> {
    return this.httpClient.post<BudgetHeadList>(`/api/BudgetHeads/search`, budgetHeadList);
  }
  getBudgetHead(uid: string): Observable<BudgetHead> {
    return this.httpClient.get<BudgetHead>(`/api/BudgetHeads/${uid}`);
  }
  updateBudgetHead(budgetHead: BudgetHead) {
    return this.httpClient.put<BudgetHead>(`/api/BudgetHeads/${budgetHead.uid}`, budgetHead)
  }
  createBudgetHead(budgetHead: any) {
    return this.httpClient.post<BudgetHead>(`/api/BudgetHeads`, budgetHead);
  }
  getBudgetHeadById(id: number): Observable<BudgetHead> {
    console.log('m in service');
    return this.httpClient.get<BudgetHead>(`/api/BudgetHeads/parent/` + id);
  }
  list(id: number): Observable<BudgetHead[]> {
    return this.httpClient.get<BudgetHead[]>('/api/BudgetHeads/children/' + id);
  }


}
