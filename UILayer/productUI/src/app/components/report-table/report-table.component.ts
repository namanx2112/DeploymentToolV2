import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
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
    this._nBrandId = val.nBrandId;
    this._fromView = val.fromView;
    this.reportId = val.request.reportId;
    this.tParam1 = val.request.tParam1;
    this.tParam = val.tParam;
    this.tProjectIDs = val.request.tProjectIDs;
    this.canEdit = val.canEdit;
    this.getReport();
  }
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  @Output() goBack = new EventEmitter<any>();
  @ViewChild('TABLE', { read: ElementRef }) table: ElementRef;
  reportId: number;
  tProjectIDs: string;
  tParam1: string;
  tParam: string;
  columns: string[];
  _fromView: string;
  tReport: ReportModel;
  dataSource: MatTableDataSource<any>;// = new MatTableDataSource(ELEMENT_DATA);
  _nBrandId: number;
  totalRows = 0;
  pageSize = CommonService.defaultPagesize;
  currentPage = 0;
  pageSizeOptions: number[] = CommonService.pageSizeOptions;
  canEdit: boolean = false;
  constructor(private _liveAnnouncer: LiveAnnouncer, private analyticsService: AnalyticsService, private service: ExStoreService) {

  }

  @ViewChild(MatSort, { static: false }) sort: MatSort;

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
    this.service.SearchStore(item, this._nBrandId).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  getReport() {
    if (typeof this.tProjectIDs != 'undefined') {
      this.analyticsService.GetReport(this.reportId, this._nBrandId, this.tProjectIDs, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => {
          this.totalRows = x.nTotalRows;
          this.tReport = x.response;
          this.loadTable();
        });
    }
    else
      this.analyticsService.GetReport(this.reportId, this._nBrandId, this.tParam1, this.tParam, "", "", "",
        this.pageSize, this.currentPage).subscribe(x => {
          this.totalRows = x.nTotalRows;
          this.tReport = x.response;
          this.loadTable();
        });
  }

  editReport() {
    this.goBack.emit({ action: "edit", reportId: this.reportId });
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
