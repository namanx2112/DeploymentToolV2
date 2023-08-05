import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { QuoteRequestTemplate } from '../interfaces/models';
import { MergedQuoteRequest } from '../interfaces/models';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class QuoteRequestWorkflowConfigService {
  constructor(private http: HttpClient, private cacheService: CacheService) { 
  }

  GetAllTemplate(nBrandId: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "QuoteRequest/GetAllTemplate?nBrandId="+ nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTemplate(nTemplateId: number) {
    return this.http.get<QuoteRequestTemplate>(CommonService.ConfigUrl + "QuoteRequest/GetTemplate?nTemplateId=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }

  CreateUpdateTemplate(request: QuoteRequestTemplate) {
    return this.http.post<QuoteRequestTemplate>(CommonService.ConfigUrl + "QuoteRequest/CreateUpdateTemplate", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(nTemplateId: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "QuoteRequest/Delete?id=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }  

  GetMergedQuoteRequest(nStoreId: number,nTemplateId: number){
    return this.http.get<MergedQuoteRequest>(CommonService.ConfigUrl + "QuoteRequest/GetMergedQuoteRequest?nStoreId=" + nStoreId + "&nTemplateId=" + nTemplateId, { headers: this.cacheService.getHttpHeaders() });
  }

  SendQuoteRequest(request: MergedQuoteRequest){
    return this.http.post<any>(CommonService.ConfigUrl + "QuoteRequest/SendQuoteRequest", request, { headers: this.cacheService.getHttpHeaders() });
  }
}
