import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { DateChangeBody, DateChangeNotificationBody, DateChangeNotitication, DateChangePOOption, StoreSearchModel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { StoreService } from 'src/app/services/store.service';
import { DateChangeRevisedPOComponent } from '../date-change-revised-po/date-change-revised-po.component';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-render-date-change-template',
  templateUrl: './render-date-change-template.component.html',
  styleUrls: ['./render-date-change-template.component.css']
})
export class RenderDateChangeTemplateComponent {
  curStore: StoreSearchModel;
  onSubmit: any;
  ckConfig: any;
  allItems: DateChangeNotitication[];
  curStep: number;
  dateChangeBody: DateChangeNotificationBody;
  selection = new SelectionModel<any>(true, []);
  constructor(public dialogRef: MatDialogRef<RenderDateChangeTemplateComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private service: StoreService,
    private commonService: CommonService, private dialog: MatDialog) {
    this.ckConfig = this.commonService.GetCKEditorConfig("256px");
    this.curStore = data.curStore;
    this.onSubmit = data.onSubmit;
    this.GetDateChangeTable();
    this.curStep = 1;
  }

  GetDateChangeTable() {
    this.service.GetDateChangeTable(this.curStore.nStoreId).subscribe(x => {
      this.allItems = x;
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.allItems.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.selection.selected);
  }

  checkboxLabel(row?: DateChangeNotitication): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
  }

  GetDateChangeBody() {
    let request = {
      nStoreId: this.curStore.nStoreId,
      lstItems: this.allItems
    }
    this.service.GetDateChangeBody(request).subscribe(x => {
      this.dateChangeBody = x;
      this.curStep++;
    });
  }

  SendRequest() {
    this.service.SendDateChangeNotification(this.dateChangeBody).subscribe(x => {
      if (typeof x == 'string')
        alert(x);
      else {
        this.onSubmit();
        let tItems: DateChangePOOption[] = x;
        const dialogConfig = new MatDialogConfig();
        let dialogRef: any;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '50%';
        //dialogConfig.height = '100%';
        dialogConfig.data = {
          curStore: this.curStore,
          tItems: tItems,
          onSubmit: function (data: any) {
            dialogRef.close();
          }
        };
        dialogRef = this.dialog.open(DateChangeRevisedPOComponent, dialogConfig);
      }
    })
  }

  cannotMove() {
    return (this.allItems.findIndex(x => x.isSelected) == -1)
  }

  cannotSend() {
    let cannot = false;
    if (this.dateChangeBody.tContent == '' || this.dateChangeBody.tTo == '' || this.dateChangeBody.tSubject == '')
      cannot = true;
    if (!cannot) {
      let eIds = this.dateChangeBody.tTo.split(";");
      for (var indx in eIds) {
        if (!CommonService.isValidEmail(eIds[indx].trim())) {
          cannot = true;
          break;
        }
      }
      if (!cannot && this.dateChangeBody.tCC != null && this.dateChangeBody.tCC != '') {
        let eIds = this.dateChangeBody.tCC.split(";");
        for (var indx in eIds) {
          if (!CommonService.isValidEmail(eIds[indx].trim())) {
            cannot = true;
            break;
          }
        }
      }
    }
    return cannot;
  }

  MoveNext() {
    this.GetDateChangeBody();
  }

  MoveBack() {
    this.curStep--;
  }

  onCancelUserDialog(): void {
    this.dialogRef.close();
  }
}
