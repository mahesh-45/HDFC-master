import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ProcurementModule } from "./procurement.module";
import { ProcurementComponent } from "./procurement.component";

const routes: Routes = [
  {
    path: "",
    component: ProcurementComponent,
    children: [
      {
        path: "budgets",
        loadChildren: () =>
          import("./budgets/budgets.module").then((m) => m.BudgetsModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProcurementRoutingModule {}
