import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { AuthService } from './auth.service';
import { NewProjectStore } from '../interfaces/sonic';

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
}
