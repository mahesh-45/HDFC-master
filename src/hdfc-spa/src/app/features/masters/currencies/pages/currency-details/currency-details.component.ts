import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';

import { Currency } from '../../shared/currency.model';
import { CurrencyService } from '../../shared/currency.service';

@Component({
  selector: "app-currency-details",
  templateUrl: "./currency-details.component.html",
  styles: [],
})
export class CurrencyDetailsComponent implements OnInit {
  costCodeForm: FormGroup;
  detailData: Currency = new Currency();
  constructor(
    private fb: FormBuilder,
    private currencyService: CurrencyService,
    private route: ActivatedRoute,
    private router: Router,
    private uiService: UiService
  ) {}

  ngOnInit() {
    const currencyUid = this.route.snapshot.paramMap.get("uid");
    if (currencyUid) {
      this.currencyService.getCurrency(currencyUid).subscribe((res) => {
        this.detailData = res;
      });
    }
  }
}
