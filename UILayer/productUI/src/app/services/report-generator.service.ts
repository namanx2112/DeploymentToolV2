import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { PartsModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';
import { ReportField, ReportFieldAndOperatorType, ReportFolder, ReportInfo } from '../interfaces/report-generator';

@Injectable({
  providedIn: 'root'
})
export class ReportGeneratorService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) {
  }

  // CreateFolder(request: any) {
  //   return this.http.post<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Create", request, { headers: this.cacheService.getHttpHeaders() });
  // }

  // Update(request: any) {
  //   return this.http.post<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Update", request, { headers: this.cacheService.getHttpHeaders() });
  // }

  GetMyFolders(nBrandId: number) {
    return this.http.get<ReportFolder[]>(CommonService.ConfigUrl + "ReportGenerator/GetMyFolder?nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  EditFolder(request: any) {
    return this.http.post<ReportFolder>(CommonService.ConfigUrl + "ReportGenerator/EditFolder", request, { headers: this.cacheService.getHttpHeaders() });
  }

  DeleteFolder(request: any) {
    return this.http.post<ReportFolder>(CommonService.ConfigUrl + "ReportGenerator/DeleteFolder", request, { headers: this.cacheService.getHttpHeaders() });
  }

  DeleteReport(request: any) {
    return this.http.post<ReportInfo>(CommonService.ConfigUrl + "ReportGenerator/DeleteReport", request, { headers: this.cacheService.getHttpHeaders() });
  }


  GetReportsForFolder(nFolderId: number) {
    return this.http.get<ReportInfo[]>(CommonService.ConfigUrl + "ReportGenerator/GetReportsForFolder?nFolderId=" + nFolderId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetReportDetails(nReportId: number) {
    return this.http.get<ReportInfo>(CommonService.ConfigUrl + "ReportGenerator/GetReportDetails?nReportId=" + nReportId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetReportFields(nBrandID: number) {
    return this.http.get<ReportField[]>(CommonService.ConfigUrl + "ReportGenerator/GetReportFields?nBrandID=" + nBrandID, { headers: this.cacheService.getHttpHeaders() });
  }

  GetFieldOperatorType(nBrandID: number) {
    return this.http.get<ReportFieldAndOperatorType[]>(CommonService.ConfigUrl + "ReportGenerator/GetFieldOperatorType?nBrandID=" + nBrandID, { headers: this.cacheService.getHttpHeaders() });
  }

  EditReport(request: any) {
    return this.http.post<ReportFolder>(CommonService.ConfigUrl + "ReportGenerator/EditReport", request, { headers: this.cacheService.getHttpHeaders() });
  }

  // Delete(request: FranchiseModel) {
  //   return this.http.get<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Delete?id=" + request.aFranchiseId, { headers: this.cacheService.getHttpHeaders() });
  // }

}
