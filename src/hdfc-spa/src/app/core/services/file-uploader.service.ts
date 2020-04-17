import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UiService } from './ui.service';

@Injectable({
  providedIn: 'root',
})
export class FileUploaderService {
  constructor(private http: HttpClient, private uiService: UiService) {}
  uploadFiles(fileList: FileList) {
    if (fileList === undefined || fileList === null || fileList.length === 0) {
      this.uiService.showError('No Files Detected');
      return;
    }

    const formData = new FormData();
    Array.from(fileList).forEach((file: File) => {
      // 5242880 is 5 mb in binary bytes
      if (file.size > 5242880) {
        this.uiService.showError('Size limit');
      }
      formData.append('files', file);
    });
    return this.http.post<any>(`/api/Upload`, formData, {
      headers: new HttpHeaders().set('Accept', 'application/json'),
    });
  }

  // bytesToSize(bytes) {
  //   const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
  //   if (bytes === 0) {
  //     return '0 Byte';
  //   }
  //   const i = Math.floor(Math.log(bytes) / Math.log(1024));
  //   return Math.round(bytes / Math.pow(1024, i)) + ' ' + sizes[i];
  // }
}
