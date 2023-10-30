import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { BrandModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { Observable } from 'rxjs';
import { Dictionary } from '../interfaces/commons';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class BrandServiceService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) {
  }

  //Create
  //Update
  //Get
  //GetSearchFields
  //GetFields
  //GetTableVisibleColumns

  Create(request: any) {
    return this.http.post<BrandModel>(CommonService.ConfigUrl + "Brand/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<BrandModel>(CommonService.ConfigUrl + "Brand/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: BrandModel) {
    return this.http.get<BrandModel>(CommonService.ConfigUrl + "Brand/Delete?id=" + request.aBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(searchFields: Dictionary<string> | null) {
    return this.http.post<BrandModel[]>(CommonService.ConfigUrl + "Brand/Get", searchFields, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTableVisibleColumns() {
    return [
      "tBrandName",
      "tBrandWebsite",
      "tBrandCountry",
      "tBrandContact"
    ];
  }

  GetSearchFields(): Fields[] {
    let fields = [{
      field_name: "Brands Name",
      fieldUniqeName: "tBrandName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Brands Name",
      validator: [],
      mandatory: false,
      hidden: false,
      icon: "search"
    }]
    return fields;
  }

  GetFields(): Fields[] {
    let fields = [
      {
        field_name: "Brand Id",
        fieldUniqeName: "aBrandId",
        defaultVal: "",
        readOnly: true,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brands Id",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Brands Name",
        fieldUniqeName: "tBrandName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Name",
        validator: [Validators.required],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Brands Domain",
        fieldUniqeName: "tBrandDomain",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Domain",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands Address",
        fieldUniqeName: "tBrandAddressLine1",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Address",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands Address2",
        fieldUniqeName: "tBrandAddressLine2",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Address",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands City",
        fieldUniqeName: "tBrandCity",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands City",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("City")
      },
      {
        field_name: "Brands State",
        fieldUniqeName: "nBrandState",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands State",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("State")
      },
      {
        field_name: "Brands Country",
        fieldUniqeName: "nBrandCountry",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands Country",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("Country")
      },
      {
        field_name: "Brands Zip",
        fieldUniqeName: "tBrandZipCode",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Zip",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands nBrandLogoAttachmentID",
        fieldUniqeName: "nBrandLogoAttachmentID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brands nBrandLogoAttachmentID",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Brands Created By",
        fieldUniqeName: "nCreatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brands Created By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Brands Updated by",
        fieldUniqeName: "nUpdateBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brands Updated by",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Brands Created on",
        fieldUniqeName: "dtCreatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Brands Created on",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Brands Updated on",
        fieldUniqeName: "dtUpdatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Brands Updated on",
        validator: [],
        mandatory: false,
        hidden: true
      }
    ];
    return fields;
  }
}
