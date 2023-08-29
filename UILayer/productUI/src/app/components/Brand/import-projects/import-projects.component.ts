import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { ProjectExcel } from '../../../interfaces/store';
import { BrandModel } from 'src/app/interfaces/models';

@Component({
  selector: 'app-import-projects',
  templateUrl: './import-projects.component.html',
  styleUrls: ['./import-projects.component.css']
})
export class ImportProjectsComponent {
  @Output() ChangeView = new EventEmitter<any>();
  @Input()
  set curBrand(val: BrandModel) {
    this._curBrand = val;
  }
  _curBrand: BrandModel;
  excelData: ProjectExcel[];
  selectedItems: ProjectExcel[];
  fileToUpload: File | null = null;
  dragAreaClass: string;
  constructor(private service: ExStoreService) {
    this.dragAreaClass = "uploadDiv blackBorder lightGray";
    this.excelData = [];
    this.selectedItems = [];
  }
  goBack() {
    this.ChangeView.emit("dashboard");
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
        this.service.UploadStore(this.fileToUpload, this._curBrand.aBrandId ).subscribe((data: ProjectExcel[]) => {
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
      this.service.CreateNewStores(this.selectedItems).subscribe((x: string) => {
        alert("Store Created Successfully you can search store");
        this.ChangeView.emit("dashboard");
      });
    }
  }
}
