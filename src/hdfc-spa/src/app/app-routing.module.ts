import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./features/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'masters',
    loadChildren: () =>
      import('./features/masters/masters.module').then(m => m.MastersModule)
  },

  {
    path: 'vendors',
    loadChildren: () =>
      import('./features/vendor/vendor.module').then(m => m.VendorModule)

  },
  {
    path: 'procurement',
    loadChildren: () =>
      import('./features/procurement/procurement.module').then(m => m.ProcurementModule)

  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
