import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { AuthService } from './auth.service';
import { ActiveProject, DeliveryStatus, HistoricalProjects, NewProjectStore } from '../interfaces/sonic';
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
    else if (tab.tab_type == TabType.StoreNotes) {
      return [
        "dNotesDate",
        "tType",
        "tSource",
        "tNote"
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
      return this.getNotes();
  }



  getNotes() {
    // return new Observable<SonicNotes[]>((obj) => {
    //   let items: SonicNotes[] = [
    //     {
    //       aNotesId: 1005,
    //       dNotesDate: new Date(),
    //       tType: "General",
    //       tSource: "Clark Kent",
    //       tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
    //     },
    //     {
    //       aNotesId: 1005,
    //       dNotesDate: new Date(),
    //       tType: "POPS",
    //       tSource: "Clark Kent",
    //       tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
    //     },
    //     {
    //       aNotesId: 1005,
    //       dNotesDate: new Date(),
    //       tType: "TEGS",
    //       tSource: "Clark Kent",
    //       tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
    //     },
    //     {
    //       aNotesId: 1005,
    //       dNotesDate: new Date(),
    //       tType: "DFDDSD",
    //       tSource: "Clark Kent",
    //       tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
    //     },
    //     {
    //       aNotesId: 1005,
    //       dNotesDate: new Date(),
    //       tType: "RRSD",
    //       tSource: "Clark Kent",
    //       tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
    //     }
    //   ];
    //   obj.next(items);
    // });
  }

  getActiveProjects(request: any) {
    return this.http.post<HistoricalProjects>(CommonService.ConfigUrl + "Store/GetActiveProjects", request, { headers: this.cacheService.getHttpHeaders() });
  }

  getHistoricalProjects(request: any) {
    return this.http.post<HistoricalProjects>(CommonService.ConfigUrl + "Store/GetHistoricalProjects", request, { headers: this.cacheService.getHttpHeaders() });
  }
}
