import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { VendorModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) {
  }

  Create(request: any) {
    return this.http.post<VendorModel>(CommonService.ConfigUrl + "Vendor/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<VendorModel>(CommonService.ConfigUrl + "Vendor/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: any | null){
    return this.http.post<VendorModel[]>(CommonService.ConfigUrl + "Vendor/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: VendorModel) {
    return this.http.get<VendorModel>(CommonService.ConfigUrl + "Vendor/Delete?id=" + request.aVendorId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTableVisibleColumns(){
    return [
      "tVendorName",
      "tVendorEmail",
      "tVendorPhone",
      "tVendorCategory"
    ];
  }

  GetSearchFields(): Fields[]{
    let fields = [
      {
        field_name: "Vendor Name",
        fieldUniqeName: "tVendorName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Name",
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
        field_name: "Vendor Id",
        fieldUniqeName: "aVendorId",
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
        field_name: "Name",
        fieldUniqeName: "tVendorName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Name",
        validator: [],
        mandatory: true,
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
        field_name: "Email",
        fieldUniqeName: "tVendorEmail",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.email,
        field_placeholder: "Enter Email",
        validator: [Validators.email],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Vendor Address",
        fieldUniqeName: "tVendorAddress",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Address",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Contact No",
        fieldUniqeName: "tVendorPhone",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Phone",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Website",
        fieldUniqeName: "tVendorWebsite",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Website",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Type",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("VendorType"),
        field_placeholder: "Enter Vendor Type",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("VendorStatus"),
        field_placeholder: "Enter Vendor Status",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Installation",
        fieldUniqeName: "nInstallation",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("YesNo"),
        field_placeholder: "Enter Installation",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Equipment",
        fieldUniqeName: "nEquipment",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("YesNo"),
        field_placeholder: "Enter Equipment",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Vendor Created By",
        fieldUniqeName: "nCreatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Vendor Created By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Vendor Updated By",
        fieldUniqeName: "nUpdatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Vendor Updated By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Vendor Created On",
        fieldUniqeName: "dtCreatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Vendor Created On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Vendor Updated On",
        fieldUniqeName: "dtUpdatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Vendor Updated On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Vendor Delted",
        fieldUniqeName: "bDeleted",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Vendor Deleted",
        validator: [],
        mandatory: false,
        hidden: true
      }
    ];
    return fields;
  }
}
