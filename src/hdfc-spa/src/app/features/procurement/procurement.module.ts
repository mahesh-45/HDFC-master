import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ProcurementRoutingModule } from "./procurement-routing.module";
import { ProcurementComponent } from "./procurement.component";
import { SharedModule } from "shared/shared.module";
import { LayoutModule } from "shared/layout/layout.module";

@NgModule({
  declarations: [ProcurementComponent],
  imports: [CommonModule, ProcurementRoutingModule, SharedModule, LayoutModule],
})
export class ProcurementModule {}
