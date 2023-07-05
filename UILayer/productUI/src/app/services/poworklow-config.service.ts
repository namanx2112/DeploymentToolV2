import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { MergedPO, POConfigTemplate } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class POWorklowConfigService {
  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  GetAllTemplate(nBrandId: number) {
    return this.http.get<any>(this.configUrl + "PurchaseOrder/GetAllTemplate?nBrandId=" + nBrandId, { headers: this.authService.getHttpHeaders() });
  }

  GetTemplate(nTemplateId: number) {
    return this.http.get<POConfigTemplate>(this.configUrl + "PurchaseOrder/GetTemplate?nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  CreateUpdateTemplate(request: POConfigTemplate) {
    return this.http.post<POConfigTemplate>(this.configUrl + "PurchaseOrder/CreateUpdateTemplate", request, { headers: this.authService.getHttpHeaders() });
  }

  Delete(nTemplateId: number) {
    return this.http.get<any>(this.configUrl + "PurchaseOrder/Delete?nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  GetMergedQuoteRequest(nProjectId: number) {
    return this.http.get<MergedPO>(this.configUrl + "PurchaseOrder/GetMergedContent?nProjectId=" + nProjectId, { headers: this.authService.getHttpHeaders() });
  }

  SendQuoteRequest(request: MergedPO) {
    return this.http.post<any>(this.configUrl + "PurchaseOrder/Send", request, { headers: this.authService.getHttpHeaders() });
  }
}