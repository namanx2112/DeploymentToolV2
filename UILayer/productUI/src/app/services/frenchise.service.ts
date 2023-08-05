import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { FranchiseModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class FranchiseService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) {
  }

  Create(request: any) {
    return this.http.post<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: FranchiseModel | null){
    return this.http.post<FranchiseModel[]>(CommonService.ConfigUrl + "Franchise/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: FranchiseModel) {
    return this.http.get<FranchiseModel>(CommonService.ConfigUrl + "Franchise/Delete?id=" + request.aFranchiseId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTableVisibleColumns(){
    return [
      "tFranchiseName",
      "tFranchiseEmail",
      "tFranchiseAddress",
      "nFranchiseState"
    ];
  }

  GetSearchFields(): Fields[]{
    let fields = [
      {
        field_name: "Franchise Name",
        fieldUniqeName: "tFranchiseName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Name",
        validator: [],
        mandatory: false,
        icon: "search",
        hidden: false
      }
    ];
    return fields;
  }

  GetFields(): Fields[]{
    let fields = [
      {
        field_name: "Franchise Id",
        fieldUniqeName: "aFranchiseId",
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
        field_name: "Franchise Name",
        fieldUniqeName: "tFranchiseName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Name",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Email ID",
        fieldUniqeName: "tFranchiseEmail",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.email,
        field_placeholder: "Enter Franchise Email",
        validator: [Validators.email],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Address Line 1",
        fieldUniqeName: "tFranchiseAddress",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Address Line 1",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Address Line 2",
        fieldUniqeName: "tFranchiseAddress2",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Address Line 2",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "City",
        fieldUniqeName: "tFranchiseCity",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter City",
        validator: [],
        mandatory: false,
        hidden: false
      },{
        field_name: "State",
        fieldUniqeName: "nFranchiseState",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("State"),
        field_placeholder: "Enter State",
        validator: [],
        mandatory: false,
        hidden: false
      },{
        field_name: "Zip Code",
        fieldUniqeName: "tFranchiseZip",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Zip Code",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Phone No.",
        fieldUniqeName: "tFranchisePhone",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Phone",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brand Id",
        fieldUniqeName: "nBrandID",
        defaultVal: "1",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brand Id",
        validator: [],
        mandatory: true,
        hidden: true
      },
      {
        field_name: "Franchise Created By",
        fieldUniqeName: "nCreatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise Created By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Franchise Updated By",
        fieldUniqeName: "nUpdatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise Updated By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Franchise Created On",
        fieldUniqeName: "dtCreatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Franchise Created On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Franchise Updated On",
        fieldUniqeName: "dtUpdatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Franchise Updated On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Franchise Delted",
        fieldUniqeName: "bDeleted",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise Deleted",
        validator: [],
        mandatory: false,
        hidden: true
      }
    ];
    return fields;
  }
}
