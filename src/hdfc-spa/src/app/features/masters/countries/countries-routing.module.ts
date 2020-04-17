import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CountryEditorComponent } from './pages/country-editor/country-editor.component';
import { CountryListComponent } from './pages/country-list/country-list.component';



const routes: Routes = [
  {
    path: '',
    component: CountryListComponent,
  },
  {
    path: 'create',
    component: CountryEditorComponent,
  },
  {
    path: 'edit/:uid',
    component: CountryEditorComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CountriesRoutingModule { }
