import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { UserModel } from '../interfaces/models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  CreateUser(request: any) {
    return this.http.post<UserModel>(this.configUrl + "User/CreateUser", request, { headers: this.authService.getHttpHeaders() });
  }

  UpdateUser(request: any) {
    return this.http.post<UserModel>(this.configUrl + "User/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetUsers(request: UserModel | null){
    return this.http.post<UserModel[]>(this.configUrl + "User/GetUsers", request, { headers: this.authService.getHttpHeaders() });
  }

  GetUserSearchFields(): Fields[]{
    let fields = [{
      field_name: "User Id",
      fieldUniqeName: "aUserId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter User Id",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
  GetUserFields(): Fields[]{
    let fields = [{
      field_name: "User Id",
      fieldUniqeName: "aUserId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter User Id",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
}
