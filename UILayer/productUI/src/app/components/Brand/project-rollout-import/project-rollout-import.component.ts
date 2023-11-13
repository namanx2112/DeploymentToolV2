import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, EventEmitter, Input, Output, Inject } from '@angular/core';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { ProjectExcel } from '../../../interfaces/store';
import { BrandModel } from 'src/app/interfaces/models';

@Component({
  selector: 'app-project-rollout-import',
  templateUrl: './project-rollout-import.component.html',
  styleUrls: ['./project-rollout-import.component.css']
})
export class ProjectRolloutImportComponent {
  _curBrand: BrandModel;
  excelData: ProjectExcel[];
  selectedItems: ProjectExcel[];
  fileToUpload: File | null = null;
  dragAreaClass: string;
  projectType: string;
  onSubmit: any;
  constructor(private service: ExStoreService, @Inject(MAT_DIALOG_DATA) public data: any) {
    if (typeof data != 'undefined') {
      this.projectType = data.projectType;
      this._curBrand = data.curBrand;
      this.onSubmit = data.onSubmit;
    }
    this.dragAreaClass = "uploadDiv blackBorder lightGray";
    this.excelData = [];
    this.selectedItems = [];
  }
  handleFileInput(files: any) {
    this.fileToUpload = files[0];
    this.uploadFileToActivity();
  }
  handleOneFile(file: any) {
    this.fileToUpload = file.target.files.item(0);
    this.uploadFileToActivity();
  }
  uploadFileToActivity() {
    if (this.fileToUpload != null) {
      if (this.fileToUpload.name.toLowerCase().endsWith("xlsx")) {
        this.service.UploadStore(this.fileToUpload, this._curBrand.aBrandId, this.projectType).subscribe((data: ProjectExcel[]) => {
          this.addRecords(data);
          // do something, if upload success
        }, error => {
          console.log(error);
        });
      }
      else
        alert("Please upload valid xlsx file only!");
    }
  }

  addRecords(data: ProjectExcel[]) {
    this.excelData = [];
    for (var indx in data) {
      data[indx].nBrandId = this._curBrand.aBrandId;
      this.excelData.push(data[indx]);
    }
  }

  SelectionChange(items: ProjectExcel[]) {
    this.selectedItems = items;
  }

  CreateNewStores() {
    if (this.selectedItems.length > 0) {
      this.onSubmit(this.projectType, this.selectedItems);
    }
  }
}
