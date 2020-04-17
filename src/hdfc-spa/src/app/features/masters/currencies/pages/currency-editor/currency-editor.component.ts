import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Status } from 'shared/enums/status.enum';

import { Currency } from '../../shared/currency.model';
import { CurrencyService } from '../../shared/currency.service';

@Component({
  selector: "app-currency-editor",
  templateUrl: "./currency-editor.component.html",
  styles: [],
})
export class CurrencyEditorComponent implements OnInit {
  currencyForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private currencySevice: CurrencyService,
    private route: ActivatedRoute,
    private router: Router,
    private uiService: UiService
  ) {}

  ngOnInit() {
    const currencyUid = this.route.snapshot.paramMap.get("uid");
    if (currencyUid) {
      this.currencySevice.getCurrency(currencyUid).subscribe((res) => {
        this.buildCurrencyForm(res);
        console.log("editDetails", res);
      });
    } else {
      this.buildCurrencyForm(new Currency());
    }
  }

  buildCurrencyForm(currency: Currency) {
    this.currencyForm = this.fb.group({
      uid: [currency.uid],
      code: [currency.code, Validators.required],
      name: [currency.name, Validators.required],
      description: [currency.description, Validators.required],
      status: [currency.status],
    });
  }

  get f() {
    return this.currencyForm.controls;
  }

  submitForm() {
    if (this.currencyForm.valid) {
      // Is form valid?
      if (this.f.uid.value) {
        // Is the form in edit mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about updating this Currency ?",
          onYes: () => this.updateCurrency(),
        });
      } else {
        // If the form in create mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about creating this Currency ?",
          onYes: () => this.createCurrency(),
        });
      }
    }
  }

  createCurrency() {
    const currency = this.extractCostCodes();
    console.log(currency);
    this.currencySevice.createCurrency(currency).subscribe((res) => {
      this.uiService.showSuccess(
        `Currency: ${res.name} is created successfully.`
      );
      this.router.navigate([`/masters/currencies`]);
    });
  }

  updateCurrency() {
    const country = this.extractCostCodes();
    this.currencySevice.updateCurrency(country).subscribe((res) => {
      this.uiService.showSuccess(
        `Currency: ${res.name} is updated successfully.`
      );
      this.router.navigate([`/masters/currencies`]);
    });
  }

  extractCostCodes(): Currency {
    const currency: Currency = {
      uid: this.currencyForm.value.uid,
      code: this.currencyForm.value.code,
      name: this.currencyForm.value.name,
      description: this.currencyForm.value.description,
      status:
        this.currencyForm.value.status === Status.Active
          ? Status.Active
          : Status.InActive,
    };
    return currency;
  }
}
