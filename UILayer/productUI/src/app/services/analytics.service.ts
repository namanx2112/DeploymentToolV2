import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { ProjectPortfolio, ReportModel } from '../interfaces/analytics';
import { CommonService } from './common.service';
import { DahboardTile, Dictionary } from '../interfaces/commons';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {

  constructor(private http: HttpClient, private cacheService: CacheService) { }

  Get(searchFields: Dictionary<string> | null) {
    return this.http.post<ProjectPortfolio[]>(CommonService.ConfigUrl + "Analytics/GetProjectPortfolio", searchFields, { headers: this.cacheService.getHttpHeaders() });
  }

  GetReport(searchFields: number) {
    return this.http.get<ReportModel>(CommonService.ConfigUrl + "Analytics/GetReport?reportId=" + searchFields, { headers: this.cacheService.getHttpHeaders() });
  }

  GetDashboards(nBrandId: number, tProjectTypes: string) {
    return this.http.get<DahboardTile[]>(CommonService.ConfigUrl + "Analytics/GetDashboards?nBrandId=" + nBrandId + "&tProjectTypes=" + tProjectTypes, { headers: this.cacheService.getHttpHeaders() });
  }
}
