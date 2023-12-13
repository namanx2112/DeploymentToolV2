import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { ProjectPortfolio, ReportModel } from '../interfaces/analytics';
import { CommonService } from './common.service';
import { DahboardTile, Dictionary, MyReportModel } from '../interfaces/commons';
import { TableResponse } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {

  constructor(private http: HttpClient, private cacheService: CacheService) { }

  Get(searchFields: any) {
    return this.http.post<TableResponse>(CommonService.ConfigUrl + "Analytics/GetProjectPortfolio", searchFields, { headers: this.cacheService.getHttpHeaders() });
  }

  GetReport(reportId: number,nBrandId: number, tParam1: string, tParam2: string, tParam3: string, tParam4: string, tParam5: string, pageSize: number,
    currentPage: number) {
    return this.http.post<TableResponse>(CommonService.ConfigUrl + "Analytics/GetReport", {
      reportId: reportId, tParam1: tParam1, tParam2: tParam2,
      tParam3: tParam3, tParam4: tParam4, tParam5: tParam5, nCurrentPage: currentPage, nPageSize: pageSize, nBrandId: nBrandId
    }, { headers: this.cacheService.getHttpHeaders() });
  }

  DownloadReport(reportId: number, tParam1: string, tParam2: string, tParam3: string, tParam4: string, tParam5: string, pageSize: number,
    currentPage: number) {
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.cacheService.getToken(),
      "Accept": "application/vnd.ms-excel"
    });
    return this.http.post(CommonService.ConfigUrl + "Analytics/DownloadReport", {
      reportId: reportId, tParam1: tParam1, tParam2: tParam2,
      tParam3: tParam3, tParam4: tParam4, tParam5: tParam5, nCurrentPage: currentPage, nPageSize: pageSize
    }, { responseType: "blob", headers: httpHeader });
  }

  GetStoreTable(params: any) {
    return this.http.post<TableResponse>(CommonService.ConfigUrl + "Analytics/GetStoreTable", params, { headers: this.cacheService.getHttpHeaders() });
  }

  DownloadStoreTable(params: any) {
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.cacheService.getToken(),
      "Accept": "application/vnd.ms-excel"
    });
    return this.http.post(CommonService.ConfigUrl + "Analytics/DownloadStoreTable", params, { responseType: "blob", headers: httpHeader });
  }

  GetDashboards(nBrandId: number, tProjectTypes: string, dStart: string, dEnd: string) {
    return this.http.post<DahboardTile[]>(CommonService.ConfigUrl + "Analytics/GetDashboards", { "nBrandId": nBrandId, "tProjectTypes": tProjectTypes, dStart: dStart, dEnd: dEnd }, { headers: this.cacheService.getHttpHeaders() });
  }

  GetSavedReportsForMe(nBrandId: number) {
    return this.http.get<MyReportModel[]>(CommonService.ConfigUrl + "Analytics/GetSavedReportsForMe?nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }
}
