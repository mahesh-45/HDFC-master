import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BudgetCategories, BudgetCategoriesList } from './budget-categories';

@Injectable({
  providedIn: 'root'
})
export class BudgetCategoriesService {
  constructor(private httpClient: HttpClient) { }
  createBudgetCategories(budgetCategories: BudgetCategories): Observable<BudgetCategories> {
    return this.httpClient.post<BudgetCategories>(`/api/BudgetCategories`, budgetCategories);
  }
  updateBudgetCategories(budgetCategories: BudgetCategories) {
    return this.httpClient.put<BudgetCategories>(
      `/api/BudgetCategories/${budgetCategories.uid}`,
      budgetCategories
    );
  }
  getBudgetCategoriesList(budgetCategoriesList: BudgetCategoriesList): Observable<BudgetCategoriesList> {
    return this.httpClient.post<BudgetCategoriesList>(`/api/BudgetCategories/search`, budgetCategoriesList);
  }
  getBudgetCategory(uid: string): Observable<BudgetCategories> {
    return this.httpClient.get<BudgetCategories>(`/api/BudgetCategories/${uid}`);
  }
}
