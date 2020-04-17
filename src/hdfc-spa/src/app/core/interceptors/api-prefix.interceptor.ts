import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

/**
 * Prefixes all requests with `environment.host`. https://localhost:44357/assets/i18n/en.json
 */
@Injectable()
export class ApiPrefixInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.url.startsWith('/assets/i18n/')) {
      return next.handle(request);
    }

    if (environment.host !== null) {
      request = request.clone({ url: environment.host + request.url });
    }
    return next.handle(request);
  }
}
