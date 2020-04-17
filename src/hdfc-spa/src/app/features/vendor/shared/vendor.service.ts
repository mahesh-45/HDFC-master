import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VendorInvitation } from './vendor-invitation.model';
import { Vendor } from './vendor.model';



@Injectable({
  providedIn: "root",
})
export class VendorService {
  constructor(private httpClient: HttpClient) { }

  vendorInvitation(vendor: VendorInvitation): Observable<Vendor> {
    return this.httpClient.post<Vendor>(`/api/Vendor/invite`, vendor);
  }


}
