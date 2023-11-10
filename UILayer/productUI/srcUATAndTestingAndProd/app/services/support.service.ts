import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { SupportContent } from '../interfaces/models';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class SupportService {

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }


  Get(request: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "Support/Get?nTicketId=" + request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetAll() {
    return this.http.get<any>(CommonService.ConfigUrl + "Support/GetAll", { headers: this.cacheService.getHttpHeaders() });
  }

  LogSupportTicket(request: SupportContent) {
    return this.http.post<any>(CommonService.ConfigUrl + "Support/Log", request, { headers: this.cacheService.getHttpHeaders() });
  }
}
