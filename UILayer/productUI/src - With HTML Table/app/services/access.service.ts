import { Injectable } from '@angular/core';
import { AuthCodes } from '../interfaces/auth-request';
import { UserAccessMeta } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class AccessService {

  static userAccessList: any[] = [];
  static shown: boolean;
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

  getMyUserMeta(): UserAccessMeta{
    let meta = {
      nOriginatorId: 0,
      userType: 0
    }
    let sItem = sessionStorage.getItem("userMeta");
    if(sItem != null){
      meta = JSON.parse(sItem);
    }
    return meta;
  }

  getCompFieldAccess(){
    let fieldAccess = {};
    let sItem = sessionStorage.getItem("compFieldAccess");
    if(sItem != null){
      fieldAccess = JSON.parse(sItem);
    }
    return fieldAccess;
  }

  getAccess(path: string) {
    let tAccess = null;
    if (typeof AccessService.userAccessList == 'undefined' || AccessService.userAccessList.length == 0) {
      let userAccessList = sessionStorage.getItem('userAccessList');
      if (typeof userAccessList != 'undefined' && userAccessList != null && userAccessList != "null")
        AccessService.userAccessList = JSON.parse(atob(userAccessList));
      else {
        if (!AccessService.shown) {
          AccessService.shown = true;
          alert("You don not have any access, please contact Admin to get access");
        }
        return {};
      }
    }
    tAccess = AccessService.userAccessList.find(x => x.tPermissionName == path);
    return tAccess;
  }

  static setAccess(sAccess: any) {
    sessionStorage.setItem("userAccessList", sAccess.tData);
    sessionStorage.setItem("userMeta", JSON.stringify(sAccess.userMeta));
    sessionStorage.setItem("compFieldAccess", JSON.stringify(sAccess.compFieldAccess));
  }
}
