import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  CurrencyDetailsComponent,
} from './pages/currency-details/currency-details.component';
import {
  CurrencyEditorComponent,
} from './pages/currency-editor/currency-editor.component';
import { CurrencyListComponent } from './pages/currency-list/currency-list.component';

const routes: Routes = [
  {
    path: "",
    component: CurrencyListComponent,
  },
  {
    path: "create",
    component: CurrencyEditorComponent,
  },
  {
    path: "edit/:uid",
    component: CurrencyEditorComponent,
  },
  {
    path: "details/:uid",
    component: CurrencyDetailsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CurrenciesRoutingModule {}
