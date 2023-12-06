import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportEditorModel, ReportField, ReportFolder } from 'src/app/interfaces/report-generator';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';
import { FieldSelectionComponent } from '../field-selection/field-selection.component';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.css']
})
export class ReportEditorComponent {
  @Input()
  set request(val: any) {
    this.curModel = val.curModel;
    this.curBrand = val.curBrand;
    this.initUI();
  }

  @Output()
  actionPerformed = new EventEmitter<any>();

  curBrand: BrandModel;
  curModel: ReportEditorModel;
  allFolders: ReportFolder[] = [];
  allFields: ReportField[] = [];
  constructor(private rgService: ReportGeneratorService, private dialog: MatDialog) {

  }

  initUI() {
    this.getMyFolders();
  }

  goBack() {

  }

  getMyFolders() {
    this.rgService.GetMyFolders(this.curBrand.aBrandId).subscribe(x => {
      this.allFolders = x;
      if (this.curModel.nFolderId == -1)
        this.curModel.nFolderId = this.allFolders[0].aFolderId;
    });
  }

  fieldSelection() {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    dialogConfig.height = '70%';
    let openDialog = function () {
      dialogConfig.data = {
        allFields: cthis.allFields,
        srtClmn: cthis.curModel.srtClmn,
        spClmn: cthis.curModel.spClmn,
        aferSave: function (data: any) {
          cthis.curModel.srtClmn = data.srtClmn;
          cthis.curModel.spClmn = data.spClmn;
          dialogRef.close();
        },
        onClose: function (ev: any) {
          dialogRef.close();
        },
        themeClass: "grayWhite",
        dialogTheme: "lightGrayWhiteTheme"
      };
      dialogRef = cthis.dialog.open(FieldSelectionComponent, dialogConfig);
    }
    if (this.allFields.length == 0) {
      this.rgService.GetReportFields(this.curBrand.aBrandId).subscribe(x => {
        this.allFields = x;
        openDialog();
      });
    }
    else
      openDialog();
  }

  compareDropDown(o1: any, o2: any) {
    if (o1 == o2)
      return true;
    else return false
  }

  buttonClicked(ev: any) {
    if (ev.action == "submit") {
      this.curModel.conditions = ev.rows;
      if (!this.cantSubmit()) {
        this.rgService.EditReport(this.curModel).subscribe(x => {
          this.actionPerformed.emit(x);
        });
      }
      else
        alert("Please enter a name for report");
    }
    else
      this.actionPerformed.emit();
  }

  cancel() {
    this.actionPerformed.emit();
  }

  cantSubmit() {
    let can = false;
    if (this.curModel.tReportName == "")
      can = true;
    return can;
  }
}
