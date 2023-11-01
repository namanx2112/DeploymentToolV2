import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ReportModel } from 'src/app/interfaces/analytics';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType } from 'src/app/interfaces/home-tab';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-report-table',
  templateUrl: './report-table.component.html',
  styleUrls: ['./report-table.component.css'],
})
export class ReportTableComponent implements OnInit {
  @Input()
  set reportParam(val: any) {
    if (typeof val.request.tProjectIDs != 'undefined')
      this.getReport(val.request.reportId, val.request.tProjectIDs, val.tParam);
    else
      this.getReport(val.request.reportId, val.request.tParam1, val.tParam);
    this._nBrandId = val.nBrandId;
    this._fromView = val.fromView;
  }
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  @Output() goBack = new EventEmitter<string>();
  @ViewChild('TABLE', { read: ElementRef }) table: ElementRef;
  columns: string[];
  _fromView: string;
  tReport: ReportModel;
  dataSource: MatTableDataSource<any>;// = new MatTableDataSource(ELEMENT_DATA);
  _nBrandId: number;
  constructor(private _liveAnnouncer: LiveAnnouncer, private analyticsService: AnalyticsService, private service: ExStoreService) {

  }

  @ViewChild(MatSort, { static: false }) sort: MatSort;

  ngOnInit() {
    // get data from API 

  }

  goBackClicked() {
    this.goBack.emit(this._fromView);
  }

  openItem(item: any) {
    this.service.SearchStore(item, this._nBrandId).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  getReport(reportId: number, tParam1: string, tParam2: string) {
    this.analyticsService.GetReport(reportId, tParam1, tParam2, "", "", "").subscribe(x => {
      this.tReport = x;
      this.loadTable();
    });
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.toLocaleLowerCase().indexOf("cost") > -1) {
      rVal = "$" + colVal;
    }
    else if (colName.toLocaleLowerCase().indexOf("date") > -1 || colName.toLocaleLowerCase().indexOf("go-live") > -1) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  loadTable() {
    this.columns = [];
    for (var indx in this.tReport.reportTable[0]) {
      this.columns.push(indx);
    }
    this.dataSource = new MatTableDataSource(this.tReport.reportTable);
    this.dataSource.sort = this.sort;
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

    var compare = function (a: any, b: any) {
      if (a[sortState.active].toLowerCase() < b[sortState.active].toLowerCase()) {
        return -1;
      }
      if (a[sortState.active].toLowerCase() > b[sortState.active].toLowerCase()) {
        return 1;
      }
      return 0;
    }
    this.dataSource = new MatTableDataSource(this.tReport.reportTable.sort(compare));
    this.dataSource.sort = this.sort;
  }

  exportAsExcel() {
    // const workSheet = XLSX.utils.json_to_sheet(this.dataSource.data, { header: this.tReport.headers });
    // const workBook: XLSX.WorkBook = XLSX.utils.book_new();
    // XLSX.utils.book_append_sheet(workBook, workSheet, 'SheetName');
    // XLSX.writeFile(workBook, 'filename.xlsx');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.table.nativeElement);//converts a DOM TABLE element to a worksheet
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.tReport.tReportName + '.xlsx');

  }
}
