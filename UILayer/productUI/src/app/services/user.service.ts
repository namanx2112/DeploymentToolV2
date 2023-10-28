import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Fields, FieldType } from '../interfaces/home-tab';
import { UserModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { Dictionary } from '../interfaces/commons';
import { Validators } from '@angular/forms';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) {
  }

  Create(request: any) {
    return this.http.post<number>(CommonService.ConfigUrl + "User/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<number>(CommonService.ConfigUrl + "User/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: UserModel) {
    return this.http.get<UserModel>(CommonService.ConfigUrl + "User/Delete?id=" + request.aUserID, { headers: this.cacheService.getHttpHeaders() });
  }

  
  GetTableVisibleColumns(){
    return [
      "tName",
      "tUserName",
      "tEmail",
      "nRole"
    ];
  }

  Get(searchFields: Dictionary<string> | null) {
    return this.http.post<UserModel[]>(CommonService.ConfigUrl + "User/Get", searchFields, { headers: this.cacheService.getHttpHeaders() });
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
  GetFields(needAccess: boolean): Fields[] {
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
      field_name: "Franchise Id",
      fieldUniqeName: "nFranchiseId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Franchise Id",
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "Name",
      fieldUniqeName: "tName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User Name",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "User access",
      fieldUniqeName: "nAccess",
      defaultVal: "2",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,      
      options: this.commonService.GetDropdown("UserAccessTypes"),
      field_placeholder: "Select User Access",
      conditionals: {
        "disable": {
          onValues: ["1"],
          controls: ["tUserName"]
        }
      },
      validator: [],
      mandatory: false,
      hidden: !needAccess
    },
    {
      field_name: "Login Name",
      fieldUniqeName: "tUserName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter User's Login Name using which user can login!",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Email",
      fieldUniqeName: "tEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter User Email",
      validator: [Validators.email],
      mandatory: false,
      hidden: false
    },{
      field_name: "Department",
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
      field_name: "Role",
      fieldUniqeName: "nRole",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,      
      options: this.commonService.GetDropdown("UserRole"),
      field_placeholder: "Enter User Role",
      validator: [],
      mandatory: false,
      hidden: needAccess
    },
    {
      field_name: "Assign Brands",
      fieldUniqeName: "rBrandID",
      defaultVal: "1",
      readOnly: false,
      invalid: false,
      field_type: FieldType.multidropdown,
      options: this.commonService.GetDropdown("Brand"),
      field_placeholder: "Select brand",
      validator: [],
      mandatory: true,
      hidden: false
    },
    {
      field_name: "Employee Id",
      fieldUniqeName: "tEmpID",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,      
      field_placeholder: "Enter User Emp Id",
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "Mobile Number",
      fieldUniqeName: "tMobile",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,      
      field_placeholder: "Enter User Mobile Number",
      validator: [],
      mandatory: false,
      hidden: true
    }]
    return fields;
  }
}
