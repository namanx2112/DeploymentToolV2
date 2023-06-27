import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { PartsModel, QuoteRequestTemplate } from '../interfaces/models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class POWorkflowConfigService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  GetTableVisibleColumns(){
    return [
      "tPartsName",
      "nPartsNumber",
      "nPartsPrice"
    ];
  }

  Create(request: any) {
    return this.http.post<PartsModel>(this.configUrl + "Parts/CreateParts", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<PartsModel>(this.configUrl + "Parts/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  Get(request: number | null) {
    return this.http.post<QuoteRequestTemplate[]>(this.configUrl + "Parts/GetPartss", request, { headers: this.authService.getHttpHeaders() });
  }
}
