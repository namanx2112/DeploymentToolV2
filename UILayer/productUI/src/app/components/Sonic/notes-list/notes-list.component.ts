import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.css']
})
export class NotesListComponent {
  curTab: HomeTab;
  constructor(private service: SonicService, private dialog: MatDialog) {
    this.curTab = this.service.GetNotesTab(TabInstanceType.Table);
  }

  rowClicked(row: any) {
  }

  createNotes() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: "Create Note",
      fields: this.curTab.fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: [],
      SubmitLabel: "Post",
      onSubmit: function (data: any) {
        let clickedVal = data.values;
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
  }
}
