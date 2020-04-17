import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  template: `

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
export class SimpleConfirmationBoxComponent {
  constructor(
    public dialogRef: MatDialogRef<SimpleConfirmationBoxComponent, Boolean>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }
}
