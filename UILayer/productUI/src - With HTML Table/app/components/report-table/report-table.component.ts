import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ReportModel } from 'src/app/interfaces/analytics';
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
    this._nBrandId = val.nBrandId;
    this._fromView = val.fromView;
    this.reportId = val.request.reportId;
    this.tParam1 = val.request.tParam1;
    this.tParam = val.tParam;
    this.tProjectIDs = val.request.tProjectIDs;
    this.getReport();
  }
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  @Output() goBack = new EventEmitter<string>();
  @ViewChild('TABLE', { read: ElementRef }) table: ElementRef;
  reportId: number;
  tProjectIDs: string;
  tParam1: string;
  tParam: string;
  columns: string[];
  _fromView: string;
  tReport: ReportModel;
  _nBrandId: number;
  totalRows = 0;
  pageSize = CommonService.defaultPagesize;
  currentPage = 0;
  pageSizeOptions: number[] = CommonService.pageSizeOptions;
  storeIdColumn: number = -1;
  storeNumberColName: string;
  constructor(private _liveAnnouncer: LiveAnnouncer, private analyticsService: AnalyticsService, private service: ExStoreService) {

  }

  ngOnInit() {
    // get data from API 

  }

  pageChanged(event: PageEvent) {
    console.log({ event });
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getReport();
  }

  goBackClicked() {
    this.goBack.emit(this._fromView);
  }

  openItem(item: any) {
    if (item[this.storeNumberColName]) {
      this.service.SearchStore(item[this.storeNumberColName], this._nBrandId).subscribe((x: StoreSearchModel[]) => {
        this.openStore.emit(x[0]);
      });
    }
  }

  getReport() {
    if (typeof this.tProjectIDs != 'undefined') {
      this.analyticsService.GetReport(this.reportId, this.tProjectIDs, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => {
          this.totalRows = x.nTotalRows;
          this.tReport = x.response;
          this.loadTable();
        });
    }
    else
      this.analyticsService.GetReport(this.reportId, this.tParam1, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => {
          this.totalRows = x.nTotalRows;
          this.tReport = x.response;
          this.loadTable();
        });
  }

  serverDownload() {
    let downloadCalback = function (tdata: any) {
      var newBlob = new Blob([tdata], { type: "application/vnd.ms-excel" });

      // For other browsers: 
      // Create a link pointing to the ObjectURL containing the blob.
      const data = window.URL.createObjectURL(newBlob);

      var link = document.createElement('a');
      link.href = data;
      link.download = "report.xlsx";
      // this is necessary as link.click() does not work on the latest firefox
      link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

      setTimeout(function () {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(data);
        link.remove();
      }, 100);
    }
    if (typeof this.tProjectIDs != 'undefined') {
      this.analyticsService.DownloadReport(this.reportId, this.tProjectIDs, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => downloadCalback);
    }
    else
      this.analyticsService.DownloadReport(this.reportId, this.tParam1, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => downloadCalback);
  }

  loadTable() {
    this.columns = [];
    let i = -1;
    this.storeIdColumn = -1;
    for (var indx in this.tReport.reportTable[0]) {
      if (this.storeIdColumn == -1) {
        if (indx.toLowerCase() == "store number") {
          this.storeIdColumn = i;
          this.storeNumberColName = "Store Number";
        }
        else if (indx.toLowerCase() == "storeno") {
          this.storeNumberColName = "StoreNo";
          this.storeIdColumn = i;
        }
        else if (indx.toLowerCase() == "store#") {
          this.storeNumberColName = "Store#";
          this.storeIdColumn = i;
        }
      }
      this.columns.push(indx);
      i++;
    }
  }

  exportAsExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.table.nativeElement);//converts a DOM TABLE element to a worksheet
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.tReport.tReportName + '.xlsx');

  }
}
