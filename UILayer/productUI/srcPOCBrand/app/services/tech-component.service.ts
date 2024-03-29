import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { TechComponentModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class TechComponenttService {

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  Create(request: any) {
    return this.http.post<TechComponentModel>(CommonService.ConfigUrl + "TechComponent/CreateTechComponent", request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetTableVisibleColumns(){
    return [
      "tTechComponentName",
      "tComponentType"
    ];
  }

  Update(request: any) {
    return this.http.post<TechComponentModel>(CommonService.ConfigUrl + "TechComponent/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: TechComponentModel | null){
    return this.http.post<TechComponentModel[]>(CommonService.ConfigUrl + "TechComponent/GetTechComponents", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: TechComponentModel) {
    return this.http.get<TechComponentModel>(CommonService.ConfigUrl + "TechComponent/Delete?id=" + request.aTechComponentId, { headers: this.cacheService.getHttpHeaders() });
  }

  GetSearchFields(): Fields[]{
    let fields = [{
      field_name: "TechComponent Name",
      fieldUniqeName: "tTechComponentName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter TechComponent Name",
      validator: [],
      mandatory: true,
      hidden: false,
      icon: "search"
    }
    ];
    return fields;
  }

  GetFields(): Fields[]{
    let fields = [
      {
        field_name: "TechComponent Id",
        fieldUniqeName: "aTechComponentId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter TechComponent Id",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "TechComponent Name",
        fieldUniqeName: "tTechComponentName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter TechComponent Name",
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
        field_name: "TechComponent Description",
        fieldUniqeName: "tTechComponentDescription",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter TechComponent Description",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "TechComponent Type",
        fieldUniqeName: "tComponentType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter TechComponent Type",
        validator: [],
        mandatory: true,
        hidden: false
      },
      {
        field_name: "TechComponent Created By",
        fieldUniqeName: "nCreatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter TechComponent Created By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "TechComponent Updated By",
        fieldUniqeName: "nUpdateBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter TechComponent Updated By",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "TechComponent Created On",
        fieldUniqeName: "dtCreatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter TechComponent Created On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "TechComponent Updated On",
        fieldUniqeName: "dtUpdatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter TechComponent Updated On",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "TechComponent Delted",
        fieldUniqeName: "bDeleted",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter TechComponent Deleted",
        validator: [],
        mandatory: false,
        hidden: true
      }
    ];
    return fields;
  }
}
