import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BudgetsRoutingModule } from './budgets-routing.module';
import { BudgetsCreateComponent } from './pages/budgets-create/budgets-create.component';
import { BudgetsEditComponent } from './pages/budgets-edit/budgets-edit.component';
import { BudgetsDetailsComponent } from './pages/budgets-details/budgets-details.component';
import { BudgetsListComponent } from './pages/budgets-list/budgets-list.component';
import { SharedModule } from 'shared/shared.module';


@NgModule({
  declarations: [BudgetsCreateComponent, BudgetsEditComponent, BudgetsDetailsComponent, BudgetsListComponent],
  imports: [
    CommonModule,
    BudgetsRoutingModule,
    SharedModule
  ]
})
export class BudgetsModule { }
