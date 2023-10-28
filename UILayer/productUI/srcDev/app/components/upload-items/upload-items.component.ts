import { Component, EventEmitter, Input, Output, Inject } from '@angular/core';
import { ExStoreService } from 'src/app/services/ex-store.service';
// import { ProjectExcel } from '../../../interfaces/store';
import { BrandModel } from 'src/app/interfaces/models';
import { HomeTab } from 'src/app/interfaces/home-tab';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-upload-items',
  templateUrl: './upload-items.component.html',
  styleUrls: ['./upload-items.component.css']
})
export class UploadItemsComponent {
  _curTab: HomeTab;
  _curBrandId: number;
  // excelData: ProjectExcel[];
  selectedItems: any[];
  fileToUpload: File | null = null;
  dragAreaClass: string;
  constructor(private service: ExStoreService, @Inject(MAT_DIALOG_DATA) public data: any) {
    if (typeof data != undefined) {
      this._curBrandId = data.curBrandId;
      this._curTab = data.curTab;
    }
    this.dragAreaClass = "uploadDiv blackBorder lightGray";
    // this.excelData = [];
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
        // this.service.UploadStore(this.fileToUpload, this._curBrand.aBrandId ).subscribe((data: ProjectExcel[]) => {
        //  // this.addRecords(data);
        //   // do something, if upload success
        // }, error => {
        //   console.log(error);
        // });
      }
      else
        alert("Please upload valid xlsx file only!");
    }
  }

  UploadItems() {

  }

  // addRecords(data: ProjectExcel[]) {
  //   this.excelData = [];
  //   for (var indx in data) {
  //     data[indx].nBrandId = this._curBrand.aBrandId;
  //     this.excelData.push(data[indx]);
  //   }
  // }

  // SelectionChange(items: ProjectExcel[]) {
  //   this.selectedItems = items;
  // }

  // CreateNewStores() {
  //   if (this.selectedItems.length > 0) {
  //     this.service.CreateNewStores(this.selectedItems).subscribe((x: string) => {
  //       alert("Store Created Successfully you can search store");
  //       this.ChangeView.emit("dashboard");
  //     });
  //   }
  // }
}
