import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { CoreModule } from 'core/core.module';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


@NgModule({

  declarations: [AppComponent],

  imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule,
    MatIconModule, MatButtonModule,
    LoadingBarHttpClientModule,
    LoadingBarRouterModule,
    MatFormFieldModule,
    MatTabsModule,
    MatTableModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    FormsModule,
    MatListModule,
    RouterModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    CoreModule, ToastrModule.forRoot({
      timeOut: 2500,
      positionClass: "toast-top-right",
      progressBar: true,
      progressAnimation: "increasing",
    }),
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'en-GB' }],
  bootstrap: [AppComponent],
})



export class AppModule { }
