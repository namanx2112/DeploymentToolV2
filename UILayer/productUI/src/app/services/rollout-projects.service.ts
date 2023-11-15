import { Injectable } from '@angular/core';
import { FieldType, Fields } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CacheService } from './cache.service';
import { CommonService } from './common.service';
import { RolloutProjects } from '../interfaces/projects';

@Injectable({
  providedIn: 'root'
})
export class RolloutProjectsService {

  constructor(private http: HttpClient, private cacheService: CacheService, private commonService: CommonService) { }

  GetTableVisibleColumns() {
    return [
      "tProjectsRolloutName",
      "tProjectsRolloutDescription",
      "nNumberOfStore",
      "tEstimateInstallTImePerStore",
      "dtStartDate",
      "dtEndDate"
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
    return this.http.get<RolloutProjects>(CommonService.ConfigUrl + "ProjectRollout/Delete?id=" + request.aProjectsRolloutID, { headers: this.cacheService.getHttpHeaders() });
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
      fieldUniqeName: "aProjectsRolloutID",
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
      fieldUniqeName: "tProjectsRolloutName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Project Name",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Project Description",
      fieldUniqeName: "tProjectsRolloutDescription",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Project Description",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Deployment Cost",
      fieldUniqeName: "cDeploymentCost",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.currency,
      field_placeholder: "Enter Deployment Cost",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Hardware Cost",
      fieldUniqeName: "cHardwareCost",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.currency,
      field_placeholder: "Enter Hardware Cost",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Brnad Id",
      fieldUniqeName: "nBrandID",
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
      field_name: "Rollout Project Status",
      fieldUniqeName: "nStatus",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      options: this.commonService.GetDropdown("ProjectRolloutStatus"),
      field_placeholder: "Enter Rollout Status",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "Estimate Install Time/Store",
      fieldUniqeName: "tEstimateInstallTImePerStore",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Estimate Install Time/Store",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "Start Date",
      fieldUniqeName: "dtStartDate",
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
      fieldUniqeName: "dtEndDate",
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
