import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthResponse } from '../interfaces/auth-response';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthCodes, AuthRequest } from '../interfaces/auth-request';
import { CommonService } from './common.service';
import { AccessService } from './access.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(private http: HttpClient, private route: ActivatedRoute, public router: Router, private commonService: CommonService, private cacheService: CacheService) {
  }

  signIn(user: any) {
    return this.http.post<AuthResponse>(CommonService.ConfigUrl + "token/get", user, { headers: this.headers })
      .subscribe((res: any) => {
        if (typeof res == 'string')
          alert(res);
        else {
          sessionStorage.setItem('aResp', JSON.stringify(res));
          this.getAccess();
        }
      });
  }

  ChangePassword(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "token/ChangePassword", request, { headers: this.cacheService.getHttpHeaders() });
  }

  ForgotPassword(request: any) {
    return this.http.post<any>(CommonService.ConfigUrl + "token/ForgotPassword", request,  { headers: this.headers });
  }

  getAccess() {
    let cThis = this;
    this.http.get<any>(CommonService.ConfigUrl + "token/GetAccess", { headers: this.cacheService.getHttpHeaders() }).subscribe((x: any) => {
      AccessService.setAccess(x);
      this.commonService.getAllDropdowns(function () {
        cThis.router.navigate(['./home'], { skipLocationChange: true, relativeTo: cThis.route });
      });
    });
  }


  isLoggedIn() {
    if (this.cacheService.getToken() != "")
      this.router.navigate(['./home'], { skipLocationChange: true, relativeTo: this.route });
  }

  loggedOut() {
    sessionStorage.clear();
    AccessService.userAccessList = [];
    CommonService.allBrands = [];
    CommonService.allItems = [];
    this.router.navigate(['./login'], { skipLocationChange: true, relativeTo: this.route });
  }

  isFirstTime() {
    let yesFirstTime = false;
    let resp = sessionStorage.getItem('aResp');
    if (typeof resp != 'undefined' && resp != null && resp != '') {
      let jResp = JSON.parse(resp);
      yesFirstTime = (jResp.isFirstTime == 1);
      if (yesFirstTime) {
        jResp.isFirstTime = 0
        sessionStorage.setItem("aResp", JSON.stringify(jResp));
      }
    }
    return yesFirstTime;
  }

  getUserName() {
    let authResp = sessionStorage.getItem('aResp');
    let userName = 'NA';
    if (typeof authResp != 'undefined' && authResp != null && authResp != '') {
      userName = JSON.parse(authResp).tName;
    }
    return userName;
  }
}
