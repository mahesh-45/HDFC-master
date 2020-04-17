import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';

import { MaterialModule } from '../shared/modules/material.module';
import {
  SimpleConfirmationBoxComponent,
} from './components/simple-confirmation-box/simple-confirmation-box.component';
import {
  SimpleDialogComponent,
} from './components/simple-dialog/simple-dialog.component';
import { AuthGuard } from './guards/auth.guard';
import { throwIfAlreadyLoaded } from './guards/module-import-guard';
import { ApiPrefixInterceptor } from './interceptors/api-prefix.interceptor';
import { ErrorHandlerInterceptor } from './interceptors/error-handler.interceptor';
import { HttpTokenInterceptor } from './interceptors/http.token.interceptor';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [SimpleDialogComponent, SimpleConfirmationBoxComponent],
  entryComponents: [SimpleDialogComponent, SimpleConfirmationBoxComponent],
  imports: [CommonModule, HttpClientModule, MaterialModule],
  exports: [],
  providers: [
    AuthService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpTokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiPrefixInterceptor,
      multi: true
    }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, "CoreModule");
  }
}
