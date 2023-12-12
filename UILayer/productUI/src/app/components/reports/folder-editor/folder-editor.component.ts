import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportFolder } from 'src/app/interfaces/report-generator';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';

@Component({
  selector: 'app-folder-editor',
  templateUrl: './folder-editor.component.html',
  styleUrls: ['./folder-editor.component.css']
})
export class FolderEditorComponent {

  @Input()
  set request(val: any) {
    this.curModel = val.curModel;
    this.curBrand = val.curBrand;
    this.initUI();
  }

  @Output()
  actionPerformed = new EventEmitter<any>();

  curBrand: BrandModel;
  curModel: ReportFolder;
  constructor(private rgService: ReportGeneratorService, private _snackBar: MatSnackBar) {

  }

  initUI() {
  }

  goBack() {

  }

  showAlert(message: string) {
    this._snackBar.open(message)._dismissAfter(4000);
  }

  setType(e: any) {
    if (e.checked)
      this.curModel.nFolderType = 1;
    else
      this.curModel.nFolderType = 0;
  }

  public SaveFolder() {
    if (!this.cantSubmit()) {
      this.rgService.EditFolder(this.curModel).subscribe(x => {
        this.actionPerformed.emit(x);
      });
    }
  }

  cancel() {
    this.actionPerformed.emit();
  }

  cantSubmit() {
    let can = false;
    if (this.curModel.tFolderName == ""){
      this.showAlert("Please enter a name to this folder");
      can = true;
    }
    return can;
  }
}
