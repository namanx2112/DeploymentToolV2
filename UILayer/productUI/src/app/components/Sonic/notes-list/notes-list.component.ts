import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { NotesService } from 'src/app/services/notes.service';
import { StoreSearchModel } from 'src/app/interfaces/sonic';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.css']
})
export class NotesListComponent {
  curTab: HomeTab;
  refreshFlag: Date;
  curStore: StoreSearchModel;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private service: NotesService, private dialog: MatDialog) {
    this.curTab = this.service.GetNotesTab(TabInstanceType.Table);
    if (typeof data != 'undefined')
      this.curStore = data.curStore;
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
      "nProjectID": this.curStore.lstProjectsInfo[this.curStore.lstProjectsInfo.length - 1].nProjectId
    };

    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: "Create Note",
      fields: this.curTab.fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: pFillData,
      SubmitLabel: "Post",
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
