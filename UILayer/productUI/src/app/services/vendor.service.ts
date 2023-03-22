import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { VendorModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService, private commonService: CommonService) {
    this.configUrl = authService.getConfigUrl();
  }

  CreateVendor(request: any) {
    return this.http.post<VendorModel>(this.configUrl + "Vendor/CreateVendor", request, { headers: this.authService.getHttpHeaders() });
  }

  UpdateVendor(request: any) {
    return this.http.post<VendorModel>(this.configUrl + "Vendor/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetVendors(request: VendorModel | null){
    return this.http.post<VendorModel[]>(this.configUrl + "Vendor/GetVendors", request, { headers: this.authService.getHttpHeaders() });
  }

  GetVendorSearchFields(): Fields[]{
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
        icon: "search",
        hidden: false
      },
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
        field_name: "Vendor TechComponent ID",
        fieldUniqeName: "nTechComponentID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Vendor TechComponent ID",
        validator: [],
        mandatory: false,
        icon: "search",
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

  GetVendorFields(): Fields[]{
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
        field_name: "Vendor Name",
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
        field_name: "Vendor TechComponent ID",
        fieldUniqeName: "nTechComponentID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Vendor TechComponent ID",
        validator: [],
        mandatory: true,
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
        field_name: "Vendor Description",
        fieldUniqeName: "tVendorDescription",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Description",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Vendor Email",
        fieldUniqeName: "tVendorEmail",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.email,
        field_placeholder: "Enter Vendor Email",
        validator: [],
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
        hidden: false
      },
      {
        field_name: "Vendor Phone",
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
        field_name: "Vendor Contact Person",
        fieldUniqeName: "tVendorContactPerson",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Contact Person",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Vendor Website",
        fieldUniqeName: "tVendorWebsite",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Website",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Vendor Country",
        fieldUniqeName: "tVendorCountry",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor Country",
        validator: [Validators.required],
        options: this.commonService.GetCountryDropdowns(),
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Vendor Established",
        fieldUniqeName: "tVendorEstablished",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Vendor Established",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Vendor Category",
        fieldUniqeName: "tVendorCategory",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Category",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Vendor Contact",
        fieldUniqeName: "tVendorContact",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor Contact",
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
