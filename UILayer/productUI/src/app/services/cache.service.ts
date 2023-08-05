import { Injectable } from '@angular/core';
import { DropdownServiceService } from './dropdown-service.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { BrandModel, DropwDown } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  constructor(private http: HttpClient) { }

  getBrands(callBack: any) {
    if (CommonService.allBrands)
      callBack(CommonService.allBrands);
    else {
      this.http.post<BrandModel[]>(CommonService.ConfigUrl + "Brand/Get", null, { headers: this.getHttpHeaders() }).
        subscribe(x => {
          callBack(x);
          CommonService.allBrands;
        });
    }
  }

  

  getHttpHeaders(): HttpHeaders {
    let tHeader = new HttpHeaders().set('Content-Type', 'application/json').set("Authorization", "Bearer " + this.getToken());
    return tHeader;
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
