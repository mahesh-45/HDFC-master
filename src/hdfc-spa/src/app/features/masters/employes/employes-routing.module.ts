import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeListComponent } from './pages/employe-list/employe-list.component';
import {
  EmployeeCreateComponent,
} from './pages/employee-create/employee-create.component';
import {
  EmployeeDetailComponent,
} from './pages/employee-detail/employee-detail.component';
import {
  EmployeeEditorComponent,
} from './pages/employee-editor/employee-editor.component';

const routes: Routes = [
  {
    path: "",
    component: EmployeListComponent,
  },
  {
    path: "create",
    component: EmployeeCreateComponent,
  },
  {
    path: "edit/:uid",
    component: EmployeeEditorComponent,
  },
  {
    path: "detail/:uid",
    component: EmployeeDetailComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EmployesRoutingModule {}
