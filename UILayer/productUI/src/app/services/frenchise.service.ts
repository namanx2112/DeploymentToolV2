import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { FranchiseModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class FranchiseService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService, private commonService: CommonService) {
    this.configUrl = authService.getConfigUrl();
  }

  Create(request: any) {
    return this.http.post<FranchiseModel>(this.configUrl + "Franchise/CreateFranchise", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<FranchiseModel>(this.configUrl + "Franchise/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  Get(request: FranchiseModel | null){
    return this.http.post<FranchiseModel[]>(this.configUrl + "Franchise/GetFranchises", request, { headers: this.authService.getHttpHeaders() });
  }

  GetTableVisibleColumns(){
    return [
      "tFranchiseName",
      "tFranchiseEmail",
      "tFranchiseOwner",
      "tFranchiseLocation"
    ];
  }

  GetSearchFields(): Fields[]{
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
        icon: "search",
        hidden: false
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
        mandatory: false,
        icon: "search",
        hidden: false
      },
      {
        field_name: "Brand Id",
        fieldUniqeName: "nBrandID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brand Id",
        validator: [],
        mandatory: false,
        icon: "search",
        hidden: false
      },
      {
        field_name: "Franchise Email",
        fieldUniqeName: "tFranchiseEmail",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.email,
        field_placeholder: "Enter Franchise Email",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Owner",
        fieldUniqeName: "tFranchiseOwner",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Owner",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Phone",
        fieldUniqeName: "tFranchisePhone",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Phone",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Page Size",
        fieldUniqeName: "nPageSize",
        defaultVal: "10",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Page Size",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Page Number",
        fieldUniqeName: "nPageNumber",
        defaultVal: "1",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Page Number",
        validator: [],
        mandatory: false,
        hidden: true
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
        field_name: "Franchise Franchise Description",
        fieldUniqeName: "tFranchiseDescription",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Franchise Description",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brand Id",
        fieldUniqeName: "nBrandID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brand Id",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Location",
        fieldUniqeName: "tFranchiseLocation",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Location",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Email",
        fieldUniqeName: "tFranchiseEmail",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.email,
        field_placeholder: "Enter Franchise Email",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Address",
        fieldUniqeName: "tFranchiseAddress",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Address",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Phone",
        fieldUniqeName: "tFranchisePhone",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Phone",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Contact",
        fieldUniqeName: "tFranchiseContact",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Contact",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Owner",
        fieldUniqeName: "tFranchiseOwner",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Owner",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Franchise EmployeeCount",
        fieldUniqeName: "nFranchiseEmployeeCount",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise Franchise EmployeeCount",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Franchise Revenue",
        fieldUniqeName: "nFranchiseRevenue",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise Franchise Franchise Revenue",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Franchise Country",
        fieldUniqeName: "tFranchiseCountry",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Franchise Country",
        validator: [Validators.required],
        options: this.commonService.GetCountryDropdowns(),
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Established",
        fieldUniqeName: "dFranchiseEstablished",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Franchise Established",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Franchise Category",
        fieldUniqeName: "tFranchiseCategory",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise Category",
        validator: [],
        mandatory: true,
        hidden: false
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
