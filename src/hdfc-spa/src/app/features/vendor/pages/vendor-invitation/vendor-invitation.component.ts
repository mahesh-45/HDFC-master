import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VendorInvitation } from '../../shared/vendor-invitation.model';
import { VendorService } from '../../shared/vendor.service'
import { UiService } from 'core/services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vendor-invitation',
  templateUrl: './vendor-invitation.component.html',
  styles: []
})
export class VendorInvitationComponent implements OnInit {

  inviteForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private VendorService: VendorService,
    private uiService: UiService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.Buildform(new VendorInvitation());
  }

  Buildform(VendorInfo: VendorInvitation) {
    this.inviteForm = this.fb.group({
      name: [VendorInfo.name, Validators.required],
      email: [VendorInfo.email, Validators.required],
      vendorOrganisationType: [VendorInfo.vendorOrganisationType, Validators.required],
      project: [VendorInfo.project, Validators.required],
      projectLocationCode: [VendorInfo.projectLocationCode, Validators.required],
      vendorType: [VendorInfo.vendorType, Validators.required]

      // {
      //   "name": "string",
      //   "email": "string",
      //   "vendorType": 0,
      //   "project": "string",
      //   "projectLocationCode": "string",
      //   "vendorOrganisationType": "string"
      // }


    })

  }

  onsubmit(vendorInvitation: VendorInvitation) {
    console.log('any', vendorInvitation);
    this.uiService.showConfirmationBox({
      message: "Are you sure about inviting vendor ?",
      onYes: () => this.inviteVendor(vendorInvitation),
    });



  }

  inviteVendor(vendorInvitation: VendorInvitation) {
    this.VendorService.vendorInvitation(vendorInvitation).subscribe((res) => {
      this.uiService.showSuccess(
        `Vendor: ${res.name} is invitaed successfully.`
      );
      this.router.navigate([`/vendors`]);
    });
  }
}
