import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';

import { CostCodes } from '../../shared/cost-codes.model';
import { CostCodesService } from '../../shared/cost-codes.service';

@Component({
  selector: "app-cost-code-detail",
  templateUrl: "./cost-code-detail.component.html",
  styles: [],
})
export class CostCodeDetailComponent implements OnInit {
  costCodeForm: FormGroup;
  detailData: CostCodes = new CostCodes();
  constructor(
    private fb: FormBuilder,
    private costCodesService: CostCodesService,
    private route: ActivatedRoute,
    private router: Router,
    private uiService: UiService
  ) {}

  ngOnInit() {
    const costCodeUid = this.route.snapshot.paramMap.get("uid");
    if (costCodeUid) {
      this.costCodesService.getCostCode(costCodeUid).subscribe((res) => {
        this.detailData = res;
      });
    }
  }
}
