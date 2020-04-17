import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ToastrService } from 'ngx-toastr';

import {
  SimpleConfirmationBoxComponent,
} from '../components/simple-confirmation-box/simple-confirmation-box.component';

@Injectable({
  providedIn: 'root',
})
export class UiService {
  constructor(
    private toastr: ToastrService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  showSuccess(message: string) {
    this.toastr.success(message, 'Success');
  }

  showInfo(message: string) {
    this.toastr.info(message, 'Info');
  }

  showWarning(message: string) {
    this.toastr.warning(message, 'Warning');
  }

  showError(message: string) {
    this.toastr.error(message, 'Error');
  }

  showToast(message: string, action = 'Close', config?: MatSnackBarConfig) {
    this.snackBar.open(
      message,
      action,
      config || {
        duration: 3000,
      }
    );
  }

  showConfirmationBox(options: { message: string; onYes: () => void; }) {
    if (this.dialog.openDialogs.length === 0) {
      const dialogRef = this.dialog.open(
        SimpleConfirmationBoxComponent,
        {
          width: '500px',
          data: {
            content: options.message,
            okText: 'Yes',
            cancelText: 'No'
          },
        }
      );
      dialogRef.afterClosed().subscribe(res => {
        if (res) {
          options.onYes();
        }
      });
    }
  }
}
