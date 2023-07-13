import { Component, EventEmitter, Output } from '@angular/core';
import { SonicService } from 'src/app/services/sonic.service';
import { SonicProjectExcel } from '../../../interfaces/sonic';

@Component({
  selector: 'app-import-projects',
  templateUrl: './import-projects.component.html',
  styleUrls: ['./import-projects.component.css']
})
export class ImportProjectsComponent {
  @Output() ChangeView = new EventEmitter<any>();
  excelData: SonicProjectExcel[];
  selectedItems: SonicProjectExcel[];
  fileToUpload: File | null = null;
  dragAreaClass: string;
  constructor(private service: SonicService) {
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
        this.service.UploadStore({ fileToUpload: this.fileToUpload }).subscribe((data: SonicProjectExcel[]) => {
          this.excelData = data;
          // do something, if upload success
        }, error => {
          console.log(error);
        });
      }
      else
      alert("Please upload valid xlsx file only!");
    }
  }

  SelectionChange(items: SonicProjectExcel[]) {
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
