import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { environment } from 'environments/environment';

import { AuthService } from '../services/auth.service';
import { UiService } from '../services/ui.service';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    private router: Router,
    private uiService: UiService,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log(state.url);
    return true;
    // return this.chekUser(state.url);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log(state.url);
    return true;
    //return this.chekUser(state.url);
  }

  canLoad(route: Route): boolean {
    // console.log(route.path);
    // return true;
    return this.authService.isEnabledModule(route.path);
  }

  private chekUser(url): boolean {
    console.error(url);
    if (!environment.production) {
      return true;
    }
    const userType = this.authService.getUserType();
    const isLogin = this.authService.isLogin();

    if (userType === 'SuperAdmin' && isLogin) {
      return true;
    } else if (isLogin) {
      // return false;
      // return true;
      // return this.authService.();
      const isRouteEnabled = this.authService.isEnabledPageUrl(url);
      if (isRouteEnabled) {
        return true;
      } else {
        this.uiService.showError('Unauthorized: Access is denied');
        // this.router.navigate(['/'], { queryParams: { returnUrl: state.url } });
        return false;
      }
      // this.router.navigate(['/client/dashboard']);
      // this.router.navigateByUrl('/');
      // return true;
      // return this.authService.getUserType() === 'Buyer';
    } else {
      this.router.navigate(['/']);
      return false;
    }
  }
}
