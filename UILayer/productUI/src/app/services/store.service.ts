import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { AuthService } from './auth.service';
import { ActiveProject, DateChangeBody, DateChangeNotificationBody, DateChangeNotitication, DateChangePOOption, DeliveryStatus, HistoricalProjects, NewProjectStore } from '../interfaces/sonic';
import { ProjectTemplates } from '../interfaces/models';
import { Fields, HomeTab, TabType } from '../interfaces/home-tab';
import { Observable } from 'rxjs';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  constructor(private http: HttpClient, private commonService: CommonService, private cacheService: CacheService) {
  }

  CreateNewStores(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "Sonic/NewStore", request, { headers: this.cacheService.getHttpHeaders() });
  }

  UpdateStore(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "Sonic/UpdateStore", request, { headers: this.cacheService.getHttpHeaders() });
  }

  getStoreDetails(nStoreId: number, nProjectType: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "Sonic/getStoreDetails?nStoreId=" + nStoreId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetProjectTemplates(nBrandId: number) {
    return this.http.get<ProjectTemplates[]>(CommonService.ConfigUrl + "Sonic/GetProjectTemplates?nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetDeliveryStatus(nStoreId: number) {
    return this.http.get<DeliveryStatus[]>(CommonService.ConfigUrl + "Store/GetDeliveryStatus?nStoreId=" + nStoreId, { headers: this.cacheService.getHttpHeaders() });
  }

  UpdateGoliveDate(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "Store/UpdateGoliveDate", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTableVisibleColumns(tab: HomeTab) {
    if (tab.tab_type == TabType.StoreProjects) {
      return [
        "tStoreNumber",
        "dProjectGoliveDate",
        "tProjectType",
        // "tStatus",
        "tPrevProjManager",
        "tProjManager",
        "tOldVendor",
        "tNewVendor"
      ];
    }
    else if (tab.tab_type == TabType.HistoricalProjects) {
      return [
        "tStoreNumber",
        "dProjectGoliveDate",
        "tProjectType",
        "dProjEndDate",
        "tProjManager",
        "tOldVendor"
      ];
    }
    else
      return [];
  }

  Get(request: any, tab: HomeTab) {
    if (tab.tab_type == TabType.StoreProjects)
      return this.getActiveProjects(request);
    else if (tab.tab_type == TabType.HistoricalProjects)
      return this.getHistoricalProjects(request);
    else
      return {};
  }

  getActiveProjects(request: any) {
    return this.http.post<HistoricalProjects>(CommonService.ConfigUrl + "Store/GetActiveProjects", request, { headers: this.cacheService.getHttpHeaders() });
  }

  getHistoricalProjects(request: any) {
    return this.http.post<HistoricalProjects>(CommonService.ConfigUrl + "Store/GetHistoricalProjects", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetDateChangeTable(nStoreId: number) {
    return this.http.get<DateChangeNotitication[]>(CommonService.ConfigUrl + "Store/GetDateChangeTable?nStoreId=" + nStoreId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetDateChangeBody(request: DateChangeBody) {
    return this.http.post<DateChangeNotificationBody>(CommonService.ConfigUrl + "Store/GetDateChangeBody", request, { headers: this.cacheService.getHttpHeaders() });
  }

  SendDateChangeNotification(request: DateChangeNotificationBody) {
    return this.http.post<any>(CommonService.ConfigUrl + "Store/SendDateChangeNotification", request, { headers: this.cacheService.getHttpHeaders() });
  }

  SendDateChangeRevisedPO(request: DateChangePOOption[]) {
    return this.http.post<any>(CommonService.ConfigUrl + "Store/SendDateChangeRevisedPO", request, { headers: this.cacheService.getHttpHeaders() });
  }
}
