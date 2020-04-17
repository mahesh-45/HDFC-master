import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SharedModule } from 'shared/shared.module';

import { EmployesRoutingModule } from './employes-routing.module';
import { EmployeListComponent } from './pages/employe-list/employe-list.component';
import {
  EmployeeCreateComponent,
} from './pages/employee-create/employee-create.component';
import {
  EmployeeEditorComponent,
} from './pages/employee-editor/employee-editor.component';
import { EmployeeDetailComponent } from './pages/employee-detail/employee-detail.component';

@NgModule({
  declarations: [
    EmployeListComponent,
    EmployeeCreateComponent,
    EmployeeEditorComponent,
    EmployeeDetailComponent,
  ],
  imports: [
    CommonModule,
    EmployesRoutingModule,
    SharedModule,
    MatFormFieldModule,
    MatInputModule,
  ],
})
export class EmployesModule { }
