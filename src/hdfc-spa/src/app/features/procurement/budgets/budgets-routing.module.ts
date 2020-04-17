import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BudgetsListComponent } from './pages/budgets-list/budgets-list.component';
import { BudgetsCreateComponent } from './pages/budgets-create/budgets-create.component';
import { BudgetsDetailsComponent } from './pages/budgets-details/budgets-details.component';
import { BudgetsEditComponent } from './pages/budgets-edit/budgets-edit.component';


const routes: Routes = [
  {
    path: '',
    component: BudgetsListComponent,
  },
  {
    path: 'create',
    component: BudgetsCreateComponent,
  },
  {
    path: 'edit/:uid',
    component: BudgetsEditComponent,
  },
  {
    path: 'detail/:uid',
    component: BudgetsDetailsComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BudgetsRoutingModule { }
