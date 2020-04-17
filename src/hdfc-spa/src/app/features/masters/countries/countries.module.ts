import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'shared/shared.module';

import { CountriesRoutingModule } from './countries-routing.module';
import { CountryEditorComponent } from './pages/country-editor/country-editor.component';
import { CountryListComponent } from './pages/country-list/country-list.component';


@NgModule({
  declarations: [CountryListComponent, CountryEditorComponent],
  imports: [
    CommonModule,
    CountriesRoutingModule,
    SharedModule
  ]
})
export class CountriesModule { }
