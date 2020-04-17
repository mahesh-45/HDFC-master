import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthService } from '../services/auth.service';

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let headersConfig = {};
    if (req.headers.keys().length === 0) {
      headersConfig = {
        'Content-Type': 'application/json',
        Accept: 'application/json'
      };
    }
    // debugger;
    // console.log('Before ' + headersConfig['Content-Type']);
    const token = this.authService.getToken();
    if (token) {
      headersConfig['Authorization'] = `Bearer ${token}`;
      if (headersConfig['Content-Type'] === 'application/json-patch+json') {
        headersConfig['Content-Type'] = `application/json`;
      }
    }
    // console.log('After ' + headersConfig['Content-Type']);
    const request = req.clone({ setHeaders: headersConfig });
    return next.handle(request);
  }
}
