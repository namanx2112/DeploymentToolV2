import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { DropdownModule, DropwDown } from '../interfaces/models';
import { FieldType, Fields } from '../interfaces/home-tab';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class DropdownServiceService {

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  Create(request: DropwDown) {
    return this.http.post<any>(CommonService.ConfigUrl + "Dropdown/Create", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Update(request: DropwDown) {
    return this.http.post<any>(CommonService.ConfigUrl + "Dropdown/Update", request, { headers: this.cacheService.getHttpHeaders() });
  }

  Get(request: string) {
    // let params = new HttpParams();
    //     params = params.set('tModuleName', request);
    return this.http.get<DropwDown[]>(CommonService.ConfigUrl + "Dropdown/Get?tModuleName=" + request, { headers: this.cacheService.getHttpHeaders() });
  }

  GetModules(nBrandId: number) {
    return this.http.get<DropdownModule[]>(CommonService.ConfigUrl + "Dropdown/GetModules?nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }

  Delete(request: number) {
    return this.http.get<number>(CommonService.ConfigUrl + "Dropdown/Delete?id=" + request, { headers: this.cacheService.getHttpHeaders() });
  }

  UpdateOrder(request: DropwDown[]){
    return this.http.post<any>(CommonService.ConfigUrl + "Dropdown/UpdateOrder", request, { headers: this.cacheService.getHttpHeaders() });
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
    let modules = ["ConfigurationDriveThrou", "ConfigurationInsideDining", "StackHolderCD", "StackHolderITPM", "NetworkingStatus", "NetworkingPrimaryType"
      , "NetworkingBackupStatus", "NetworkingBackupType", "NetworkingTempStatus", "NetworkingTempType", "POSStatus", "POSPaperworkStatus", "AudioStatus", "AudioConfiguration", "AudioLoopStatus", "AudioLoopType",
      "ExteriorMenuStatus", "PaymentSystemType", "PaymentSystemBuyPassID", "PaymentSystemServerEPS", "PaymentSystemStatus", "InteriorMenuStatus",
      "SonicRaidoColors", "SonicRadioStatus", "InstallationStatus", "InstallationSignOffs", "InstallationTestTransactions", "InstallationProjectStatus", "SonicNoteType", "ProjectStatus",
      "UserDepartment", "UserRole", "City", "State", "Country", "VendorType", "VendorStatus"];
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