import { Injectable } from '@angular/core';
// import { TreeNode, Budget, BudgetList } from './budgets.model';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { BudgetList } from './budget-list.model';
import { TreeNode } from './tree.model';
import { Budget } from './budgets.model';
import { SubBudgets } from './subbudgets.model';

@Injectable({
  providedIn: 'root'
})
export class BudgetsService {

  constructor(private httpClient: HttpClient) { }
  private reloadListSource = new BehaviorSubject(0);
  reloadListEvent = this.reloadListSource.asObservable();

  reloadList() {
    this.reloadListSource.next(1);
  }
  getList(search: string): Observable<BudgetList> {
    const searchDto = new SearchDto();
    searchDto.search = search;
    return this.httpClient.post<BudgetList>(`/api/Budgets/search/`, searchDto);
  }
  getAllBudgetHeads(): any {
    return this.httpClient.get<TreeNode[]>('/api/BudgetHeads/GetTree/');
  }
  createBudget(FormValue: Budget): Observable<Budget> {
    return this.httpClient.post<Budget>(`/api/Budgets`, FormValue)
  }
  updateBudget(FormValue: Budget): Observable<Budget> {
    return this.httpClient.put<Budget>(`/api/Budgets/`, FormValue)
  }
  getAllBudgets(): Observable<Budget[]> {
    return this.httpClient.get<Budget[]>(`/api/Budgets`)
  }
  getBudgetByUid(uid: string): Observable<Budget> {
    return this.httpClient.get<Budget>(`/api/Budgets/${uid}`)
  }
  deleteSubBudgets(uid: string, subBudgetId: number): Observable<any> {
    return this.httpClient.delete<SubBudgets>(
      `/api/Budgets/${uid}/deleteSubBudgets/${subBudgetId}`
    );
  }
  deleteBudgetSpendLimits(uid: string, budgetSpendLimitId: number): Observable<any> {
    return this.httpClient.delete<SubBudgets>(
      `/api/Budgets/${uid}/deleteBudgetSpendLimit/${budgetSpendLimitId}`
    );
  }
  holdBudget(budgetId: number): Observable<any> {
    return this.httpClient.put<Budget>(`/api/Budgets/Hold/${budgetId}`, null);
  }
  getUserById(userId: number): Observable<any> {
    return this.httpClient.get(`/api/Users/${userId}`);
  }
}
export class SearchDto {
  sort: string; order: string; page: number; pageSize: number; search: any
}
