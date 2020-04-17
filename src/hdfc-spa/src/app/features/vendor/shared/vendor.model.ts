import { VendorAttachment } from './vendor-attachment.model';
import { VendorUsers } from './vendor-user.model';
import { VendorMSMEDetails } from './VendorMSMEDetails.model';
import { VendorLocations } from './vendor-location.model';
import { VendorBankDetail } from './VendorBankDetail.model';

export class Vendor {
  name = "";
  contactPerson = "";

  companyType = '';


  catalogue: Array<Catlogue>
  subCatalogue: Array<Subcatlogue>
  vendorType = 0;
  email = "";
  mobileNo = "";
  landlineNo = "";
  city = "";
  state = "";
  pinCode = "";
  panNo = "";
  panHolderName = "";
  aadharNo = "";
  vendorStatus = 0;
  proprietorName = "";
  isCompositeTaxable = true;
  status = 0;
  vendorAttachments: VendorAttachment[] = [];
  VendorUsers: VendorUsers[] = [];
  VendorMSMEDetails: VendorMSMEDetails[] = [];
  VendorLocations: VendorLocations[] = [];
  VendorBankDetail: VendorBankDetail[] = [];

  createdBy = 0;
  uid = "";
  createdDate = "";
  updatedBy = 0;
  updatedDate = "";
  updatedByUsername = "";
  id = 0;
  notCheck = true;
}

export class inviteVendor {
  useremail = '';
}
export class Catlogue {
  catloague = ''
}
export class Subcatlogue {
  subCatahgue = ''
}
export class PhoneNumber {
  num = 0;
}