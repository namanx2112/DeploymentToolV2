import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { PartsModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class PartsService {

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  GetTableVisibleColumns() {
    return [
      "tPartDesc",
      "tPartNumber",
      "cPrice"
    ];
  }

  Create(request: any) {
    return this.http.post<PartsModel>(CommonService.ConfigUrl + "VendorParts/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<PartsModel>(CommonService.ConfigUrl + "VendorParts/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: any | null) {
    return this.http.post<PartsModel[]>(CommonService.ConfigUrl + "VendorParts/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: PartsModel) {
    return this.http.get<PartsModel>(CommonService.ConfigUrl + "VendorParts/Delete?id=" + request.aPartID, { headers: this.cacheService.getHttpHeaders() });
  }

  GetSearchFields(): Fields[] {
    let fields = [{
      field_name: "Parts Description",
      fieldUniqeName: "tPartDesc",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Description",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
  GetFields(): Fields[] {
    let fields = [{
      field_name: "Parts Id",
      fieldUniqeName: "aPartID",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Id",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
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
      field_name: "Parts Description",
      fieldUniqeName: "tPartDesc",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Parts Description",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Parts Number",
      fieldUniqeName: "tPartNumber",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Parts Number",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Parts Price",
      fieldUniqeName: "cPrice",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.currency,
      field_placeholder: "Enter Parts Price",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
}
