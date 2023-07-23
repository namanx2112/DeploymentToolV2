import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { AuthService } from './auth.service';
import { ActiveProject, HistoricalProjects, NewProjectStore, SonicNotes } from '../interfaces/sonic';
import { ProjectTemplates } from '../interfaces/models';
import { Fields, HomeTab, TabType } from '../interfaces/home-tab';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  configUrl: any;
  constructor(private http: HttpClient, private commonService: CommonService, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  CreateNewStores(request: any) {
    return this.http.post<any>(this.configUrl + "Sonic/NewStore", request, { headers: this.authService.getHttpHeaders() });
  }

  UpdateStore(request: any) {
    return this.http.post<any>(this.configUrl + "Sonic/UpdateStore", request, { headers: this.authService.getHttpHeaders() });
  }

  getStoreDetails(nStoreId: number, nProjectType: number) {
    return this.http.get<any>(this.configUrl + "Sonic/getStoreDetails?nStoreId=" + nStoreId, { headers: this.authService.getHttpHeaders() });
  }

  GetProjectTemplates(nBrandId: number) {
    return this.http.get<ProjectTemplates[]>(this.configUrl + "Sonic/GetProjectTemplates?nBrandId=" + nBrandId, { headers: this.authService.getHttpHeaders() });
  }

  GetTableVisibleColumns(tab: HomeTab) {
    if (tab.tab_type == TabType.StoreProjects) {
      return [
        "tStoreNumber",
        "dProjectGoliveDate",
        "tProjectType",
        "tStatus",
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
    return new Observable<SonicNotes[]>((obj) => {
      let items: SonicNotes[] = [
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "General",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "POPS",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "TEGS",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "DFDDSD",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "RRSD",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        }
      ];
      obj.next(items);
    });
  }

  getActiveProjects(request: any) {
    return this.http.post<HistoricalProjects>(this.configUrl + "Store/GetActiveProjects", request, { headers: this.authService.getHttpHeaders() });
  }

  getHistoricalProjects(request: any) {
    return this.http.post<HistoricalProjects>(this.configUrl + "Store/GetHistoricalProjects", request, { headers: this.authService.getHttpHeaders() });
  }
}
