import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LayoutModule } from 'shared/layout/layout.module';
import { SharedModule } from 'shared/shared.module';
import { MastersRoutingModule } from './masters-routing.module';
import { MastersComponent } from './masters.component';


@NgModule({
  declarations: [MastersComponent, ],
  imports: [
    CommonModule,
    MastersRoutingModule, LayoutModule, SharedModule
  ]
})
export class MastersModule { }