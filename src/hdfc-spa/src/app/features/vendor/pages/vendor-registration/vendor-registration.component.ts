import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';
import { Vendor } from '../../shared/vendor.model';
import { VendorBankDetail } from '../../shared/VendorBankDetail.model';

@Component({
  selector: 'app-vendor-registration',
  templateUrl: './vendor-registration.component.html',
  styles: []
})
export class VendorRegistrationComponent implements OnInit {

  comp: any;
  registerForm: FormGroup;
  propName = false;
  IsRegisteredUnderMSME = false;
  selectCatlog: any[] = [];
  selectSubcat: any[] = [];
  selectedNum: any[] = [];
  selectedCurrency: any[] = [];
  hide = true;
  state: any[] = [
    'Karnataka',
    'Kerala',
    'Madya pradesh',
    'Gujarath',
    'Arunachal Pradesh',
    'Goa',
    'Thelangana',
    'Thamilanadu',
    'West Bamgal',
    'Delhi',
    'Panjab'
  ]
  currency: any[] = [
    'INR', 'USD', 'GBP', 'EUR', 'new123', 'QTR', 'DEN'
  ]

  accountType: any[] = [
    'Savings',
    'Current'
  ]


  document: any[] = [{
    num: 1,
    DocumentType: 'PAN Card',
    Mandaotory: 'Yes',
    DocumentName: '',
    download: false
  },
  {
    num: 2,
    DocumentType: 'Vendor Evaluavtion Form(Download this)',
    Mandaotory: 'No',
    DocumentName: '',
    download: true
  },
  {
    num: 3,
    DocumentType: 'Cancelled Cheque',
    Mandaotory: 'Yes',
    DocumentName: '',
    download: false
  },
  {
    num: 4,
    DocumentType: 'COI/Establishment Certificate/Propreitorship Certificate',
    Mandaotory: 'No',
    DocumentName: '',
    download: false
  },
  {
    num: 5,
    DocumentType: 'GST Registration/GST Declaration Certificate ',
    Mandaotory: 'Yes',
    DocumentName: '',
    download: false
  },
  {
    num: 6,
    DocumentType: 'NDA(Download this)',
    Mandaotory: 'Yes',
    DocumentName: '',
    download: true
  },
  {
    num: 7,
    DocumentType: 'Additional(If more than Two Docs Upload in zip)',
    Mandaotory: 'No',
    DocumentName: '',
    download: false

  }
  ];

  country: any[] = [
    'USA',
    'China',
    'Bahrain',
    'South Africa',
    'England',
    'farance',
    'Italy',
    'India'
  ]

  Pnum: number[] = [
    12356888,
    1425656,
    123232323,
    23232323
  ]

  locationType: any[] = [
    'Company',
    'item',
    'House'
  ]

  vendorStatus: any[] = [
    'Individual',
    'HUF',
    'Proprietorship',
    'PartnershipFirm',
    'Company',
    'Others'
  ];

  Bnature: any[] = [
    'Manufacturer',
    'Traders',
    'Service Providers',
    'Manufacturer and Service'
  ];


  catalog: any[] = [
    'p1',
    'p2',
    'p3',
    'p4',
  ];

  subCatlog: any[] = [
    'p1',
    'p2',
    'p3',
    'p4',
  ]

  constructor(
    private fb: FormBuilder
  ) {
    this.buildForm(new Vendor());
    this.addUser(1);
  }

  ngOnInit(): void {
    //this.buildForm(new VendorRegister())
  }


  registr(event) {
    if (event.value === 'yes') {
      this.IsRegisteredUnderMSME = true;
      this.registerForm.controls.registerdUnderMSME.setValue('Yes');
    } else {
      this.IsRegisteredUnderMSME = false;
      this.registerForm.controls.registerdUnderMSME.setValue('No');
    }
  }

  SelectionComapny(type) {
    if (type === 'Proprietorship') {
      this.propName = true;
    } else {
      this.propName = false;
    }
  }

  SelectionCat(types: any) {
    // console.log("types", types)
    this.selectCatlog = [];
    types.forEach(element => {
      this.selectCatlog.push({
        Catalogue: element
      });
    });
  }

  SelectionCurreny(types: any) {
    this.selectedCurrency = [];
    types.forEach(element => {
      this.selectedCurrency.push({
        Currency: element
      });
    });
  }


  selected: boolean;
  num = 0;
  myFunction() {
    this.num = this.num + 1;
    //console.log("num", this.num);
    // console.log("num", (this.num) % 2);
    var checkBox = document.getElementById("myCheck");
    //  console.log("checkBox", checkBox);
    if (checkBox && (this.num) % 2 === 1) {
      this.selected = false;
      this.registerForm.controls.notCheck.patchValue(false);
      // console.log("check", this.registerForm.controls.notCheck);
    }
    else if ((this.num) % 2 === 0) {
      this.selected = true;
      this.registerForm.controls.notCheck.patchValue(true);
    }
  }

  delete(i) {
    //  console.log("i", i);
    const arr = this.registerForm.get(
      `inviteVendors.${i}`
    ) as FormArray;
    //  console.log("arr", arr);
    // console.log("const", arr.value);
    // const index = arr.value.indexOf(i, 0);
    // console.log("index", index);
    if (i > -1) {
      arr.value.splice(i);
    }

    arr.removeAt(i);
  }


  SelectionNum(type: any) {
    this.selectedNum = [];
    type.forEach(element => {
      this.selectedNum.push({
        number: element
      });
    });
  }


  getInviteUser(registerForm) {
    //  console.log('approvalCycleForm', approvalCycleForm);
    return registerForm.controls.inviteVendors.controls;
  }

  addUser(i?) {
    const control = this.registerForm.get('inviteVendors') as FormArray;
    // console.log("control", control)
    control.push(this.userList(control.value.length + 1));
  }

  userList(i?) {
    return this.fb.group({
      userid: i ? i : 0,
      userEmail: ''
    });
  }


  SelectionSCat(type: any) {
    //console.log('type', type)
    this.selectSubcat = [];
    type.forEach(element => {
      this.selectSubcat.push({
        SubCatalogue: element
      });
    });
  }


  buildForm(register: Vendor) {
    this.registerForm = this.fb.group({
      name: [register.name, Validators.required],
      contactPerson: [register.contactPerson, Validators.required],
      companytype: ['', Validators.required],
      businessNature: ['', Validators.required],
      panNumber: ['', Validators.required],
      currency: ['', Validators.required],
      propreterName: [''],
      description: ['', Validators.required],
      otherservice: ['', Validators.required],
      catlogue: [register.catalogue, Validators.required],
      subcatlogue: [register.subCatalogue, Validators.required],
      isGstApplicable: ['', Validators.required],
      composiTaxableDealer: ['', Validators.required],
      registerdUnderMSME: ['', Validators.required],
      mMSMEregisterNumbaer: [''],
      dateOfRegistration: [''],
      adress1: ['', Validators.required],
      adress2: ['', Validators.required],
      locationType: ['', Validators.required],
      country: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      zipCode: ['', Validators.required],
      gSTNumber: ['', Validators.required],
      accountType: [register.VendorBankDetail, Validators.required],
      accNum: [register.VendorBankDetail, Validators.required],
      bankCode: [register.VendorBankDetail, Validators.required],
      bankName: [register.VendorBankDetail, Validators.required],
      accountHolderName: [register.VendorBankDetail, Validators.required],
      bankAdress: [register.VendorBankDetail, Validators.required],
      mSMEREGISTNUm: ['', Validators.required],
      primaryUSerEmail: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      notCheck: [register.notCheck],
      designation: ['', Validators.required],
      phoneNum: ['', Validators.required],
      password: ['', Validators.required],
      confirmPass: ['', Validators.required],
      inviteVendors: this.fb.array([]),
    });
  }

  onSubmit(register: Vendor) {


    if (register.notCheck || this.selected) {
      this.selected = true;
    }
    else {
      this.selected = false;
      console.log("register", register);
    }
  }
}
