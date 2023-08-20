import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { NotesService } from 'src/app/services/notes.service';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { Dictionary } from 'src/app/interfaces/commons';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.css']
})
export class NotesListComponent {
  curTab: HomeTab;
  refreshFlag: Date;
  curStore: any;
  searchFields: Dictionary<string> = {};
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private service: NotesService, private dialog: MatDialog) {
    if (typeof data != 'undefined')
      this.curStore = data.curStore;
    this.searchFields = { "nStoreId": this.curStore.nStoreId.toString() };
    this.curTab = this.service.GetNotesTab(TabInstanceType.Table);
  }

  rowClicked(row: any) {
  }

  createNotes() {
    let cThis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    let pFillData = {
      "nStoreID": this.curStore.nStoreId,
      "nProjectID": (typeof this.curStore.nProjectId != 'undefined') ? this.curStore.nProjectId : this.curStore.lstProjectsInfo[this.curStore.lstProjectsInfo.length - 1].nProjectId
    };

    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: "Create Note",
      fields: this.curTab.fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: pFillData,
      SubmitLabel: "Post",
      curBrandId: this.curStore.nBrandId,
      onSubmit: function (data: any) {
        cThis.service.Create(data.value).subscribe((x: any) => {
          cThis.refreshFlag = new Date();
          dialogRef.close();
        });
      },
      onClose: function (ev: any) {
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
  }
}
