import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { AuthService } from '../services/auth.service';
import { LoggerService } from '../services/logger.service';
import { UiService } from '../services/ui.service';

// import { FORBIDDEN, INTERNAL_SERVER_ERROR, UNAUTHORIZED } from 'http-status-codes';
/**
 * Adds a default error handler to all requests.
 */
@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {
  constructor(
    private logger: LoggerService,
    private router: Router,
    private uiService: UiService,
    private authService: AuthService
  ) { }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next
      .handle(request)
      .pipe(catchError(error => this.errorHandler(error)));
  }

  // Customize the default error handler here if needed
  private errorHandler(response: HttpEvent<any>): Observable<HttpEvent<any>> {
    // if (!environment.production) {
    //   // Do something with the error
    //   // this.logger.logError('Request error ' + JSON.stringify(response));
    //   if (response instanceof HttpErrorResponse) {
    //     if (response.status === 401 && !this.router.url.startsWith("/?")) {
    //       this.authService.logout();
    //       // window.location.replace(environment.authHost);
    //       // window.location.replace(environment.authHost);
    //       this.router.navigate(['/'], {
    //         queryParams: { returnUrl: this.router.routerState.snapshot.url },
    //       });
    //     }
    //   }
    //   throw response;
    // }

    if (response instanceof HttpErrorResponse) {
      if (response.status === 401) {
        this.authService.logout();
        this.router.navigate(['/']);
        // window.location.replace(environment.authHost);
        // this.router.navigate(['/'], {
        //   queryParams: { returnUrl: this.router.routerState.snapshot.url },
        // });
      }

      const applicationError = response.headers.get("Application-Error");
      if (applicationError) {
        this.uiService.showError(applicationError);
        return;
      }
      const serverError = response.error;
      let modelStateErrors = "";
      if (serverError && typeof serverError === "object") {
        for (const key in serverError) {
          if (serverError[key]) {
            modelStateErrors += serverError[key] + "\n";
          }
        }
      }
      this.uiService.showError(
        modelStateErrors || serverError || "Server error"
      );
    }

    throw response;
  }
}
