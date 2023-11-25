import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { ProjectNotes } from '../interfaces/store';
import { CommonService } from './common.service';
import { FieldType, HomeTab, TabInstanceType, TabType } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor( private commonService: CommonService, private http: HttpClient, private cacheService: CacheService) {
  }

  GetTableVisibleColumns() {
    return [
      "dtCreatedOn",
       "nCreatedBy",
      "nNoteType",
      "tSource",
      "tNoteDesc"
    ];
  }

  Create(request: any) {
    return this.http.post<ProjectNotes>(CommonService.ConfigUrl + "Notes/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<ProjectNotes>(CommonService.ConfigUrl + "Notes/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: any | null) {
    return this.http.post<ProjectNotes[]>(CommonService.ConfigUrl + "Notes/Get", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: ProjectNotes) {
    return this.http.get<ProjectNotes>(CommonService.ConfigUrl + "Notes/Delete?id=" + request.aNoteID, { headers: this.cacheService.getHttpHeaders() });
  }

  

  GetNotesTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Notes",
      tab_header: "Notes",
      tTableName: "tblNotes",
      tab_type: TabType.StoreNotes,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "aNoteID",
        fieldUniqeName: "aNoteID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Notes Id",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aNoteID",
        fieldUniqeName: "aNoteID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Notes Id",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "nProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Project Id",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Date",
        fieldUniqeName: "dtCreatedOn",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Date",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "User",
        fieldUniqeName: "nCreatedBy",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Date",
        options: this.commonService.GetDropdown("User"),
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Type",
        fieldUniqeName: "nNoteType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("SonicNoteType"),
        field_placeholder: "Enter Type",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nStoreID",
        fieldUniqeName: "nStoreID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Type",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Source",
        fieldUniqeName: "tSource",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Source",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Note",
        fieldUniqeName: "tNoteDesc",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.textarea,
        field_placeholder: "Enter Note",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false,
      isTechComponent: false
    };
  }
}
