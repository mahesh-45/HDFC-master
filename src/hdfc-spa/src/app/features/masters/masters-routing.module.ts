import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MastersComponent } from './masters.component';

const routes: Routes = [
  {
    path: "",
    component: MastersComponent,
    children: [
      {
        path: "countries",
        loadChildren: () =>
          import("./countries/countries.module").then((m) => m.CountriesModule),
      },
      {
        path: "employees",
        loadChildren: () =>
          import("./employes/employes.module").then((m) => m.EmployesModule),
      },
      {
        path: "budget-heads",
        loadChildren: () =>
          import("./budget-heads/budget-heads.module").then(
            (m) => m.BudgetHeadsModule
          ),
      },

      {
        path: "cost-codes",
        loadChildren: () =>
          import("./cost-codes/cost-codes.module").then(
            (m) => m.CostCodesModule
          ),
      },

      {
        path: "currencies",
        loadChildren: () =>
          import("./currencies/currencies.module").then(
            (m) => m.CurrenciesModule
          ),
      },

      {
        path: "budget-categories",
        loadChildren: () =>
          import("./budget-categories/budget-categories.module").then(
            (m) => m.BudgetCategoriesModule
          ),
      },
      {
        path: "division",
        loadChildren: () =>
          import("./division/division.module").then((m) => m.DivisionModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MastersRoutingModule {}
