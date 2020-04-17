import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Status } from 'shared/enums/status.enum';

import { Country } from '../../shared/country.model';
import { CountryService } from '../../shared/country.service';

@Component({
  selector: "app-country-editor",
  templateUrl: "./country-editor.component.html",
  styles: [],
})
export class CountryEditorComponent implements OnInit {
  countryForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private countryService: CountryService,
    private route: ActivatedRoute,
    private router: Router,
    private uiService: UiService
  ) {}

  ngOnInit() {
    const countryUid = this.route.snapshot.paramMap.get("uid");
    if (countryUid) {
      this.countryService.getCountry(countryUid).subscribe((res) => {
        this.buildCountryForm(res);
      });
    } else {
      this.buildCountryForm(new Country());
    }
  }

  buildCountryForm(country: Country) {
    this.countryForm = this.formBuilder.group({
      uid: [country.uid],
      name: [country.name, Validators.required],
      code: [country.code, Validators.required],
      status: [country.status],
    });
  }

  get f() {
    return this.countryForm.controls;
  }

  submitForm() {
    if (this.countryForm.valid) {
      // Is form valid?
      if (this.f.uid.value) {
        // Is the form in edit mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about updating this Country?",
          onYes: () => this.updateCountry(),
        });
      } else {
        // If the form in create mode
        this.uiService.showConfirmationBox({
          message: "Are you sure about creating this Country?",
          onYes: () => this.createCountry(),
        });
      }
    }
  }

  createCountry() {
    const country = this.extractCountry();
    this.countryService.createCountry(country).subscribe((res) => {
      this.uiService.showSuccess(
        `Country: ${res.name} is created successfully.`
      );
      this.router.navigate([`/masters/countries`]);
    });
  }

  updateCountry() {
    const country = this.extractCountry();
    this.countryService.updateCountry(country).subscribe((res) => {
      this.uiService.showSuccess(
        `Country: ${res.name} is updated successfully.`
      );
      this.router.navigate([`/masters/countries`]);
    });
  }

  extractCountry(): Country {
    const country: Country = {
      uid: this.countryForm.value.uid,
      name: this.countryForm.value.name,
      code: this.countryForm.value.code,
      status:
        this.countryForm.value.status === Status.Active
          ? Status.Active
          : Status.InActive,
    };
    return country;
  }
}
