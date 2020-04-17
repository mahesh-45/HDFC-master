import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  BudgetCategoriesEditorComponent,
} from './pages/budget-categories-editor/budget-categories-editor.component';
import {
  BudgetCategoriesListComponent,
} from './pages/budget-categories-list/budget-categories-list.component';


const routes: Routes = [
  {
    path: '',
    component: BudgetCategoriesListComponent,
  },
  {
    path: 'create',
    component: BudgetCategoriesEditorComponent,
  },
  {
    path: 'edit/:uid',
    component: BudgetCategoriesEditorComponent
  },
  {
    path: 'edit/parentId/:parentId/id/:0',
    component: BudgetCategoriesEditorComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BudgetCategoriesRoutingModule { }
