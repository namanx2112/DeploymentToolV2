import { Injectable } from '@angular/core';
import { SonicProjectHighlights } from '../interfaces/commons';
import { Observable } from 'rxjs';
import { FieldType, Fields, HomeTab, TabInstanceType, TabType } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { CommonService } from './common.service';
import { SonicNotes, SonicProjectExcel, StoreProjects, StoreSearchModel } from '../interfaces/sonic';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SonicService {
  configUrl: any;
  constructor(private http: HttpClient, private commonService: CommonService, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  UploadStore({ fileToUpload }: { fileToUpload: File; }) {
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.authService.getToken()
    });
    return this.http.post<SonicProjectExcel[]>(this.configUrl + "Attachment/UploadStore", formData, { headers: httpHeader });
  }

  CreateNewStores(request: SonicProjectExcel[]) {
    return this.http.post<string>(this.configUrl + "Sonic/CreateNewStores", request, { headers: this.authService.getHttpHeaders() });
  }

  SearchStore(request: string) {
    return this.http.get<StoreSearchModel[]>(this.configUrl + "Sonic/SearchStore?searchText=" + request, { headers: this.authService.getHttpHeaders() });
  }

  GetTableVisibleColumns(tab: HomeTab) {
    if (tab.tab_type == TabType.StoreProjects) {
      if (tab.tab_header == "Historical Projects") {
        return [
          "nStoreNo",
          "dProjectGoliveDate",
          "tProjectType",
          "dProjEndDate",
          "tProjManager",
          "tOldVendor"
        ];
      }
      else {
        return [
          "nStoreNo",
          "dProjectGoliveDate",
          "tProjectType",
          "tStatus",
          "tPrevProjManager",
          "tProjManager",
          "tOldVendor",
          "tNewVendor"
        ];
      }
    }
    else if (tab.tab_type == TabType.StoreNotes) {
      return [
        "dNotesDate",
        "tType",
        "tSource",
        "tNote"
      ];
    }
    else
      return [];
  }


  Get(fields: Fields, tab: HomeTab) {
    if (tab.tab_type == TabType.StoreProjects)
      return this.getProjects(fields);
    else
      return this.getNotes();

  }

  getNotes() {
    return new Observable<SonicNotes[]>((obj) => {
      let items: SonicNotes[] = [
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "General",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "POPS",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "TEGS",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "DFDDSD",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        },
        {
          aNotesId: 1005,
          dNotesDate: new Date(),
          tType: "RRSD",
          tSource: "Clark Kent",
          tNote: "Mr. Guterres said peace is needed today more than ever before, as war and conflict unleash devastating poverty and hunger, forcing tens of millions from their homes. The entire planet is battling climate chaos, and even peaceful countries are facing “gaping inequalities and political polarization”, he added. "
        }
      ];
      obj.next(items);
    });
  }

  getProjects(request: any) {
    //return this.http.post<StoreProjects>(this.configUrl + "User/CreateUser", request, { headers: this.authService.getHttpHeaders() });
    return new Observable<StoreProjects[]>((obj) => {
      let items: StoreProjects[] = [
        {
          nStoreNo: 1005,
          tProjectType: "Audio Installation",
          tStatus: "On Track",
          tPrevProjManager: "Clark Kent",
          tProjManager: "Berry Alien",
          dProjectGoliveDate: new Date(),
          dProjEndDate: new Date(),
          tOldVendor: "ABC Inc",
          tNewVendor: "HME"
        },
        {
          nStoreNo: 1005,
          tProjectType: "POS Installation",
          tStatus: "Action Required",
          tPrevProjManager: "Tony Kent",
          tProjManager: "Ben Alien",
          dProjectGoliveDate: new Date(),
          dProjEndDate: new Date(),
          tOldVendor: "ABC Inc",
          tNewVendor: "HME"
        },
        {
          nStoreNo: 1005,
          tProjectType: "Parts Replacement",
          tStatus: "At Risk",
          tPrevProjManager: "Berry Kent",
          tProjManager: "Kent Alien",
          dProjectGoliveDate: new Date(),
          dProjEndDate: new Date(),
          tOldVendor: "XYZ Inc",
          tNewVendor: "HXX"
        },
        {
          nStoreNo: 1005,
          tProjectType: "Audio Installation",
          tStatus: "On Track",
          tPrevProjManager: "Clark Kent",
          tProjManager: "Berry Alien",
          dProjectGoliveDate: new Date(),
          dProjEndDate: new Date(),
          tOldVendor: "ABC Inc",
          tNewVendor: "HME"
        },
        {
          nStoreNo: 1005,
          tProjectType: "Display Installation",
          tStatus: "On Track",
          tPrevProjManager: "Clark Kent",
          tProjManager: "Berry Alien",
          dProjectGoliveDate: new Date(),
          dProjEndDate: new Date(),
          tOldVendor: "ABC Inc",
          tNewVendor: "HME"
        }
      ];
      obj.next(items);
    });
  }

  GetProjecthighlights() {
    return new Observable<SonicProjectHighlights[]>((obj) => {
      let items = [{
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      },
      {
        title: "New project opening in next month",
        count: 0
      }];
      obj.next(items);
    });
  }

  GetStoretabs(): HomeTab[] {
    let tabs = [
      this.GetStoreContactTab(TabInstanceType.Single),
      this.GetStoreConfigurationTab(TabInstanceType.Single),
      this.GetStoreStackholderTab(TabInstanceType.Single),
      this.GetStoreNetworingTab(TabInstanceType.Single),
      this.GetStorePOSTab(TabInstanceType.Single),
      this.GetStoreAudioTab(TabInstanceType.Single),
      this.GetStoreExteriorMenusTab(TabInstanceType.Single),
      this.GetStorePaymentSystemTab(TabInstanceType.Single),
      this.GetStoreInteriorMenusTab(TabInstanceType.Single),
      this.GetStoreSonicRadioTab(TabInstanceType.Single),
      this.GetStoreInsallationTab(TabInstanceType.Single)
    ];
    return tabs;
  }

  GetPOSInstallationTabs(): HomeTab[] {
    let tabs = [
      this.GetStoreContactTab(TabInstanceType.Single),
      this.GetStoreStackholderTab(TabInstanceType.Single),
      this.GetStorePOSTab(TabInstanceType.Single)
    ];
    return tabs;
  }

  GetAudioInstallationTabs(): HomeTab[] {
    let tabs = [
      this.GetStoreContactTab(TabInstanceType.Single),
      this.GetStoreStackholderTab(TabInstanceType.Single),
      this.GetStoreAudioTab(TabInstanceType.Single)
    ];
    return tabs;
  }

  GetMenuInstallationTabs(): HomeTab[] {
    let tabs = [
      this.GetStoreContactTab(TabInstanceType.Single),
      this.GetStoreStackholderTab(TabInstanceType.Single),
      this.GetStoreExteriorMenusTab(TabInstanceType.Single)
    ];
    return tabs;
  }

  GetPaymentTerminalInstallationTabs(): HomeTab[] {
    let tabs = [
      this.GetStoreContactTab(TabInstanceType.Single),
      this.GetStoreStackholderTab(TabInstanceType.Single),
      this.GetStorePaymentSystemTab(TabInstanceType.Single)
    ];
    return tabs;
  }

  GetSearchStoresTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Select Store",
      tTableName: "",
      tab_header: "Select Store",
      tab_type: TabType.SearchStore,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [],
      fields: []
    };
  }

  GetNewStoresTab(instType: TabInstanceType): HomeTab {
    let fields = this.GetStoreContactFields(false);
    fields.push({
      field_name: "nStoreId",
      fieldUniqeName: "nStoreId",
      defaultVal: "0",
      readOnly: true,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter nStoreId",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "ProjectType",
      fieldUniqeName: "nProjectType",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Project Type",
      options: this.commonService.GetDropdown("ProjectType"),
      validator: [],
      mandatory: false,
      hidden: false
    },
      {
        field_name: "StoreNumber",
        fieldUniqeName: "tStoreNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter StoreNumber",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DMAID",
        fieldUniqeName: "nDMAID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DMA ID",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DMA",
        fieldUniqeName: "tDMA",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter DMA Name",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "RED",
        fieldUniqeName: "tRED",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter RED",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "CM",
        fieldUniqeName: "tCM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter CM",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "ANE",
        fieldUniqeName: "tANE",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter ANE",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
      field_name: "RVP",
      fieldUniqeName: "tRVP",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter RVP",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Principal Partner",
      fieldUniqeName: "tPrincipalPartner",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Principal Partner",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Status",
      fieldUniqeName: "dStatus",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter Status",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Go-live Date",
      fieldUniqeName: "dOpenStore",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter Golive Date",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Project Status",
      fieldUniqeName: "nProjectStatus",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      options: this.commonService.GetDropdown("ProjectStatus"),
      field_placeholder: "Enter Project Status",
      validator: [],
      mandatory: false,
      hidden: false
    });

    return {
      tab_name: "New Store",
      tab_header: "New Store",
      tTableName: "",
      tab_type: TabType.NewStore,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Store Name",
        fieldUniqeName: "tStoreName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Store Name",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: fields
    };
  }

  GetStoreContactFields(readOnly: boolean): Fields[] {
    let fArray = [{
      field_name: "aProjectStoreID",
      fieldUniqeName: "aProjectStoreID",
      defaultVal: "0",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter aProjectStoreID",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "ProjectID",
      fieldUniqeName: "nProjectID",
      defaultVal: "0",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter nProjectID",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Store Name",
      fieldUniqeName: "tStoreName",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Name",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Address1",
      fieldUniqeName: "tStoreAddressLine1",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Address",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Address2",
      fieldUniqeName: "tStoreAddressLine2",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Address2",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store City",
      fieldUniqeName: "tCity",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store City",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store State",
      fieldUniqeName: "nStoreState",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Store State",
      options: this.commonService.GetDropdown("State"),
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Zip",
      fieldUniqeName: "tStoreZip",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Store Zip",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Manager",
      fieldUniqeName: "tStoreManager",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Manager",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store POC",
      fieldUniqeName: "tPOC",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store POC",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store POC Phone",
      fieldUniqeName: "tPOCPhone",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store POC Phone",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store POC Email",
      fieldUniqeName: "tPOCEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter Store POC Email",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "General Contractor",
      fieldUniqeName: "tGC",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store GC",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "General Contractor Phone",
      fieldUniqeName: "tGCPhone",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store GC Phone",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "General ContractorEmail",
      fieldUniqeName: "tGCEMail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter GC Email",
      validator: [],
      mandatory: false,
      hidden: false
    }];
    return fArray;
  }

  GetStoreContactTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Contact",
      tab_header: "Store Contact",
      tTableName: "tblProjectStores",
      tab_type: TabType.StoreContact,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Store Name",
        fieldUniqeName: "tStoreName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Store Name",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: this.GetStoreContactFields(false)
    };
  }

  GetStoreConfigurationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Configuration",
      tab_header: "Store Configuration",
      tTableName: "tblProjectConfigs",
      tab_type: TabType.StoreConfiguration,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Stall Count",
        fieldUniqeName: "nStallCount",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Stall Count",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectConfigID",
        fieldUniqeName: "aProjectConfigID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectConfigID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Stall Count",
        fieldUniqeName: "nStallCount",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Stall Count",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Drive-Thru",
        fieldUniqeName: "nDriveThru",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Drive-Thru",
        options: this.commonService.GetDropdown("ConfigurationDriveThru"),
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Inside Dining",
        fieldUniqeName: "nInsideDining",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Inside Dining",
        options: this.commonService.GetDropdown("ConfigurationInsideDining"),
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Ground Break",
        fieldUniqeName: "nGroundBreak",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Ground Break",
        options: this.commonService.GetDropdown("ConfigurationGroundBreak"),
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Kitchen Install",
        fieldUniqeName: "nKitchenInstall",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Kitchen Install",
        options: this.commonService.GetDropdown("ConfigurationKitchenInstall"),
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Project Cost",
        fieldUniqeName: "cProjectCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Project Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreStackholderTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Stake Holders",
      tab_header: "Stake Holders",
      tTableName: "tblProjectStakeHolders",
      tab_type: TabType.StoreStackHolder,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Franchise",
        fieldUniqeName: "nFranchise",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectStakeHolderID",
        fieldUniqeName: "aProjectStakeHolderID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Franchise",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Franchise",
        fieldUniqeName: "nFranchisee",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Franchise",
        options: this.commonService.GetDropdown("Franchise"),
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "RVP",
        fieldUniqeName: "tRVP",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter RVP",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "FBC",
        fieldUniqeName: "tFBC",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter FBC",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "CD",
        fieldUniqeName: "nCD",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter CD",
        options: this.commonService.GetDropdown("StackHolderCD"),
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "IT PM",
        fieldUniqeName: "nITPM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter IT PM",
        validator: [],
        options: this.commonService.GetDropdown("StackHolderITPM"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "RED",
        fieldUniqeName: "tRED",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter RED",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "CM",
        fieldUniqeName: "tCM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter CM",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "AandE",
        fieldUniqeName: "tAandE",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter AandE",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Principal Partner",
        fieldUniqeName: "tPrincipalPartner",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Principal Partner",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreNetworingTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Networking",
      tab_header: "Networking",
      tTableName: "tblProjectNetworkings",
      tab_type: TabType.StoreNetworking,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectNetworkingID",
        field_group: "Primary",
        fieldUniqeName: "aProjectNetworkingID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectNetworkingID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        field_group: "Primary",
        fieldUniqeName: "nProjectID",
        defaultVal: "o",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        field_group: "Primary",
        fieldUniqeName: "nPrimaryStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Primary Date",
        field_group: "Primary",
        fieldUniqeName: "dPrimaryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter PrimaryType",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Primary Type",
        field_group: "Primary",
        fieldUniqeName: "nPrimaryType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter PrimaryType",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingPrimaryType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Backup Status",
        field_group: "Backup",
        fieldUniqeName: "nBackupStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Backup Status",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingBackupStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Date",
        field_group: "dBackupDate",
        fieldUniqeName: "dBackupDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Backup Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Backup Type",
        field_group: "Backup",
        fieldUniqeName: "nBackupType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Backup Type",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingBackupType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Temp Status",
        field_group: "Temp",
        fieldUniqeName: "nTempStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Temp Status",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingTempStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Temp Date",
        field_group: "Temp",
        fieldUniqeName: "dTempDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Temp Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Temp Type",
        field_group: "Temp",
        fieldUniqeName: "nTempType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Temp Type",
        validator: [],
        options: this.commonService.GetDropdown("NetworkingTempType"),
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStorePOSTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "POS",
      tab_header: "POS",
      tTableName: "tblProjectPOS",
      tab_type: TabType.StorePOS,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectPOSID",
        fieldUniqeName: "aProjectPOSID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter POS ID",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "nrojectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Config Date",
        fieldUniqeName: "dConfigDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Config Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Support Date",
        fieldUniqeName: "dSupportDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Support Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("POSStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Paperwork Status",
        fieldUniqeName: "nPaperworkStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Paperwork Status",
        validator: [],
        options: this.commonService.GetDropdown("POSPaperworkStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreAudioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Audio",
      tab_header: "Audio",
      tTableName: "tblProjectAudios",
      tab_type: TabType.StoreAudio,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectAudioID",
        fieldUniqeName: "aProjectAudioID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectAudioID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("AudioStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Configuration",
        fieldUniqeName: "nConfiguration",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Configuration",
        validator: [],
        options: this.commonService.GetDropdown("AudioConfiguration"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Loop Status",
        fieldUniqeName: "nLoopStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Loop Status",
        validator: [],
        options: this.commonService.GetDropdown("AudioLoopStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Loop Type",
        fieldUniqeName: "nLoopType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Loop Type",
        validator: [],
        options: this.commonService.GetDropdown("AudioLoopType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Loop Delivery Date",
        fieldUniqeName: "dLoopDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Loop Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreExteriorMenusTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Exterior Menus",
      tab_header: "Exterior Menus",
      tTableName: "tblProjectExteriorMenus",
      tab_type: TabType.StoreExteriorMenus,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectExteriorMenuID",
        fieldUniqeName: "aProjectExteriorMenuID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectExteriorMenuID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "Stalls",
        fieldUniqeName: "nStalls",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Stalls",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Patio",
        fieldUniqeName: "nPatio",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Patio",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Flat",
        fieldUniqeName: "nFlat",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nFlat",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DT Pops",
        fieldUniqeName: "nDTPops",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DT Pops",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DT Menu",
        fieldUniqeName: "nDTMenu",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DT Menu",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("ExteriorMenuStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "FabCon Cost",
        fieldUniqeName: "cFabConCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter FabCon Cost",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "ID Tech Cost",
        fieldUniqeName: "cIDTechCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter ID Tech Cost",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Total Cost",
        fieldUniqeName: "cTotalCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Total Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStorePaymentSystemTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Payment System",
      tab_header: "Payment System",
      tTableName: "tblProjectPaymentSystems",
      tab_type: TabType.StorePaymetSystem,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectPaymentSystemID",
        fieldUniqeName: "aProjectPaymentSystemID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectPaymentSystemID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "BuyPass ID",
        fieldUniqeName: "nBuyPassID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter BuyPass ID",
        validator: [],
        options: this.commonService.GetDropdown("PaymentSystemBuyPassID"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "ServerEPS",
        fieldUniqeName: "nServerEPS",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter ServerEPS",
        validator: [],
        options: this.commonService.GetDropdown("PaymentSystemServerEPS"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("PaymentSystemPaymentSystemStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "PAYS Units",
        fieldUniqeName: "nPAYSUnits",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter PAYS Units",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "45 Enclosures",
        fieldUniqeName: "n45Enclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter 45 Enclosures",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "90 Enclosures",
        fieldUniqeName: "n90Enclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter 90 Enclosures",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "DT Enclosures",
        fieldUniqeName: "nDTEnclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DT Enclosures",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "15 Sun Shields",
        fieldUniqeName: "n15SunShields",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter 15 Sun Shields",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "UPS",
        fieldUniqeName: "nUPS",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter UPS",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Shelf",
        fieldUniqeName: "nShelf",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Shelf",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Type",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nType",
        options: this.commonService.GetDropdown("PaymentSystemType"),
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreInteriorMenusTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Interior Menus",
      tab_header: "Interior Menus",
      tTableName: "tblProjectInteriorMenus",
      tab_type: TabType.StoreInteriorMenus,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectInteriorMenuID",
        fieldUniqeName: "aProjectInteriorMenuID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectInteriorMenuID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DMB Quantity",
        fieldUniqeName: "nDMBQuantity",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter DMB Quantity",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("InteriorMenuStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreSonicRadioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Sonic Radio",
      tab_header: "Sonic Radio",
      tTableName: "tblProjectSonicRadios",
      tab_type: TabType.StoreSonicRadio,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectSonicRadioID",
        fieldUniqeName: "aProjectSonicRadioID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectSonicRadioID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Outdoor Speakers",
        fieldUniqeName: "nOutdoorSpeakers",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Outdoor Speakers",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Colors",
        fieldUniqeName: "nColors",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Colors",
        validator: [],
        options: this.commonService.GetDropdown("SonicRaidoColors"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Indoor Speakers",
        fieldUniqeName: "nIndoorSpeakers",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Indoor Speakers",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Zones",
        fieldUniqeName: "nZones",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Zones",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Server Racks",
        fieldUniqeName: "nServerRacks",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Server Racks",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("SonicRadioStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDeliveryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreInsallationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Installation",
      tab_header: "Installation",
      tTableName: "tblProjectInstallations",
      tab_type: TabType.StoreInstallation,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "ProjectInstallationID",
        fieldUniqeName: "aProjectInstallationID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectInstallationID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "ProjectID",
        fieldUniqeName: "nProjectID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ProjectID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Lead Tech",
        fieldUniqeName: "tLeadTech",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Lead Tech",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Install Date",
        fieldUniqeName: "dInstallDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Install Date",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Install End",
        fieldUniqeName: "dInstallEnd",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Install End",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [],
        options: this.commonService.GetDropdown("InstallationStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Signoffs",
        fieldUniqeName: "nSignoffs",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Signoffs",
        options: this.commonService.GetDropdown("InstallationSignOffs"),
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Test Transactions",
        fieldUniqeName: "nTestTransactions",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Test Transactions",
        options: this.commonService.GetDropdown("InstallationTestTransactions"),
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Project Status",
        fieldUniqeName: "nProjectStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Project Status",
        validator: [],
        options: this.commonService.GetDropdown("ProjectStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.currency,
        field_placeholder: "Enter Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetProjectsTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Projects",
      tab_header: "Projects",
      tTableName: "tblProject",
      tab_type: TabType.StoreProjects,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Vendor",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "Store No",
        fieldUniqeName: "nStoreNo",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Store No",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Project Type",
        fieldUniqeName: "tProjectType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Store Type",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        fieldUniqeName: "tStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Status",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Prev Proj Manager",
        fieldUniqeName: "tPrevProjManager",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Prev Proj Manager",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Proj Manager",
        fieldUniqeName: "tProjManager",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Proj Manager",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Project GoliveDate",
        fieldUniqeName: "dProjectGoliveDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter ProjectGoliveDate",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Old Vendor",
        fieldUniqeName: "tOldVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Old Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "New Vendor",
        fieldUniqeName: "tNewVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter New Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
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
        field_name: "aNotesId",
        fieldUniqeName: "aNotesId",
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
        field_name: "aNotesId",
        fieldUniqeName: "aNotesId",
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
        field_name: "Date",
        fieldUniqeName: "dNotesDate",
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
        field_name: "Type",
        fieldUniqeName: "tType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        options: this.commonService.GetDropdown("SonicNoteType"),
        field_placeholder: "Enter Type",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Source",
        fieldUniqeName: "tSource",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Source",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Note",
        fieldUniqeName: "tNote",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.textarea,
        field_placeholder: "Enter Note",
        validator: [],
        mandatory: false,
        hidden: false
      }]
    };
  }
}
