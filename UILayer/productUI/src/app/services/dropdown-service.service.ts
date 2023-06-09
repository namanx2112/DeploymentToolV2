import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { DropwDown } from '../interfaces/models';
import { FieldType, Fields } from '../interfaces/home-tab';

@Injectable({
  providedIn: 'root'
})
export class DropdownServiceService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  Create(request: DropwDown) {
    return this.http.post<number>(this.configUrl + "Dropdown/Create", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: DropwDown) {
    return this.http.post<any>(this.configUrl + "Dropdown/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  Get(request: string) {
    // let params = new HttpParams();
    //     params = params.set('tModuleName', request);
    return this.http.get<DropwDown[]>(this.configUrl + "Dropdown/Get?tModuleName=" + request, { headers: this.authService.getHttpHeaders() });
  }

  Delete(request: number) {
    return this.http.get<number>(this.configUrl + "Dropdown/Delete?id=" + request, { headers: this.authService.getHttpHeaders() });
  }


  GetTableVisibleColumns() {
    return [
      "tModuleName",
      "tDropdownText",
      "nBrandId",
      "bDeleted"
    ];
  }

  GetAllModules() {
    let modules = ["ProjectType", "ConfigurationDriveThrou", "ConfigurationInsideDining", "StackHolderCD", "StackHolderITPM", "NetworkingStatus", "NetworkingPrimaryType"
      , "NetworkingBackupStatus", "NetworkingBackupType", "NetworkingTempStatus", "NetworkingTempType", "POSStatus", "POSPaperworkStatus", "AudioStatus", "AudioConfiguration", "AudioLoopStatus  ", "AudioLoopType",
      "ExteriorMenuStatus", "PaymentSystemType", "PaymentSystemBuyPassID", "PaymentSystemServerEPS", "PaymentSystemStatus", "InteriorMenuStatus",
      "SonicRaidoColors", "SonicRadioStatus", "InstallationStatus", "InstallationSignOffs", "InstallationTestTransactions", "SonicNoteType", "ProjectStatus",
      "UserDepartment", "UserRole", "City", "State", "Country"];
    return modules;
  }

  GetSearchFields(): Fields[] {
    let fields = [
      {
        field_name: "Section",
        fieldUniqeName: "tModuleName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Section",
        validator: [],
        mandatory: false,
        icon: "",
        hidden: false
      },
      {
        field_name: "Dropdown Text",
        fieldUniqeName: "tDropdownText",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Dropdown Text",
        validator: [],
        mandatory: true,
        hidden: false
      }
    ];
    return fields;
  }

  GetFields(): Fields[] {
    let fields = [
      {
        field_name: "aDropdownId",
        fieldUniqeName: "aDropdownId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DropdownId",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "nBrandId",
        fieldUniqeName: "nBrandId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nBrandId",
        validator: [],
        mandatory: false,
        icon: "search",
        hidden: true
      },
      {
        field_name: "Section",
        fieldUniqeName: "tModuleName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Section",
        validator: [],
        mandatory: false,
        icon: "",
        hidden: false
      },
      {
        field_name: "Dropdown Text",
        fieldUniqeName: "tDropdownText",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Dropdown Text",
        validator: [],
        mandatory: true,
        hidden: false
      }
    ];
    return fields;
  }
}