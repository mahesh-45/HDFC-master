import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'core/services/auth.service';
import { UiService } from 'core/services/ui.service';
import { EmailValidation, PasswordValidation } from 'shared/miscellaneous/validations';


declare const gapi: any;
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loginError = '';
  redirectUrl;
  logo: any;
  hide = true;
  show = true;


  public auth2: any;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private element: ElementRef,
    private uiService: UiService
  ) {
    this.activatedRoute.queryParams.subscribe(params => {
      this.redirectUrl = params.returnUrl;
    });
    this.companyLogo();
  }

  ngOnInit() {
    if (this.authService.isLogin()) {
      // this.router.navigate([this.authService.getLandingPageUrl()]);
      this.router.navigate(['/masters/countries']);
    }
    this.buildLoginForm();
  }

  companyLogo() {
    // const myItem = JSON.parse(localStorage.getItem('appSetting'));
    // this.logo = myItem.buyerOrg.logoUrl;
  }

  buildLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', EmailValidation],
      password: ['', PasswordValidation],
      rememberMe: true
    });
  }

  login(submittedForm: FormGroup) {
    this.authService.login(submittedForm.value).subscribe(res => {
      const tokenResponse = JSON.parse(res);
      // this.router.navigate([tokenResponse.landingPage]);
      this.router.navigate(['/masters/countries']);
    });
  }


}
