import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ReportModel } from 'src/app/interfaces/analytics';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-advanced-search-result-try',
  templateUrl: './advanced-search-result-try.component.html',
  styleUrls: ['./advanced-search-result-try.component.css']
})
export class AdvancedSearchResultTryComponent {
  @Input()
  set reportParam(val: any) {
    this.tParams = val;
    this._nBrandId = val.nBrandId;
    this._fromView = val.fromView;
    setTimeout(() => {
      this.getReport();
    }, 10);
  }
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  @Output() goBack = new EventEmitter<string>();
  @ViewChild('tableReference', { read: ElementRef }) table: ElementRef;
  columns: string[];
  _fromView: string;
  tReport: ReportModel;
  _nBrandId: number;
  totalRows = 0;
  pageSize = CommonService.defaultPagesize;
  currentPage = 0;
  pageSizeOptions: number[] = CommonService.pageSizeOptions;
  tParams: any;
  storeIdColumn: number = -1;
  constructor(private _liveAnnouncer: LiveAnnouncer, private analyticsService: AnalyticsService, private service: ExStoreService) {

  }

  ngafterviewinit() {

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
    let sNo = item["Store Number"];
    this.service.SearchStore(sNo, this._nBrandId).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  getReport() {
    let newParam = this.tParams;
    newParam.pageSize = this.pageSize.toString();
    newParam.currentPage = this.currentPage.toString();
    this.analyticsService.GetStoreTable(newParam).subscribe(x => {
      this.totalRows = x.nTotalRows;
      this.tReport = x.response;
      this.loadTable();
    });
  }

  getFormatedDate(strVal: string) {
    return CommonService.getFormatedDateString(strVal);
  }

  loadTable() {
    this.columns = [];
    let i = 0;
    for (var indx in this.tReport.reportTable[0]) {
      if (indx.toLowerCase() == "store number")
        this.storeIdColumn = i;
      this.columns.push(indx);
      i++;
    }
  }

  serverDownload() {
    let newParam = this.tParams;
    newParam.pageSize = this.pageSize.toString();
    newParam.currentPage = this.currentPage.toString();
    this.analyticsService.DownloadStoreTable(newParam).subscribe(tdata => {
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
    }, async (error) => {
      console.log("Error occured:" + error);
    });
  }

  exportAsExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.table.nativeElement);//converts a DOM TABLE element to a worksheet
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, this.tReport.tReportName + '.xlsx');
  }
}
