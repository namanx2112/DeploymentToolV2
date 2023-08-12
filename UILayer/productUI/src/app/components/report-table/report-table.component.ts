import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ReportModel } from 'src/app/interfaces/analytics';
import { FieldType } from 'src/app/interfaces/home-tab';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-report-table',
  templateUrl: './report-table.component.html',
  styleUrls: ['./report-table.component.css'],
})
export class ReportTableComponent {
  tReport: ReportModel;
  dataSource: MatTableDataSource<any>;// = new MatTableDataSource(ELEMENT_DATA);
  constructor(private _liveAnnouncer: LiveAnnouncer, private analyticsService: AnalyticsService) {
    this.getReport();
  }

  @ViewChild(MatSort) sort: MatSort;

  getReport() {
    this.analyticsService.GetReport(1).subscribe(x => {
      this.tReport = x;
      this.loadTable();
    });
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (colName.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  loadTable() {
    this.dataSource = new MatTableDataSource(this.tReport.data);
  }

  announceSortChange(sortState: Sort) {
    // This example uses English messages. If your application supports
    // multiple language, you would internationalize these strings.
    // Furthermore, you can customize the message to add additional
    // details about the values being sorted.
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}
