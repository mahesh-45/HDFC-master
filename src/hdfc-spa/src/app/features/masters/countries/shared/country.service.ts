import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Country, CountryList } from './country.model';

@Injectable({
  providedIn: "root",
})
export class CountryService {
  constructor(private httpClient: HttpClient) {}

  createCountry(country: Country): Observable<Country> {
    return this.httpClient.post<Country>(`/api/Countries`, country);
  }

  updateCountry(country: Country): Observable<Country> {
    return this.httpClient.put<Country>(
      `/api/Countries/${country.uid}`,
      country
    );
  }

  getAllCountries(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(`/api/Countries/All`);
  }
  getCountriesList(
    sort: string,
    order: string,
    page: number,
    pageSize: number,
    search: any
  ): Observable<CountryList> {
    return this.httpClient.get<CountryList>(
      `/api/Countries/list/${sort}/${order}/${page}/${pageSize}/${search}`
    );
  }

  getCountry(uid: string): Observable<Country> {
    return this.httpClient.get<Country>(`/api/Countries/${uid}`);
  }
}
