import { Injectable } from '@angular/core';
import { FieldType, Fields } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CacheService } from './cache.service';
import { CommonService } from './common.service';
import { RolloutProjects } from '../interfaces/store';

@Injectable({
  providedIn: 'root'
})
export class RolloutProjectsService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) { }

  GetTableVisibleColumns() {
    return [
      "nTechComponent",
      "nEquipmentVendor",
      "nInstallationVendor",
      "dStartDate",
      "dEndDate"
    ];
  }

  Create(request: any) {
    return this.http.post<RolloutProjects>(CommonService.ConfigUrl + "ProjectRollout/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<RolloutProjects>(CommonService.ConfigUrl + "ProjectRollout/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: any | null) {
    return this.http.post<RolloutProjects[]>(CommonService.ConfigUrl + "ProjectRollout/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: RolloutProjects) {
    return this.http.get<RolloutProjects>(CommonService.ConfigUrl + "ProjectRollout/Delete?id=" + request.aRolloutProjectId, { headers: this.cacheService.getHttpHeaders() });
  }


  GetSearchFields(): Fields[] {
    let fields = [{
      field_name: "Project Name",
      fieldUniqeName: "tProjectName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Project Name",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }]
    return fields;
  }
  GetFields(): Fields[] {
    let fields = [{
      field_name: "Rollout Project Id",
      fieldUniqeName: "aRolloutProjectId",
      defaultVal: "0",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Project Id",
      validator: [Validators.required],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Project Name",
      fieldUniqeName: "tProjectName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Project Name",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Brnad Id",
      fieldUniqeName: "nBrandId",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Brand Id",
      validator: [Validators.required],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "Technology Component",
      fieldUniqeName: "nTechComponent",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      options: this.commonService.GetDropdown("TechComponent"),
      field_placeholder: "Enter Technology Component",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Equipment Vendor",
      fieldUniqeName: "nEquipmentVendor",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Equipment Vendor",
      options: this.commonService.GetDropdown("Vendor"),
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Installation Vendor",
      fieldUniqeName: "nInstallationVendor",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Installation Vendor",
      options: this.commonService.GetDropdown("Vendor"),
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "Start Date",
      fieldUniqeName: "dStartDate",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter Start Date",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "End Date",
      fieldUniqeName: "dEndDate",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter End Date",
      validator: [],
      mandatory: false,
      hidden: false
    }];
    return fields;
  }
}
