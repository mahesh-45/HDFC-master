import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Currency } from './currency.model';

@Injectable({
  providedIn: "root",
})
export class CurrencyService {
  constructor(private httpClient: HttpClient) {}

  getCurrencyAll(): Observable<any> {
    return this.httpClient.get<any>(`/api/Currency/all`);
  }
  createCurrency(currency: Currency): Observable<Currency> {
    return this.httpClient.post<Currency>(`/api/Currency`, currency);
  }
  updateCurrency(currency: Currency): Observable<Currency> {
    return this.httpClient.put<Currency>(
      `/api/Currency/${currency.uid}`,
      currency
    );
  }
  getCurrency(uid: string): Observable<Currency> {
    return this.httpClient.get<Currency>(`/api/Currency/${uid}`);
  }
}
