import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AppSettingService {
  constructor() { }

  getDefaultLanguage() {
    const appSetting =
      localStorage.getItem('appSetting') || sessionStorage.getItem('appSetting');
    return JSON.parse(appSetting).defaultLanguage;
  }

  getDefaultAppName() {
    const appSetting =
      localStorage.getItem('appSetting') || sessionStorage.getItem('appSetting');
    return JSON.parse(appSetting).appName;
  }

  getUrl() {
    const appSetting =
      localStorage.getItem('appSetting') || sessionStorage.getItem('appSetting');
    return JSON.parse(appSetting).url;
  }
}
