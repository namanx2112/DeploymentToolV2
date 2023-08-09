import { Injectable } from '@angular/core';
import { AuthCodes } from '../interfaces/auth-request';

@Injectable({
  providedIn: 'root'
})
export class AccessService {

  static userAccessList: any[] = [];
  restricted: string[];
  constructor() {
    this.restricted = [
      "home.sonic.project.deliverystatus", "home.sonic.project.documentstab"
    ];
  }



  public hasAccess(path: string, code: AuthCodes) {
    path = path.toLowerCase()
    let tVal = (this.restricted.indexOf(path) > -1) ? false : true;
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
    if (typeof AccessService.userAccessList == 'undefined' || AccessService.userAccessList.length == 0) {
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
