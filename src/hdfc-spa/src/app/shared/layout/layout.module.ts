import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'shared/shared.module';

import { MaterialModule } from './../modules/material.module';
import { FooterComponent } from './footer/footer.component';
import { LayoutComponent } from './layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

@NgModule({
  declarations: [
    LayoutComponent,
    ToolbarComponent,
    NavbarComponent,
    FooterComponent,
  ],
  imports: [CommonModule, MaterialModule, RouterModule, SharedModule],
  exports: [LayoutComponent, ToolbarComponent, NavbarComponent],
})
export class LayoutModule { }
