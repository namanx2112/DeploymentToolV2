import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { QuoteRequestTemplate } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class QuoteRequestWorkflowConfigService {
  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) { 
    this.configUrl = authService.getConfigUrl();
  }

  GetAllTemplate(nBrandId: number) {
    return this.http.get<any>(this.configUrl + "Sonic/GetAllTemplate?=nBrandId"+ nBrandId, { headers: this.authService.getHttpHeaders() });
  }

  GetTemplate(nTemplateId: number) {
    return this.http.get<QuoteRequestTemplate>(this.configUrl + "Sonic/GetTemplate?nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }

  CreateUpdateTemplate(request: QuoteRequestTemplate) {
    return this.http.post<QuoteRequestTemplate>(this.configUrl + "Sonic/CreateUpdateTemplate", request, { headers: this.authService.getHttpHeaders() });
  }

  Delete(nTemplateId: number) {
    return this.http.get<any>(this.configUrl + "Sonic/Delete?nTemplateId=" + nTemplateId, { headers: this.authService.getHttpHeaders() });
  }
}
