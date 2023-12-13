import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportEditorModel, ReportField, ReportFolder } from 'src/app/interfaces/report-generator';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';
import { FieldSelectionComponent } from '../field-selection/field-selection.component';
import { FilterTableComponent } from '../filter-table/filter-table.component';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  fieldsByGroup: any;
  columnsConfigured: string[] = [];
  @ViewChild('conditionTable') conditionTable: FilterTableComponent;
  constructor(private rgService: ReportGeneratorService, private dialog: MatDialog, private _snackBar: MatSnackBar) {

  }

  initUI() {
    this.getMyFolders();
    this.rgService.GetReportFields(this.curBrand.aBrandId).subscribe(x => {
      this.allFields = x;
      this.fieldsByGroup = this.rgService.getFieldByGroup(x);
      this.updateConfiguredReport();
    });
  }

  goBack() {

  }

  updateConfiguredReport() {
    this.columnsConfigured = [];
    for (var indx in this.curModel.spClmn) {
      let tField = this.allFields.find(x => x.aFieldID == this.curModel.spClmn[indx].nFieldID);
      if (tField)
        this.columnsConfigured.push(tField.tFieldName);
    }
  }

  getMyFolders() {
    this.rgService.GetMyFolders(this.curBrand.aBrandId).subscribe(x => {
      this.allFolders = x;
      if (this.curModel.nFolderId == -1)
        this.curModel.nFolderId = this.allFolders[0].aFolderId;
    });
  }

  showAlert(message: string) {
    this._snackBar.open(message)._dismissAfter(4000);
  }

  // Called from report Generator
  public FieldSelection() {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    dialogConfig.height = '70%';
    let openDialog = function () {
      dialogConfig.data = {
        allFields: cthis.allFields,
        fieldsByGroup: cthis.fieldsByGroup,
        srtClmn: cthis.curModel.srtClmn,
        spClmn: cthis.curModel.spClmn,
        aferSave: function (data: any) {
          cthis.curModel.srtClmn = data.srtClmn;
          cthis.curModel.spClmn = data.spClmn;
          cthis.updateConfiguredReport();
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
    openDialog();
  }

  compareDropDown(o1: any, o2: any) {
    if (o1 == o2)
      return true;
    else return false
  }

  public ReportSaveCall(preview: boolean) {
    if (!this.cantSubmit()) {
      let rows = this.conditionTable.GetConditionRows();
      if (rows.length > 0) {
        this.curModel.conditions = rows;
        this.rgService.EditReport(this.curModel).subscribe(x => {
          if (typeof x == 'object') {
            if (this.curModel.aReportId == 0)
              alert("Report " + x.tReportName + " created successfully.");
            else
              alert("Report " + x.tReportName + " updated successfully.");
            if (this.curModel.aReportId == 0 && typeof this.curModel.shareRequest != 'undefined') {

              this.curModel.shareRequest.reportIds = [x.aReportId]
              this.rgService.ShareReport(this.curModel.shareRequest).subscribe(sResp => {
                this.actionPerformed.emit(
                  {
                    postCall: (preview) ? "previewReport" : "",
                    reportModel: x
                  }
                );
              })
            }
            else
              this.actionPerformed.emit({
                postCall: (preview) ? "previewReport" : "",
                reportModel: x
              });
          }
          else if (typeof x == 'string')
            alert(x);
        });
      }
    }
  }

  cantSubmit() {
    let cant = false;
    if (this.curModel.tReportName == "") {
      cant = true;
      this.showAlert("Please enter a name for report");
    }
    else if (this.curModel.spClmn.length == 0) {
      cant = true;
      this.showAlert("Please select at least one column for this report using 'Column options'");
    }
    return cant;
  }
}
