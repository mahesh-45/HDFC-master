import { Component, OnInit } from '@angular/core';
import { AuthService } from 'core/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  styles: [],
})
export class NavbarComponent implements OnInit {
  expansions = {};
  path = '';
  isHelpDesk: boolean;
  vendorStatus = false;
  isRequester: boolean;
  isBuyyer: boolean;
  isBuyyerManager: boolean;
  userRoleSData: any;
  constructor(private authService: AuthService) {
    this.path = window.location.pathname;
    console.log('url', this.path);
    this.check();
  }

  ngOnInit() {
    if (this.authService.getUserType() === 'Supplier') {
      this.vendorStatus = true;
    }
    this.getroles();
  }
  isBuyer() {
    return (
      this.authService.getUserType() === 'Buyer' ||
      this.authService.getUserType() === 'SuperAdmin'
    );
  }

  isEnabledModule(moduleKey: any) {
    // if (!this.authService.isGuest()) {
    return (
      this.authService.isEnabledModule(moduleKey) ||
      this.authService.getUserRoles().includes('SuperAdmin')
    );
    // }
  }

  hasAdminRole() {
    return (
      this.authService.getUserRoles().includes('Admin') ||
      this.authService.getUserRoles().includes('SuperAdmin')
    );
  }

  openBottomSheet(): void {
  }

  check() {
    if (this.path.includes('help-desk')) {
      this.isHelpDesk = true;
    } else {
      this.isHelpDesk = false;
    }
  }
  getroles() {

  }
}
