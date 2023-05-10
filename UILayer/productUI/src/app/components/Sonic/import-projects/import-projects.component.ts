import { Component, EventEmitter, Output } from '@angular/core';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-import-projects',
  templateUrl: './import-projects.component.html',
  styleUrls: ['./import-projects.component.css']
})
export class ImportProjectsComponent {
  @Output() ChangeView = new EventEmitter<any>();
  fileToUpload: File | null = null;
  dragAreaClass:string;
  constructor(private service: SonicService) {
    this.dragAreaClass = "uploadDiv blackBorder lightGray";
  }
  goBack() {
    this.ChangeView.emit("storeview");
  }
  handleFileInput(files: any) {
    this.fileToUpload = files[0];
    this.uploadFileToActivity();
  }
  handleOneFile(file: any){
    this.fileToUpload = file.target.files.item(0);
    this.uploadFileToActivity();
  }
  uploadFileToActivity() {
    if (this.fileToUpload != null) {
      this.service.postFile(this.fileToUpload).subscribe(data => {
        // do something, if upload success
      }, error => {
        console.log(error);
      });
    }
  }
}
