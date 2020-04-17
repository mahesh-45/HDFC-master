import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BudgetHeadsRoutingModule } from './budget-heads-routing.module';
import { BudgetHeadsListComponent } from './pages/budget-heads-list/budget-heads-list.component';
import { BudgetHeadsEditorComponent } from './pages/budget-heads-editor/budget-heads-editor.component';
import { BudgetHeadsDetailsComponent } from './pages/budget-heads-details/budget-heads-details.component';
import { SharedModule } from 'shared/shared.module';


@NgModule({
  declarations: [BudgetHeadsListComponent, BudgetHeadsEditorComponent, BudgetHeadsDetailsComponent],
  imports: [
    CommonModule,
    BudgetHeadsRoutingModule,
    SharedModule
  ]
})
export class BudgetHeadsModule { }
