import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'shared/shared.module';

import { BudgetCategoriesRoutingModule } from './budget-categories-routing.module';
import {
  BudgetCategoriesDetailsComponent,
} from './pages/budget-categories-details/budget-categories-details.component';
import {
  BudgetCategoriesEditorComponent,
} from './pages/budget-categories-editor/budget-categories-editor.component';
import {
  BudgetCategoriesListComponent,
} from './pages/budget-categories-list/budget-categories-list.component';


@NgModule({
  declarations: [BudgetCategoriesListComponent,
    BudgetCategoriesEditorComponent, BudgetCategoriesDetailsComponent,],
  imports: [
    CommonModule,
    BudgetCategoriesRoutingModule,
    SharedModule
  ]
})
export class BudgetCategoriesModule { }
