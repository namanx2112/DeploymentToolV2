import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { AuthService } from './auth.service';
import { MergedQuoteRequest, NewProjectStore } from '../interfaces/sonic';
import { ProjectTemplates } from '../interfaces/models';

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

  CreateAndGetProjectStoreDetails(nProjectId: number) {
    return this.http.get<any>(this.configUrl + "Sonic/CreateAndGetProjectStoreDetails?nProjectId=" + nProjectId, { headers: this.authService.getHttpHeaders() });
  }

  GetProjectTemplates(nBrandId: number){
    return this.http.get<ProjectTemplates[]>(this.configUrl + "Sonic/GetProjectTemplates?nBrandId=" + nBrandId, { headers: this.authService.getHttpHeaders() });
  }

  GetMergedQuoteRequest(nProjectId: number){
    return this.http.get<MergedQuoteRequest>(this.configUrl + "Sonic/GetMergedQuoteRequest?nProjectId=" + nProjectId, { headers: this.authService.getHttpHeaders() });
  }

  SendQuoteRequest(request: MergedQuoteRequest){
    return this.http.post<any>(this.configUrl + "Sonic/SendQuoteRequest", request, { headers: this.authService.getHttpHeaders() });
  }
}
