import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DivisionListComponent } from './pages/division-list/division-list.component';
import { DivisionEditorComponent } from './pages/division-editor/division-editor.component';


const routes: Routes = [
  {
    path: "",
    component: DivisionListComponent,
  },
  {
    path: "create",
    component: DivisionEditorComponent,
  },
  {
    path: "edit/:uid",
    component: DivisionEditorComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DivisionRoutingModule { }
