import { Component, EventEmitter, Input, Output } from '@angular/core';
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
    this.curFolder = val.curFolder;
    this.curBrand = val.curBrand;
    this.initUI();
  }

  @Output()
  actionPerformed = new EventEmitter<any>();

  curBrand: BrandModel;
  curFolder: ReportFolder;
  constructor(private rgService: ReportGeneratorService) {

  }

  initUI() {
  }

  goBack() {

  }

  submitMe() {
    this.rgService.EditFolder(this.curFolder).subscribe(x => {
      this.actionPerformed.emit(x);
    });
  }

  cancel() {
    this.actionPerformed.emit();
  }

  cantSubmit() {
    let can = false;
    if (this.curFolder.tFolderName != "")
      can = true;
    return true;
  }
}
