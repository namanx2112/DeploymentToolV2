import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { MergedPO, POConfigTemplate, POMailMessage } from '../interfaces/models';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class POWorkflowConfigService {
  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  GetAllTemplate(nBrandId: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "PurchaseOrder/GetAllTemplate?nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTemplate(nTemplateId: number) {
    return this.http.get<POConfigTemplate>(CommonService.ConfigUrl + "PurchaseOrder/GetTemplate?nTemplateId=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }

  CreateUpdateTemplate(request: POConfigTemplate) {
    return this.http.post<POConfigTemplate>(CommonService.ConfigUrl + "PurchaseOrder/CreateUpdateTemplate", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(nTemplateId: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "PurchaseOrder/Delete?id=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetMergedPO(nStoreId: number, nTemplateId: number) {
    return this.http.get<MergedPO>(CommonService.ConfigUrl + "PurchaseOrder/GetMergedPO?nStoreId=" + nStoreId + "&nTemplateId=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }

  SenMergedPO(request: MergedPO) {
    return this.http.post<POMailMessage>(CommonService.ConfigUrl + "PurchaseOrder/SenMergedPO", request, { headers: this.cacheService.getHttpHeaders() });
  }

  SendPO(request: POMailMessage) {
    return this.http.post<any>(CommonService.ConfigUrl + "PurchaseOrder/SendPO", request, { headers: this.cacheService.getHttpHeaders() });
  }

  downloadPO(tMyFolderId: string, tFileName: string, nStoreId: number) {
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.cacheService.getToken(),
      "Accept": "application/pdf"
    });
    return this.http.post(CommonService.ConfigUrl + "PurchaseOrder/downloadPO", { tMyFolderId: tMyFolderId, tFileName: tFileName, nStoreId: nStoreId }, { responseType: "blob", headers: httpHeader });
  }
}