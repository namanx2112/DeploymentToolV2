import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Fields, FieldType } from '../interfaces/home-tab';
import { UserModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { Dictionary } from '../interfaces/commons';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService, private commonService: CommonService) {
    this.configUrl = authService.getConfigUrl();
  }

  Create(request: any) {
    return this.http.post<number>(this.configUrl + "User/Create", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<number>(this.configUrl + "User/Update", request, { headers: this.authService.getHttpHeaders() });
  }
  
  GetTableVisibleColumns(){
    return [
      "tEmail",
      "tUserName",
      "tEmail",
      "nDepartment",
      "nRole"
    ];
  }

  Get(searchFields: Dictionary<string> | null) {
    return this.http.post<UserModel[]>(this.configUrl + "User/Get", searchFields, { headers: this.authService.getHttpHeaders() });
    // return new Observable<UserModel[]>((obj) => {
    //   let items = [{
    //     aUserID: 0,
    //     tUserName: "Tom",
    //     tUserEmail: "tom@tom.com",
    //     tContactNumber: "9898989839",
    //     tRole: "Admin",
    //     nBrandId: 1
    //   },
    //   {
    //     aUserID: 0,
    //     tUserName: "Pom",
    //     tUserEmail: "pom@tom.com",
    //     tContactNumber: "8898989839",
    //     tRole: "Admin",
    //     nBrandId: 1
    //   },
    //   {
    //     aUserID: 0,
    //     tUserName: "Com",
    //     tUserEmail: "Com@tom.com",
    //     tContactNumber: "7898989839",
    //     tRole: "Admin",
    //     nBrandId: 1
    //   }];
    //     obj.next(items);
    // });
  }

  GetSearchFields(): Fields[] {
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
      hidden: false,
      icon: "search"
    }]
    return fields;
  }
  GetFields(): Fields[] {
    let fields = [{
      field_name: "User Id",
      fieldUniqeName: "aUserID",
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
      field_name: "Vendor Id",
      fieldUniqeName: "nVendorId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Vendor Id",
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "User Name",
      fieldUniqeName: "tName",
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
      field_name: "User Login Name",
      fieldUniqeName: "tUserName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Login Name",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Email",
      fieldUniqeName: "tEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter User Email",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "User Department",
      fieldUniqeName: "nDepartment",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,      
      options: this.commonService.GetDropdown("UserDepartment"),
      field_placeholder: "Enter User Department",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "User Role",
      fieldUniqeName: "nRole",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,      
      options: this.commonService.GetDropdown("UserRole"),
      field_placeholder: "Enter User Role",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Emp Id",
      fieldUniqeName: "tEmpID",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,      
      field_placeholder: "Enter User Emp Id",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "User Mobile Number",
      fieldUniqeName: "tMobile",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,      
      field_placeholder: "Enter User Mobile Number",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
}
