import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  CostCodeDetailComponent,
} from './pages/cost-code-detail/cost-code-detail.component';
import {
  CostCodeEditorComponent,
} from './pages/cost-code-editor/cost-code-editor.component';
import { CostCodeListComponent } from './pages/cost-code-list/cost-code-list.component';

const routes: Routes = [
  {
    path: "",
    component: CostCodeListComponent,
  },
  {
    path: "create",
    component: CostCodeEditorComponent,
  },
  {
    path: "edit/:uid",
    component: CostCodeEditorComponent,
  },
  {
    path: "detail/:uid",
    component: CostCodeDetailComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CostCodesRoutingModule {}
