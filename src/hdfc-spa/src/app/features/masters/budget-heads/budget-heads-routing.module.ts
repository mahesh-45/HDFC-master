import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  BudgetHeadsEditorComponent,
} from './pages/budget-heads-editor/budget-heads-editor.component';
import {
  BudgetHeadsListComponent,
} from './pages/budget-heads-list/budget-heads-list.component';


const routes: Routes = [
  {
    path: '',
    component: BudgetHeadsListComponent,
  },
  {
    path: 'create',
    component: BudgetHeadsEditorComponent,
  },
  {
    path: 'edit/:uid',
    component: BudgetHeadsEditorComponent,
  },
  {
    path: 'edit',
    component: BudgetHeadsEditorComponent,
  },
  {
    path: 'edit/parentId/:parentId/id/:0',
    component: BudgetHeadsEditorComponent,
  },
  {
    path: 'sublist/parentId/:parentId',
    component: BudgetHeadsListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BudgetHeadsRoutingModule { }
