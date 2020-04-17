import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnumService {
  constructor(private http: HttpClient) {}

  getDocStackLinkedToTypeEnum(): Observable<string[]> {
    return this.http.get<string[]>(`/api/Enum/DocStackLinkedToTypeEnum`);
  }

  getDocStackCostedToTypeEnum(): Observable<string[]> {
    return this.http.get<string[]>(`/api/Enum/DocStackCostedToTypeEnum`);
  }

  getFreightForwardTypeEnum(): Observable<string[]> {
    return this.http.get<string[]>(`/api/Enum/FreightForwardTypeEnum`);
  }
  getQuotationAttachmentTypes(): Observable<string[]> {
    return this.http.get<string[]>(`/api/Enum/QuotationAttachmentTypes`);
  }
}
