import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { ProjectPortfolio } from '../interfaces/analytics';
import { CommonService } from './common.service';
import { Dictionary } from '../interfaces/commons';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {

  constructor(private http: HttpClient, private cacheService: CacheService) { }

  Get(searchFields: Dictionary<string> | null) {
    return this.http.post<ProjectPortfolio[]>(CommonService.ConfigUrl + "Analytics/GetProjectPortfolio", searchFields, { headers: this.cacheService.getHttpHeaders() });
  }
}
