import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { SupportContent } from '../interfaces/models';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class SupportService {

  constructor(private http: HttpClient, private authService: AuthService) {
  }


  Get(request: number) {
    return this.http.get<any>(CommonService.ConfigUrl + "Support/Get?nTicketId=" + request, { headers: this.authService.getHttpHeaders() });
  }

  GetAll() {
    return this.http.get<any>(CommonService.ConfigUrl + "Support/GetAll", { headers: this.authService.getHttpHeaders() });
  }

  LogSupportTicket(request: SupportContent) {
    return this.http.post<any>(CommonService.ConfigUrl + "Support/Log", request, { headers: this.authService.getHttpHeaders() });
  }
}
