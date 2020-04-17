import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatRadioModule } from '@angular/material/radio';
import { SharedModule } from 'shared/shared.module';

import { CurrenciesRoutingModule } from './currencies-routing.module';
import {
  CurrencyDetailsComponent,
} from './pages/currency-details/currency-details.component';
import {
  CurrencyEditorComponent,
} from './pages/currency-editor/currency-editor.component';
import { CurrencyListComponent } from './pages/currency-list/currency-list.component';

@NgModule({
  declarations: [
    CurrencyEditorComponent,
    CurrencyListComponent,
    CurrencyDetailsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    CurrenciesRoutingModule,
    MatRadioModule,
  ],
})
export class CurrenciesModule {}
