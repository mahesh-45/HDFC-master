import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

const credentialsKey = 'currentUser';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) { }

  login(loginData: User): Observable<any> {
    this.logout();
    const href = '/api/users/login';
    return this.http.post<any>(href, loginData).pipe(
      tap(function (data) {
        const res = JSON.parse(data);
        if (res.token) {
          const storage = loginData.rememberMe ? localStorage : sessionStorage;
          storage.setItem(credentialsKey, data);
        }
        return res;
      })
    );
  }

  loginWithGoogle(user: any): Observable<any> {
    this.logout();
    const href = '/api/users/external-google';
    return this.http.post<any>(href, user).pipe(
      tap(function (data) {
        console.log(data);
        const res = JSON.parse(data);
        if (res.token) {
          const storage = true ? localStorage : sessionStorage;
          storage.setItem(credentialsKey, data);
        }
        return res;
      })
    );
  }

  twoFactorLogin(user: User): Observable<any> {
    return this.http.post('/api/users/TwoFactorLogin', user);
  }


  twoFactorLoginValidate(loginData: User): Observable<any> {
    this.logout();
    const href = '/api/users/TwoFactorLoginValidate';
    return this.http.post<any>(href, loginData).pipe(
      tap(function (data) {
        const res = JSON.parse(data);
        if (res.token) {
          const storage = loginData.rememberMe ? localStorage : sessionStorage;
          storage.setItem(credentialsKey, data);
        }
        return res;
      })
    );
  }

  validatePing(code: string): Observable<any> {
    this.logout();
    const href = '/api/users/ValidatePing?t=' + code;
    return this.http.get<any>(href);
  }

  pingFederateHandshake(code: string, ): Observable<any> {
    this.logout();
    const href = '/api/users/ping?t=' + code;
    return this.http.get<any>(href).pipe(
      tap(function (data) {
        console.log(data);
        const res = JSON.parse(data);
        if (res.token) {
          const storage = true ? localStorage : sessionStorage;
          storage.setItem(credentialsKey, data);
        }
        return res;
      })
    );
  }

  pingDelegate(code: string, user: string): Observable<any> {
    this.logout();
    const href = '/api/users/pingDelegate?t=' + code + '&email=' + user;
    return this.http.get<any>(href).pipe(
      tap(function (data) {
        console.log(data);
        const res = JSON.parse(data);
        if (res.token) {
          const storage = true ? localStorage : sessionStorage;
          storage.setItem(credentialsKey, data);
        }
        return res;
      })
    );
  }

  guestLogin(loginData: User): Observable<any> {
    this.logout();
    const href = '/api/users/guestLogin';
    return this.http.post<any>(href, loginData);
  }

  guestUserStatus(loginData: User): Observable<any> {
    const href = '/api/users/UserStatus';
    return this.http.post<any>(href, loginData);
  }

  logout(): Observable<boolean> {
    sessionStorage.removeItem(credentialsKey);
    localStorage.removeItem(credentialsKey);
    return of(true);
  }

  getUserInfo(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials);
  }

  isLogin() {
    if (localStorage.getItem(credentialsKey) || sessionStorage.getItem(credentialsKey)) {
      return true;
    }
    return false;
  }
  isGuest() {
    if (localStorage.getItem('userType') === 'Guest') {
      return true;
    }
    return false;
  }

  getToken() {
    const savedCredentials = this.getUser();
    return savedCredentials && savedCredentials['token'];
  }

  getUserRole(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['roles']);
  }

  getLandingPageUrl(): string {
    const savedCredentials = this.getUser();
    return savedCredentials['landingPage'];
  }

  getDisplayUserName(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['userDisplayName']);
  }

  getDefaultLang(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['lang']);
  }

  getEnabledModules(): Observable<any> {
    const savedCredentials = this.getUser();

    console.log('savedCredentials', savedCredentials);
    return of(savedCredentials['modules']);
  }

  getEnabledPages(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['pages']);
  }

  getOrgId(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['orgId']);
  }

  getEnabledSections(): Observable<any> {
    const savedCredentials = this.getUser();
    return of(savedCredentials['sections']);
  }
  isEnabledSections(sectionKey: any): Observable<boolean> {
    // console.warn(sectionKey);
    if (this.getUserRoles().includes('SuperAdmin')) {
      return of(true);
    }
    const savedCredentials = this.getUser();
    return of(savedCredentials['sections'].includes(sectionKey));
  }

  isEnabledPageUrl(pageUrl: any): boolean {
    const savedCredentials = this.getUser();
    console.log(savedCredentials['pages'].includes(pageUrl));
    if (pageUrl.includes(';')) {
      pageUrl = pageUrl.split(';')[0];
    }

    pageUrl = this.removeNumbersFromUrl(pageUrl);
    console.error(pageUrl);
    return savedCredentials['pages'].some(s => s.includes(pageUrl));
  }

  private removeNumbersFromUrl(url) {
    const splitted = url.split('/');
    const result = [];
    for (let i = splitted.length - 1; i > 0; i--) {
      if (isNaN(splitted[i])) {
        result.push(splitted[i]);
      }
    }

    return (
      '/' +
      result
        .reverse()
        .toString()
        .split(',')
        .join('/')
    );
  }

  isEnabledModule(moduleKey: any): boolean {
    if (!environment.production) {
      return true;
    }
    const savedCredentials = this.getUser();
    return savedCredentials['modules'].includes(moduleKey);
  }

  // djskd(element) {
  //   // checks whether an element is even
  //   return element.includes('items/list/(detail:details');
  // }
  // even = function(element) {
  //   return element.includes('items/list/(detail:details');
  // };
  getUserType() {
    const savedCredentials = this.getUser();
    if (this.isLogin()) {
      return savedCredentials['userType'];
    } else {
      return false;
    }
  }

  getUserRoles(): any[] {
    const savedCredentials = this.getUser();
    if (this.isLogin()) {
      return savedCredentials['roles'];
    } else {
      return [];
    }
  }

  sendPasswordResetLink(userName: string): Observable<any> {
    return this.http.post<any>(`/api/Users/SendPasswordResetLink/` + userName, null);
  }

  resetPassword(user: User): Observable<User> {
    return this.http.post<User>(`/api/Users/ResetPassword/`, user);
  }

  private getUser() {
    const savedCredentials =
      sessionStorage.getItem(credentialsKey) || localStorage.getItem(credentialsKey);
    return JSON.parse(savedCredentials);
  }
  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`/api/Users/` + userId);
  }
}

export interface User {
  email: string;
  password: string;
  rememberMe: boolean;
}
export class TempUser {
  id?: number;
  UserType: string;
  EntityType: string;
  Email: string;
  EntityId: number;
  Status?: string;
  password: string;
}
