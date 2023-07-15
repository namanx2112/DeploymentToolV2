import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { QuoteRequestTemplate } from '../interfaces/models';
import { MergedQuoteRequest } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class QuoteRequestWorkflowConfigService {
  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) { 
    this.configUrl = authService.getConfigUrl();
  }

  GetAllTemplate(nBrandId: number) {
    return this.http.get<any>(this.configUrl + "QuoteRequest/GetAllTemplate?nBrandId="+ nBrandId, { headers: this.authService.getHttpHeaders() });
  }

  GetTemplate(nTemplateId: number) {
    return this.http.get<QuoteRequestTemplate>(this.configUrl + "QuoteRequest/GetTemplate?nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  CreateUpdateTemplate(request: QuoteRequestTemplate) {
    return this.http.post<QuoteRequestTemplate>(this.configUrl + "QuoteRequest/CreateUpdateTemplate", request, { headers: this.authService.getHttpHeaders() });
  }

  Delete(nTemplateId: number) {
    return this.http.get<any>(this.configUrl + "QuoteRequest/Delete?id=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }  

  GetMergedQuoteRequest(nProjectId: number,nTemplateId: number){
    return this.http.get<MergedQuoteRequest>(this.configUrl + "QuoteRequest/GetMergedQuoteRequest?nProjectId=" + nProjectId + "&nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  SendQuoteRequest(request: MergedQuoteRequest){
    return this.http.post<any>(this.configUrl + "QuoteRequest/SendQuoteRequest", request, { headers: this.authService.getHttpHeaders() });
  }
}
