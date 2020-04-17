import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './modules/material.module';
import { DecimalDirective } from './directives/decimal.directive';
import { DateFormatPipe } from './pipes/date-format.pipe';
import { UtcDateFormatPipe } from './pipes/utc-date-format.pipe';

//import { DecimalDirective } from './directives/decimal.directive';
@NgModule({
  declarations: [DecimalDirective, DateFormatPipe, UtcDateFormatPipe],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MaterialModule],
  exports: [FormsModule, ReactiveFormsModule, MaterialModule, DecimalDirective, DateFormatPipe, UtcDateFormatPipe]
})
export class SharedModule { }
