import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DivisionRoutingModule } from './division-routing.module';
import { SharedModule } from 'shared/shared.module';
import { DivisionListComponent } from './pages/division-list/division-list.component';
import { DivisionEditorComponent } from './pages/division-editor/division-editor.component';


@NgModule({
  declarations: [DivisionListComponent, DivisionEditorComponent],
  imports: [
    CommonModule,
    DivisionRoutingModule,
    SharedModule
  ]
})
export class DivisionModule { }
