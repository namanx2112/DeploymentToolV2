import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { SupportContent } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class SupportService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }


  Get(request: number) {
    return this.http.get<any>(this.configUrl + "Support/Get?nTicketId=" + request, { headers: this.authService.getHttpHeaders() });
  }

  LogSupportTicket(request: SupportContent) {
    return this.http.post<any>(this.configUrl + "Support/Log", request, { headers: this.authService.getHttpHeaders() });
  }
}
