import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Fields, FieldType } from '../interfaces/home-tab';
import { PartsModel } from '../interfaces/models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class PartsService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  GetTableVisibleColumns(){
    return [
      "tPartsName",
      "nPartsNumber",
      "nPartsPrice"
    ];
  }

  Create(request: any) {
    return this.http.post<PartsModel>(this.configUrl + "Parts/CreateParts", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<PartsModel>(this.configUrl + "Parts/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  Get(request: PartsModel | null) {
    return this.http.post<PartsModel[]>(this.configUrl + "Parts/GetPartss", request, { headers: this.authService.getHttpHeaders() });
  }

  GetSearchFields(): Fields[] {
    let fields = [{
      field_name: "Parts Number",
      fieldUniqeName: "nPartsNumber",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Id",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
  GetFields(): Fields[] {
    let fields = [{
      field_name: "Parts Id",
      fieldUniqeName: "aPartsId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Id",
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "Parts Name",
      fieldUniqeName: "tPartsName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Parts Name",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Parts Number",
      fieldUniqeName: "nPartsNumber",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Number",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Parts Price",
      fieldUniqeName: "nPartsPrice",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Parts Price",
      validator: [],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
}
