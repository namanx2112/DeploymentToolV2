import { Component, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { Fields } from 'src/app/interfaces/home-tab';

@Component({
  selector: 'app-dialog-controls',
  templateUrl: './dialog-controls.component.html',
  styleUrls: ['./dialog-controls.component.css']
})
export class DialogControlsComponent {
  fields: Fields[];
  needButton: boolean;
  themeClass: string;
  numberOfControlsInARow: number;
  controlValues: Dictionary<string>;
  SubmitLabel: string;
  readOnlyForm: boolean;
  onSubmit: any;
  onClose: any;
  title: string;
  dialogTheme: string;
  curBrandId: number;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    if (typeof data != 'undefined') {
      this.numberOfControlsInARow = data.numberOfControlsInARow;
      this.fields = data.fields;
      this.readOnlyForm = data.readOnlyForm;
      this.needButton = data.needButton;
      this.controlValues = data.controlValues;
      this.SubmitLabel = data.SubmitLabel;
      this.themeClass = data.themeClass;
      this.onSubmit = data.onSubmit;
      this.onClose = data.onClose;
      this.title = data.title;
      this.dialogTheme = data.dialogTheme;
      this.curBrandId = data.curBrandId;
    }
  }

  onSubmitClicked(tVal: any) {
    this.onSubmit(tVal);
  }

  onCloseClicked(ev: any){
    this.onClose(ev);
  }
}
