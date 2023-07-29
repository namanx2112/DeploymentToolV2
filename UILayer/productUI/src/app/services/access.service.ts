import { Injectable } from '@angular/core';
import { AuthCodes } from '../interfaces/auth-request';

@Injectable({
  providedIn: 'root'
})
export class AccessService {

  constructor() { }

  

  public hasAccess(path: string, code: AuthCodes){
    return true;
  }
}
