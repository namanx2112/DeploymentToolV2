import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportEditorModel } from 'src/app/interfaces/report-generator';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';

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
  constructor(private rgService: ReportGeneratorService) {

  }

  initUI() {
  }

  goBack() {

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
