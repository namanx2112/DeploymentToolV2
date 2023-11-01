import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MyReportModel } from 'src/app/interfaces/commons';
import { BrandModel } from 'src/app/interfaces/models';
import { AnalyticsService } from 'src/app/services/analytics.service';

@Component({
  selector: 'app-saved-reports',
  templateUrl: './saved-reports.component.html',
  styleUrls: ['./saved-reports.component.css']
})
export class SavedReportsComponent {

  @Input()
  set curBrand(val: BrandModel) {
    this._curBrand = val;
    this.getMyReports();
  }
  @Output() ChangeView = new EventEmitter<any>();
  _curBrand: BrandModel;
  myReports: MyReportModel[];
  constructor(private analyticsService: AnalyticsService) {

  }

  getMyReports() {
    this.analyticsService.GetSavedReportsForMe(this._curBrand.aBrandId).subscribe(x => {
      this.myReports = x;
    });
  }

  openReport(item: MyReportModel) {
    let request = { reportId: item.aReportId, tParam1: this._curBrand.aBrandId.toString() }
    this.ChangeView.emit({ viewName: "viewreport", request: request, tParam: "", fromView: "savedreports" });
  }
}
