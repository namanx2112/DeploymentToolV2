import { Injectable } from '@angular/core';
import { DropdownServiceService } from './dropdown-service.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { BrandModel, DropwDown } from '../interfaces/models';
import { AccessService } from './access.service';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  constructor(private http: HttpClient, private accessService: AccessService) { }

  getBrands(callBack: any) {
    if (CommonService.allBrands && CommonService.allBrands.length > 0)
      callBack(CommonService.allBrands);
    else {
      this.http.post<BrandModel[]>(CommonService.ConfigUrl + "Brand/Get", null, { headers: this.getHttpHeaders() }).
        subscribe(x => {
          callBack(x);
          CommonService.allBrands = x;
        });
    }
  }

  getHttpHeaders(refreshToken: boolean = false): HttpHeaders {
    let tHeader = new HttpHeaders().set('Content-Type', 'application/json').set("Authorization", "Bearer " + this.getToken(refreshToken));
    return tHeader;
  }

  getToken(refreshToken: boolean = false) {
    let authResp = sessionStorage.getItem('aResp');
    let token = '';
    if (typeof authResp != 'undefined' && authResp != null && authResp != '') {
      if (refreshToken)
        token = JSON.parse(authResp).auth.RefreshToken;
      else
        token = JSON.parse(authResp).auth.Token;
    }
    return token;
  }
}
