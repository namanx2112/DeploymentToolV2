import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { MergedPO, POConfigTemplate, POMailMessage } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class POWorkflowConfigService {
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

  GetMergedPO(nProjectId: number, nTemplateId: number) {
    return this.http.get<MergedPO>(this.configUrl + "PurchaseOrder/GetMergedPO?nProjectId=" + nProjectId + "&nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  SenMergedPO(request: MergedPO) {
    return this.http.post<POMailMessage>(this.configUrl + "PurchaseOrder/SenMergedPO", request, { headers: this.authService.getHttpHeaders() });
  }

  SendPO(request: POMailMessage) {
    return this.http.post<any>(this.configUrl + "PurchaseOrder/SendPO", request, { headers: this.authService.getHttpHeaders() });
  }

  downloadPO(tFileName: string, nProjectId: number) {
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.authService.getToken(),
      "Accept": "application/pdf"
    });
    return this.http.get(this.configUrl + "PurchaseOrder/downloadPO?tFileName=" + tFileName + "&nProjectId=" + nProjectId, { responseType: "blob", headers: httpHeader });
  }
}