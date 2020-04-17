import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'shared/shared.module';

import { CostCodesRoutingModule } from './cost-codes-routing.module';
import {
  CostCodeEditorComponent,
} from './pages/cost-code-editor/cost-code-editor.component';
import { CostCodeListComponent } from './pages/cost-code-list/cost-code-list.component';
import { CostCodeDetailComponent } from './pages/cost-code-detail/cost-code-detail.component';

@NgModule({
  declarations: [CostCodeEditorComponent, CostCodeListComponent, CostCodeDetailComponent],
  imports: [CommonModule, CostCodesRoutingModule, SharedModule],
})
export class CostCodesModule {}
