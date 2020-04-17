import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'core/services/auth.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styles: [],
})
export class ToolbarComponent implements OnInit {
  title: string;
  userDisplayName = '';
  logo: any;
  displayPictureUrl: any;
  orgId = 0;
  userType: string;
  @Output() toggleSidenav = new EventEmitter<void>();
  @Output() toggleTheme = new EventEmitter<void>();
  @Output() toggleRightSidenav = new EventEmitter<void>();

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {
    const localObject = localStorage.getItem('currentUser');
    if (localObject != null) {
      const jsonData = JSON.parse(localObject);
      console.log('jsonData', jsonData);
      this.userType = jsonData.userType;
      this.displayPictureUrl = jsonData.displayPictureUrl;
      this.companyLogo();
    }
  }

  getTitle() {
    return (this.title = document.title);
  }
  ngOnInit() {
    if (localStorage.getItem('currentUser') != null) {
      this.authService.getDisplayUserName().subscribe(res => {
        this.userDisplayName = res;
      });
      this.authService.getOrgId().subscribe(res => {
        this.orgId = res;
      });
    }
  }
  navigateLink(row) {
    this.router.navigate([row.link]);
  }
  companyLogo() {
    // const myItem = JSON.parse(localStorage.getItem('appSetting'));
    // this.logo = myItem.buyerOrg.logoUrl;
  }
  isBuyer() {
    return (
      this.authService.getUserType() === 'Buyer' ||
      this.authService.getUserType() === 'SuperAdmin'
    );
  }
  hasAdminRole() {
    return (
      this.authService.getUserRoles().includes('Admin') ||
      this.authService.getUserRoles().includes('SuperAdmin')
    );
  }
  logout() {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['']);
    });
  }
  viewProfile() {
    if (this.userType === 'Supplier') {
      this.router.navigate(['supplier-orgs/users/list']);
    }
    if (this.userType === 'Buyer') {
      this.router.navigate(['settings/buyer-org-users/user/profile']);
    }
  }
  viewCompnayProfile() {
    if (this.userType === 'Supplier') {
      this.router.navigate(['supplier-orgs/details/']);
    }
    if (this.userType === 'Buyer') {
      this.router.navigate(['settings/buyer-orgs/company/profile']);
    }
  }
  openBottomSheet(): void {
  }
}
