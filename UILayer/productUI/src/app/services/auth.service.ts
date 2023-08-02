import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthResponse } from '../interfaces/auth-response';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthCodes, AuthRequest } from '../interfaces/auth-request';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(private http: HttpClient, private route: ActivatedRoute, public router: Router) {
  }

  signIn(user: any) {
    return this.http.post<AuthResponse>(CommonService.ConfigUrl + "token/get", user, { headers: this.headers })
      .subscribe((res: AuthResponse) => {
        localStorage.setItem('authResponse', JSON.stringify(res));
        this.router.navigate(['./home'], { skipLocationChange: true, relativeTo: this.route });
      });
  }

  ChangePassword(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "token/ChangePassword", request, { headers: this.getHttpHeaders() });
  }


  isLoggedIn() {
    if (this.getToken() != "")
      this.router.navigate(['./home'], { skipLocationChange: true, relativeTo: this.route });
  }

  loggedOut() {
    localStorage.clear();
    this.router.navigate(['./login'], { skipLocationChange: true, relativeTo: this.route });
  }

  getHttpHeaders(): HttpHeaders {
    let tHeader = new HttpHeaders().set('Content-Type', 'application/json').set("Authorization", "Bearer " + this.getToken());
    return tHeader;
  }

  getUserName() {
    let authResp = localStorage.getItem('authResponse');
    let userName = 'NA';
    if (typeof authResp != 'undefined' && authResp != null && authResp != '') {
      userName = JSON.parse(authResp).userName;
    }
    return userName;
  }

  getToken() {
    let authResp = localStorage.getItem('authResponse');
    let token = '';
    if (typeof authResp != 'undefined' && authResp != null && authResp != '') {
      token = JSON.parse(authResp).auth.Token;
    }
    return token;
  }
}
