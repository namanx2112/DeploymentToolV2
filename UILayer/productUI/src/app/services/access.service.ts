import { Injectable } from '@angular/core';
import { AuthCodes } from '../interfaces/auth-request';

@Injectable({
  providedIn: 'root'
})
export class AccessService {

  static userAccessList: any[];
  constructor() {

  }



  public hasAccess(path: string, code: AuthCodes) {
    let tVal = true;
    var tAccess = this.getAccess(path);
    if (tAccess) {
      if (code == AuthCodes.Show)
        tVal = (tAccess.nPermVal == 1 || tAccess.nPermVal == 2);
      else if (code == AuthCodes.Edit)
        tVal = (tAccess.nPermVal == 2);
    }
    return tVal;
  }

  getAccess(path: string) {
    let tAccess = null;
    if (typeof AccessService.userAccessList == 'undefined') {
      let userAccessList = localStorage.getItem('userAccessList');
      if (typeof userAccessList != 'undefined' && userAccessList != null)
        AccessService.userAccessList = JSON.parse(atob(userAccessList));
    }
    tAccess = AccessService.userAccessList.find(x => x.tPermissionName == path);
    return tAccess;
  }

  static setAccess(sAccess: any) {
    localStorage.setItem("userAccessList", sAccess.tData);
  }
}
