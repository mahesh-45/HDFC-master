import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorComponent } from './vendor.component';
import { VendorRegistrationComponent } from './pages/vendor-registration/vendor-registration.component';
import { VendorDetailsComponent } from './pages/vendor-details/vendor-details.component';
import { VendorInvitationComponent } from './pages/vendor-invitation/vendor-invitation.component';
import { VendorListComponent } from './pages/vendor-list/vendor-list.component';


const routes: Routes = [
  {
    path: '',
    component: VendorComponent,
    children: [
      {
        path: '',
        component: VendorListComponent,
      },
      {
        path: 'vendor-invitation',
        component: VendorInvitationComponent,
      },

      {
        path: 'details/:uid',
        component: VendorDetailsComponent,
      },
    ]
  },
  {
    path: 'vendor-registration',
    component: VendorRegistrationComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }
