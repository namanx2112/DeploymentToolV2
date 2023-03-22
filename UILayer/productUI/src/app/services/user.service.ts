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

  GetUsers(request: UserModel | null) {
    return this.http.post<UserModel[]>(this.configUrl + "User/GetUsers", request, { headers: this.authService.getHttpHeaders() });
  }

  GetUserSearchFields(): Fields[] {
    let fields = [{
      field_name: "User Name",
      fieldUniqeName: "tUserName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Name",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
  GetUserFields(): Fields[] {
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
      hidden: true
    },
    {
      field_name: "User Name",
      fieldUniqeName: "tUserName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Name",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Email",
      fieldUniqeName: "tUserEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter User Email",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Contact Number",
      fieldUniqeName: "tContactNumber",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Contact Number",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Role",
      fieldUniqeName: "tRole",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Role",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Brand Id",
      fieldUniqeName: "nBrandId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Brand Id",
      validator: [],
      mandatory: false,
      hidden: true
    }]
    return fields;
  }
}
