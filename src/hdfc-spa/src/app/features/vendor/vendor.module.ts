import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'shared/shared.module';
import { LayoutModule } from 'shared/layout/layout.module';
import { MatIconModule } from '@angular/material/icon';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { VendorComponent } from './vendor.component';
import { VendorRoutingModule } from './vendor-routing.module';
import { VendorRegistrationComponent } from './pages/vendor-registration/vendor-registration.component';
import { VendorInvitationComponent } from './pages/vendor-invitation/vendor-invitation.component';
import { VendorListComponent } from './pages/vendor-list/vendor-list.component';
import { VendorDetailsComponent } from './pages/vendor-details/vendor-details.component';

@NgModule({
  declarations: [VendorComponent, VendorRegistrationComponent, VendorInvitationComponent, VendorListComponent, VendorDetailsComponent],
  imports: [
    CommonModule,
    VendorRoutingModule, SharedModule, LayoutModule, MatIconModule, MatInputModule, MatFormFieldModule,
    MatSelectModule, MatRadioModule, MatCheckboxModule, FormsModule, ReactiveFormsModule, MatDatepickerModule, MatListModule, MatTabsModule
  ],
  exports: [
  ]
})
export class VendorModule { }
