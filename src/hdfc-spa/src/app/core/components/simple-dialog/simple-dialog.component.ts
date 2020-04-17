import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  template: `
    <div class="mat-dialog-header">
      <h2 mat-dialog-title>{{ data.title }}</h2>
      <button mat-icon-button mat-dialog-close align="end">
        <mat-icon>close</mat-icon>
      </button>
    </div>
    <mat-dialog-content class="mat-typography">
      <p>{{ data.content }}</p>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button
        mat-raised-button
        color="primary"
        class="mr-2"
        color="primary"
        cdkFocusInitial
        [mat-dialog-close]="true"
      >
        {{ data.okText }}
      </button>
      <button
        mat-stroked-button
        color="primary"
        [mat-dialog-close]="false"
        *ngIf="data.cancelText"
      >
        {{ data.cancelText }}
      </button>
    </mat-dialog-actions>
  `,
})
export class SimpleDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<SimpleDialogComponent, Boolean>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }
}
