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
        count: 111
      },
      {
        title: "New project opening in next month",
        count: 231
      },
      {
        title: "New project opening in next month",
        count: 443
      },
      {
        title: "New project opening in next month",
        count: 332
      },
      {
        title: "New project opening in next month",
        count: 432
      },
      {
        title: "New project opening in next month",
        count: 111
      },
      {
        title: "New project opening in next month",
        count: 111
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

  GetNewStoresTab(instType: TabInstanceType): HomeTab{
    let fields = this.GetStoreContactFields();
    fields.push({
      field_name: "nProjectType",
      fieldUniqeName: "nProjectType",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Project Type",      
      options: this.commonService.GetDropdown("ProjectType"),
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "tStoreNumber",
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
      field_name: "tAddress",
      fieldUniqeName: "tAddress",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Address",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "nCity",
      fieldUniqeName: "nCity",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter City",      
      options: this.commonService.GetDropdown("City"),
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "tState",
      fieldUniqeName: "tState",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter State",
      validator: [],
      mandatory: false,
      hidden: false
    },
    {
      field_name: "nDMAID",
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
      field_name: "tDMA",
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
      field_name: "tRED",
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
      field_name: "tCM",
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
      field_name: "tANE",
      fieldUniqeName: "tANE",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter ANE",
      validator: [],
      mandatory: false,
      hidden: true
    },{
      field_name: "tRVP",
      fieldUniqeName: "tRVP",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter RVP",
      validator: [],
      mandatory: false,
      hidden: true
    },{
      field_name: "tPrincipalPartner",
      fieldUniqeName: "tPrincipalPartner",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Principal Partner",
      validator: [],
      mandatory: false,
      hidden: true
    },{
      field_name: "dStatus",
      fieldUniqeName: "dStatus",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter Status",
      validator: [],
      mandatory: false,
      hidden: true
    },{
      field_name: "dOpenStore",
      fieldUniqeName: "dOpenStore",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.date,
      field_placeholder: "Enter Golive Date",
      validator: [],
      mandatory: false,
      hidden: false
    },{
      field_name: "nProjectStatus",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: fields
    };
  }

  GetStoreContactFields(): Fields[]{
    let fArray = [{
      field_name: "aProjectStoreID",
      fieldUniqeName: "aProjectStoreID",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter aProjectStoreID",
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
    },{
      field_name: "Store Name",
      fieldUniqeName: "tStoreName",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Name",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Address1",
      fieldUniqeName: "tStoreAddressLine1",
      defaultVal: "",
      readOnly: true,
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
      readOnly: true,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Address2",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store City",
      fieldUniqeName: "nCity",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Store City",
      options: this.commonService.GetDropdown("City"),
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store State",
      fieldUniqeName: "nStoreState",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Store State",        
      options: this.commonService.GetDropdown("State"),
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Zip",
      fieldUniqeName: "tStoreZip",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Store Zip",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Manager",
      fieldUniqeName: "tStoreManager",
      defaultVal: "",
      readOnly: true,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Manager",
      validator: [Validators.required],
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
      validator: [Validators.required],
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
      validator: [Validators.required],
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
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store GC",
      fieldUniqeName: "tGC",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store GC",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store GC Phone",
      fieldUniqeName: "tGCPhone",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store GC Phone",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store GC Email",
      fieldUniqeName: "tGCEMail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter GC Email",
      validator: [Validators.required],
      mandatory: false,
      hidden: false
    }];
    return fArray;
  }

  GetStoreContactTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Contact",
      tab_header: "Store Contact",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: this.GetStoreContactFields()
    };
  }

  GetStoreConfigurationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Configuration",
      tab_header: "Store Configuration",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectConfigID",
        fieldUniqeName: "aProjectConfigID",
        defaultVal: "",
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
        hidden: false
      }, {
        field_name: "Project Type",
        fieldUniqeName: "nProjectType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Project Type",
        options: this.commonService.GetDropdown("ProjectType"),
        validator: [Validators.required],
        mandatory: false,
        hidden: false
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
        fieldUniqeName: "nDriveThrou",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Drive-Thru",
        options: this.commonService.GetDropdown("ConfigurationDriveThrou"),
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Ground Break",
        fieldUniqeName: "dGroundBreak",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Ground Break",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Kitchen Install",
        fieldUniqeName: "dKitchenInstall",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Kitchen Install",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Project Cost",
        fieldUniqeName: "cProjectCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectStakeHolderID",
        fieldUniqeName: "aProjectStakeHolderID",
        defaultVal: "",
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
        defaultVal: "",
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
        options: this.commonService.GetDropdown("Franchisee"),
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("StackHolderITPM"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "tRED",
        fieldUniqeName: "tRED",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter tRED",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "tCM",
        fieldUniqeName: "tCM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter tCM",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "tAandE",
        fieldUniqeName: "tAandE",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter tAandE",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "tPrincipalPartner",
        fieldUniqeName: "tPrincipalPartner",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter tPrincipalPartner",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectNetworkingID",
        field_group: "Primary",
        fieldUniqeName: "aProjectNetworkingID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectNetworkingID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nProjectID",
        field_group: "Primary",
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
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("NetworkingStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "dPrimaryDate",
        field_group: "Primary",
        fieldUniqeName: "dPrimaryDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter nPrimaryType",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nPrimaryType",
        field_group: "Primary",
        fieldUniqeName: "nPrimaryType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter nPrimaryType",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("NetworkingPrimaryType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nBackupStatus",
        field_group: "Backup",
        fieldUniqeName: "nBackupStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter nBackupStatus",
        validator: [Validators.required],
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
        field_placeholder: "Enter dBackupDate",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nBackupType",
        field_group: "Backup",
        fieldUniqeName: "nBackupType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter nBackupType",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("NetworkingBackupType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nTempStatus",
        field_group: "Temp",
        fieldUniqeName: "nTempStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter nTempStatus",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("NetworkingTempStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "dTempDate",
        field_group: "Temp",
        fieldUniqeName: "dTempDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dTempDate",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "nTempType",
        field_group: "Temp",
        fieldUniqeName: "nTempType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter nTempType",
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectPOSID",
        fieldUniqeName: "aProjectPOSID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter POS ID",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
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
      },{
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("POSPaperworkStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "nCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreAudioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Audio",
      tab_header: "Audio",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectAudioID",
        fieldUniqeName: "aProjectAudioID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectAudioID",
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
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Loop Status",
        fieldUniqeName: "dLoopStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Loop Status",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreExteriorMenusTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Exterior Menus",
      tab_header: "Exterior Menus",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectExteriorMenuID",
        fieldUniqeName: "aProjectExteriorMenuID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectExteriorMenuID",
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
      },{
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "nStalls",
        fieldUniqeName: "nStalls",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Stalls",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "FabCon Cost",
        fieldUniqeName: "cFabConCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter FabCon Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "ID Tech Cost",
        fieldUniqeName: "cIDTechCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ID Tech Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Total Cost",
        fieldUniqeName: "cTotalCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Total Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStorePaymentSystemTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Payment System",
      tab_header: "Payment System",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectPaymentSystemID",
        fieldUniqeName: "aProjectPaymentSystemID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectPaymentSystemID",
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
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },{
        field_name: "BuyPass ID",
        fieldUniqeName: "nBuyPassID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter BuyPass ID",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Cost",
        fieldUniqeName: "cCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }, {
        field_name: "nType",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter nType",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreInteriorMenusTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Interior Menus",
      tab_header: "Interior Menus",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectInteriorMenuID",
        fieldUniqeName: "aProjectInteriorMenuID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectInteriorMenuID",
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
      },{
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DMB Quantity",
        fieldUniqeName: "tDMB Quantity",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter DMB Quantity",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "nCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreSonicRadioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Sonic Radio",
      tab_header: "Sonic Radio",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectSonicRadioID",
        fieldUniqeName: "aProjectSonicRadioID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectSonicRadioID",
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
      },{
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Outdoor Speakers",
        fieldUniqeName: "tOutdoor Speakers",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Outdoor Speakers",
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("SonicRaidoColors"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Indoor Speakers",
        fieldUniqeName: "tIndoor Speakers",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Indoor Speakers",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Zones",
        fieldUniqeName: "tZones",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Zones",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Server Racks",
        fieldUniqeName: "tServer Racks",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Server Racks",
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("SonicRadioStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Delivery Date",
        fieldUniqeName: "dDelivery Date",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Delivery Date",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "nCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetStoreInsallationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Installation",
      tab_header: "Installation",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aProjectInstallationID",
        fieldUniqeName: "aProjectInstallationID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter aProjectInstallationID",
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
      },{
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("Vendor"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Lead Tech",
        fieldUniqeName: "tLead Tech",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Lead Tech",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Install Date",
        fieldUniqeName: "dInstall Date",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Install Date",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Install End",
        fieldUniqeName: "dInstall End",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Install End",
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("InstallationStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Signoffs",
        fieldUniqeName: "tSignoffs",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Signoffs",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Test Transactions",
        fieldUniqeName: "tTestTransactions",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Test Transactions",
        validator: [Validators.required],
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
        validator: [Validators.required],
        options: this.commonService.GetDropdown("ProjectStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Cost",
        fieldUniqeName: "nCost",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Cost",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetProjectsTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Projects",
      tab_header: "Projects",
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }

  GetNotesTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Notes",
      tab_header: "Notes",
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "aNotesId",
        fieldUniqeName: "aNotesId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Notes Id",
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
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
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }]
    };
  }
}
