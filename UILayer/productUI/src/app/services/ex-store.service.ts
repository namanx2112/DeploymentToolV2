import { Injectable } from '@angular/core';
import { DashboardTileType, DahboardTile } from '../interfaces/commons';
import { Observable } from 'rxjs';
import { FieldType, Fields, HomeTab, TabInstanceType, TabType } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { CommonService } from './common.service';
import { ActiveProject, HistoricalProjects, ProjectTypes, ProjectNotes, ProjectExcel, StoreSearchModel } from '../interfaces/store';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CacheService } from './cache.service';
import { BrandModel, Brands } from '../interfaces/models';
import { ChartType } from "chart.js";

@Injectable({
  providedIn: 'root'
})
export class ExStoreService {
  brands: BrandModel[];
  chartTypes: { [key: string]: ChartType } = {
    line: 'line',
    bar: 'bar',
    doughnut: 'doughnut',
    pie: 'pie'
  };
  constructor(private http: HttpClient, private commonService: CommonService, private cacheService: CacheService) {
  }


  UploadStore(fileToUpload: File, nBrandId: number) {
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name + String.fromCharCode(1000) + nBrandId);
    let httpHeader = new HttpHeaders({
      "Authorization": "Bearer " + this.cacheService.getToken()
    });
    return this.http.post<ProjectExcel[]>(CommonService.ConfigUrl + "Attachment/UploadStore", formData, { headers: httpHeader });
  }

  CreateNewStores(request: ProjectExcel[]) {
    return this.http.post<string>(CommonService.ConfigUrl + "ExStore/CreateNewStores", request, { headers: this.cacheService.getHttpHeaders() });
  }

  SearchStore(request: string, nBrandId: number) {
    return this.http.get<StoreSearchModel[]>(CommonService.ConfigUrl + "ExStore/SearchStore?searchText=" + request + "&nBrandId=" + nBrandId, { headers: this.cacheService.getHttpHeaders() });
  }


  getProjectType(tabType: HomeTab, curStore: StoreSearchModel) {
    let type: ProjectTypes = ProjectTypes.New;
    switch (tabType.tab_type) {
      case TabType.StorePOS:
        type = ProjectTypes.POSInstallation;
        break;
      case TabType.StoreAudio:
        type = ProjectTypes.AudioInstallation;
        break;
      case TabType.StoreExteriorMenus:
        type = ProjectTypes.ExteriorMenuInstallation;
        break;
      case TabType.StoreInteriorMenus:
        type = ProjectTypes.InteriorMenuInstallation;
        break;
      case TabType.StorePaymetSystem:
        type = ProjectTypes.PaymentTerminalInstallation;
        break;
      case TabType.StoreProjectServerHandheld:
        type = ProjectTypes.ServerHandheldInstallation;
        break;
      default:
        let indx = curStore.lstProjectsInfo.findIndex(x => x.nProjectType != ProjectTypes.POSInstallation && x.nProjectType != ProjectTypes.AudioInstallation &&
          x.nProjectType != ProjectTypes.ExteriorMenuInstallation && x.nProjectType != ProjectTypes.InteriorMenuInstallation && x.nProjectType != ProjectTypes.PartsReplacement && x.nProjectType != ProjectTypes.ServerHandheldInstallation)
        if (indx > -1) {
          type = curStore.lstProjectsInfo[indx].nProjectType;
        }
        break;
    }
    return type;
  }

  getProjectIdFromSearchStore(tabType: HomeTab, curStore: StoreSearchModel) {
    let nProjectId = 0;
    let tProjectType = this.getProjectType(tabType, curStore);
    let tIndx = curStore.lstProjectsInfo.findIndex(x => x.nProjectType == tProjectType)
    if (tIndx > -1) {
      nProjectId = curStore.lstProjectsInfo[tIndx].nProjectId;
    }
    return nProjectId;
  }

  // Delete(request: UserModel) {
  //   return this.http.delete<UserModel>(CommonService.ConfigUrl + "User/Delete?id=" + request.aUserID, { headers: this.cacheService.getHttpHeaders() });
  // }


  GetPOQuantityFields() {
    let items = [
      { name: "Store Configuration", fields: [{ title: "Stall Count", field: "nStallCount", tableName: "tblProjectConfig" }] },
      {
        name: "Exterior Menus", fields: [{ title: "Stalls", field: "nStalls", tableName: "tblProjectExteriorMenus" }, { title: "Patio", field: "nPatio", tableName: "tblProjectExteriorMenus" }, { title: "Flat", field: "nFlat", tableName: "tblProjectExteriorMenus" },
        { title: "DT POPS", field: "nDTPops", tableName: "tblProjectExteriorMenus" }, { title: "DT Menu", field: "nDTMenu", tableName: "tblProjectExteriorMenus" }]
      },
      { name: "Interior Menus", fields: [{ title: "DMB Quantity", field: "nDMBQuantity", tableName: "tblProjectInteriorMenus" }] },
      {
        name: "Payment Systems", fields: [{ title: "PAYS Units", field: "nPAYSUnits", tableName: "tblProjectPaymentSystem" }, { title: "45 Enclosures", field: "n45Enclosures", tableName: "tblProjectPaymentSystem" },
        { title: "90 Enclosures", field: "n90Enclosures", tableName: "tblProjectPaymentSystem" }, { title: "DT Enclosures", field: "nDTEnclosures", tableName: "tblProjectPaymentSystem" }, { title: "15 Sun Shields", field: "n15SunShields", tableName: "tblProjectPaymentSystem" },
        { title: "UPS", field: "nUPS", tableName: "tblProjectPaymentSystem" }, { title: "Shelf", field: "nShelf", tableName: "tblProjectPaymentSystem" }]
      },
      {
        name: "Sonic Radio", fields: [{ title: "Outdoor Speakers", field: "nOutdoorSpeakers", tableName: "tblProjectSonicRadio" }, { title: "Indoor Speakers", field: "nIndoorSpeakers", tableName: "tblProjectSonicRadio" },
        { title: "Zones", field: "nZones", tableName: "tblProjectSonicRadio" }, { title: "Server Racks", field: "nServerRacks", tableName: "tblProjectSonicRadio" }]
      },
      { name: "Installation", fields: [{ title: "Lead Tech", field: "tLeadTech", tableName: "tblProjectInstallation" }] },
      { name: "Server Handheld", fields: [{ title: "#Of Tablets Per Store", field: "nNumberOfTabletsPerStore", tableName: "tblProjectServerHandheld" }] },
    ];
    return items;
  }

  getPOTechConfigs() {
    return ["Audio", "POS", "Exterior Menus", "Installation", "Interior Menus", "Payment Systems", "Sonic Radio", "Server Handheld"];
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
          "tStoreNumber",
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
    else
      return [];
  }

  GetStoretabs(nBrandId: number): HomeTab[] {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == nBrandId);
    let tabs: HomeTab[];
    if (tBrand.nBrandType == Brands.Buffalo) {
      tabs = [
        this.GetStoreContactTab(TabInstanceType.Single),
        this.GetStoreConfigurationTab(TabInstanceType.Single),
        this.GetStoreStackholderTab(TabInstanceType.Single),
        this.GetStoreNetworingTab(TabInstanceType.Single),
        this.GetStorePOSTab(TabInstanceType.Single),
        this.GetStoreAudioTab(TabInstanceType.Single),
        this.GetStoreExteriorMenusTab(TabInstanceType.Single),
        this.GetStorePaymentSystemTab(TabInstanceType.Single),
        this.GetStoreInteriorMenusTab(TabInstanceType.Single),
        this.GetStoreServerHandheldTab(TabInstanceType.Single),
        this.GetStoreInsallationTab(TabInstanceType.Single)
      ];
    }
    else if (tBrand.nBrandType == Brands.Arby) {
      tabs = [
        this.GetStoreContactTab(TabInstanceType.Single),
        this.GetStoreConfigurationTab(TabInstanceType.Single),
        this.GetStoreStackholderTab(TabInstanceType.Single),
        this.GetStoreNetworingTab(TabInstanceType.Single),
        this.GetStorePOSTab(TabInstanceType.Single),
        this.GetStoreAudioTab(TabInstanceType.Single),
        this.GetStoreExteriorMenusTab(TabInstanceType.Single),
        this.GetStorePaymentSystemTab(TabInstanceType.Single),
        this.GetStoreInteriorMenusTab(TabInstanceType.Single),
        this.GetStoreRadioTab(TabInstanceType.Single),
        this.GetStoreNetworkSwitchTab(TabInstanceType.Single),
        this.GetStoreImageMemoryTab(TabInstanceType.Single),
        this.GetStoreInsallationTab(TabInstanceType.Single)
      ];
    }
    else if (tBrand.nBrandType == Brands.Dunkin) {
      tabs = [
        this.GetStoreContactTab(TabInstanceType.Single),
        this.GetStoreConfigurationTab(TabInstanceType.Single),
        this.GetStoreStackholderTab(TabInstanceType.Single),
        this.GetStoreNetworingTab(TabInstanceType.Single),
        this.GetStorePOSTab(TabInstanceType.Single),
        this.GetStoreAudioTab(TabInstanceType.Single),
        this.GetStoreExteriorMenusTab(TabInstanceType.Single),
        this.GetStorePaymentSystemTab(TabInstanceType.Single),
        this.GetStoreInteriorMenusTab(TabInstanceType.Single),
        this.GetStoreOrderAccuracyTab(TabInstanceType.Single),
        this.GetStoreOrderStatusBoardTab(TabInstanceType.Single),
        this.GetStoreInsallationTab(TabInstanceType.Single)
      ];
    }
    else {
      tabs = [
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
    }
    return tabs;
  }

  GetStoreTabsForProjectType(projectType: number, nBrandId: number): HomeTab[] {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == nBrandId);
    let tabs: HomeTab[] = [];
    switch (projectType) {
      case 5://POS
        tabs.push(this.GetStorePOSTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      case 6://Audio
        tabs.push(this.GetStoreAudioTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      case 7://Interior Menu
        tabs.push(this.GetStoreInteriorMenusTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      case 11://Exterior Menu
        tabs.push(this.GetStoreExteriorMenusTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      case 8://Payment
        tabs.push(this.GetStorePaymentSystemTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      case 10://Server Handheld
        tabs.push(this.GetStoreServerHandheldTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
      default:
        tabs.push(this.GetStoreAudioTab(TabInstanceType.Single));
        tabs.push(this.GetStoreNetworingTab(TabInstanceType.Single));
        tabs.push(this.GetStorePOSTab(TabInstanceType.Single));
        tabs.push(this.GetStoreExteriorMenusTab(TabInstanceType.Single));
        tabs.push(this.GetStorePaymentSystemTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInteriorMenusTab(TabInstanceType.Single));
        if (tBrand.nBrandType == Brands.Buffalo)
          tabs.push(this.GetStoreServerHandheldTab(TabInstanceType.Single));
        else if (tBrand.nBrandType == Brands.Arby)
          tabs.push(this.GetStoreRadioTab(TabInstanceType.Single));
        else
          tabs.push(this.GetStoreSonicRadioTab(TabInstanceType.Single));
        tabs.push(this.GetStoreInsallationTab(TabInstanceType.Single));
        break;
    }
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
      fields: [],
      my_service: "",
      needImport: false,
      isTechComponent: false
    };
  }

  GetAuditsTab(): HomeTab {
    return {
      tab_name: "Audit",
      tTableName: "",
      tab_header: "Audit",
      tab_type: TabType.AuditChanges,
      tab_unique_name: "",
      instanceType: TabInstanceType.Single,
      childTabs: [],
      search_fields: [],
      fields: [],
      my_service: "",
      needImport: false,
      isTechComponent: false
    };
  }

  GetNewStoresTab(instType: TabInstanceType): HomeTab {
    let fields = [];
    fields.push({
      field_name: "aStoreId",
      fieldUniqeName: "aStoreId",
      defaultVal: "0",
      readOnly: true,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter StoreId",
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
        validator: [Validators.required],
        mandatory: true,
        hidden: false
      },
      {
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
      },
      {
        field_name: "Go-live Date",
        fieldUniqeName: "dOpenStore",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Golive Date",
        validator: [Validators.required],
        mandatory: true,
        hidden: false
      },
      {
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
      },
      {
        field_name: "Address Line1",
        field_group: "Store Contact",
        fieldUniqeName: "tStoreAddressLine1",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Store Address",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
      field_name: "Address Line2",
      field_group: "Store Contact",
      fieldUniqeName: "tStoreAddressLine2",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Address2",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store City",
      field_group: "Store Contact",
      fieldUniqeName: "tCity",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store City",
      validator: [Validators.required],
      mandatory: true,
      hidden: false
    }, {
      field_name: "Store State",
      field_group: "Store Contact",
      fieldUniqeName: "nStoreState",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      field_placeholder: "Enter Store State",
      options: this.commonService.GetDropdown("State"),
      validator: [Validators.required],
      mandatory: true,
      hidden: false
    }, {
      field_name: "Store Zip",
      field_group: "Store Contact",
      fieldUniqeName: "tStoreZip",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Store Zip",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store Manager",
      field_group: "Store Contact",
      fieldUniqeName: "tStoreManager",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Manager",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Store POC",
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
      fieldUniqeName: "tPOCEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter Store POC Email",
      validator: [Validators.email],
      mandatory: false,
      hidden: false
    }, {
      field_name: "General Contractor",
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_name: "General Contractor Email",
      field_group: "Store Contact",
      fieldUniqeName: "tGCEMail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter GC Email",
      validator: [Validators.email],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Company",
      field_group: "Bill to",
      fieldUniqeName: "tBillToCompany",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Company",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Address",
      field_group: "Bill to",
      fieldUniqeName: "tBillToAddress",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter GC Email",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "City",
      field_group: "Bill to",
      fieldUniqeName: "tBillToCity",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter City",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "State",
      field_group: "Bill to",
      fieldUniqeName: "nBillToState",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      options: this.commonService.GetDropdown("State"),
      field_placeholder: "Enter State",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Zip",
      field_group: "Bill to",
      fieldUniqeName: "tBillToZip",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Zip",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Email",
      field_group: "Bill to",
      fieldUniqeName: "tBillToEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Email",
      validator: [],
      mandatory: false,
      hidden: false
    },
      {
        field_name: "DMA ID",
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
      fields: fields,
      my_service: "",
      needImport: false,
      isTechComponent: false
    };
  }

  GetStoreContactFields(readOnly: boolean): Fields[] {
    let fArray = [{
      field_name: "Store Id",
      fieldUniqeName: "aStoreID",
      defaultVal: "0",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Store Id",
      validator: [],
      mandatory: false,
      hidden: true
    },
    {
      field_name: "nBrandID",
      fieldUniqeName: "nBrandID",
      defaultVal: "0",
      readOnly: false,
      invalid: false,
      field_type: FieldType.number,
      field_placeholder: "Enter Brand Id",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Store Number",
      fieldUniqeName: "tStoreNumber",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Number",
      validator: [],
      mandatory: false,
      hidden: true
    }, {
      field_name: "Store Name",
      field_group: "Store Contact",
      fieldUniqeName: "tStoreName",
      defaultVal: "",
      readOnly: readOnly,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Store Name",
      validator: [],
      mandatory: false,
      hidden: true// Hiding for now as requested by Client
    }, {
      field_name: "Address Line1",
      field_group: "Store Contact",
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
      field_name: "Address Line2",
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_group: "Store Contact",
      fieldUniqeName: "tPOCEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter Store POC Email",
      validator: [Validators.email],
      mandatory: false,
      hidden: false
    }, {
      field_name: "General Contractor",
      field_group: "Store Contact",
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
      field_group: "Store Contact",
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
      field_name: "General Contractor Email",
      field_group: "Store Contact",
      fieldUniqeName: "tGCEMail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.email,
      field_placeholder: "Enter GC Email",
      validator: [Validators.email],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Company",
      field_group: "Bill to",
      fieldUniqeName: "tBillToCompany",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Company",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Address",
      field_group: "Bill to",
      fieldUniqeName: "tBillToAddress",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter GC Email",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "City",
      field_group: "Bill to",
      fieldUniqeName: "tBillToCity",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter City",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "State",
      field_group: "Bill to",
      fieldUniqeName: "nBillToState",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.dropdown,
      options: this.commonService.GetDropdown("State"),
      field_placeholder: "Enter State",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Zip",
      field_group: "Bill to",
      fieldUniqeName: "tBillToZip",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Zip",
      validator: [],
      mandatory: false,
      hidden: false
    }, {
      field_name: "Email",
      field_group: "Bill to",
      fieldUniqeName: "tBillToEmail",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Email",
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
      tTableName: "tblStore",
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
      fields: this.GetStoreContactFields(false),
      my_service: "",
      needImport: false,
      isTechComponent: false
    };
  }

  GetStoreConfigurationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Configuration",
      tab_header: "Store Configuration",
      tTableName: "tblProjectConfig",
      tab_type: TabType.StoreConfiguration,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      isTechComponent: false,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("ConfigurationDriveThrou"),
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
        field_type: FieldType.currency,
        field_placeholder: "Enter Project Cost",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
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
      isTechComponent: false,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        fieldUniqeName: "tCD",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter CD",
        // options: this.commonService.GetDropdown("StackHolderCD"),
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "IT PM",
        fieldUniqeName: "tITPM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter IT PM",
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
        hidden: true
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
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreNetworingTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Networking",
      tab_header: "Networking",
      tTableName: "tblProjectNetworking",
      tab_type: TabType.StoreNetworking,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        field_name: "dDateFor_nPrimaryStatus",
        field_group: "Primary",
        fieldUniqeName: "dDateFor_nPrimaryStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nPrimaryStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nBackupStatus",
        field_group: "Primary",
        fieldUniqeName: "dDateFor_nBackupStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nBackupStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nTempStatus",
        field_group: "Primary",
        fieldUniqeName: "dDateFor_nTempStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nTempStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Date",
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
        field_name: "Type",
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
        field_name: "Status",
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
        field_group: "Backup",
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
        field_name: "Type",
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
        field_name: "Status",
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
        field_name: "Date",
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
        field_name: "Type",
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
      }],
      my_service: "",
      needImport: false
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
      isTechComponent: true,
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
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        field_group: "Primary",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nPaperworkStatus",
        field_group: "Primary",
        fieldUniqeName: "dDateFor_nPaperworkStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nPaperworkStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreAudioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Audio",
      tab_header: "Audio",
      tTableName: "tblProjectAudio",
      tab_type: TabType.StoreAudio,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nLoopStatus",
        fieldUniqeName: "dDateFor_nLoopStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "dDateFor_nLoopStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },],
      my_service: "",
      needImport: false
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
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStorePaymentSystemTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Payment System",
      tab_header: "Payment System",
      tTableName: "tblProjectPaymentSystem",
      tab_type: TabType.StorePaymetSystem,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("PaymentSystemStatus"),
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
      },
      {
        field_name: "dDateFor_nBuyPassID",
        fieldUniqeName: "dDateFor_nBuyPassID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nBuyPassID",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nServerEPS",
        fieldUniqeName: "dDateFor_nServerEPS",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nServerEPS",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
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
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreRadioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Radio",
      tab_header: "Radio",
      tTableName: "tblProjectSonicRadio",
      tab_type: TabType.StoreSonicRadio,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreSonicRadioTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Sonic Radio",
      tab_header: "Sonic Radio",
      tTableName: "tblProjectSonicRadio",
      tab_type: TabType.StoreSonicRadio,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreInsallationTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Installation",
      tab_header: "Installation",
      tTableName: "tblProjectInstallation",
      tab_type: TabType.StoreInstallation,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("InstallationStatus"),
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
        field_name: "Install Time",
        fieldUniqeName: "dInstallTime",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.time,
        field_placeholder: "Enter Install Time",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Install Tech Number",
        fieldUniqeName: "tInstallTechNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Install Tech Number",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Manager Name",
        fieldUniqeName: "tManagerName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Manager Name",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Manager Number",
        fieldUniqeName: "tManagerNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Manager Number",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Manager Checkout",
        fieldUniqeName: "nManagerCheckout",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Manager Checkout",
        options: this.commonService.GetDropdown("InstallationManagerCheckout"),
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Photo Deliverables",
        fieldUniqeName: "nPhotoDeliverables",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Photo Deliverables",
        options: this.commonService.GetDropdown("InstallationPhotoDeliverables"),
        validator: [],
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
        options: this.commonService.GetDropdown("InstallationProjectStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Revisit Date",
        fieldUniqeName: "dRevisitDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Revisit Date",
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
      }, {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "dDateFor_nProjectStatus",
        fieldUniqeName: "dDateFor_nProjectStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter dDateFor_nProjectStatus",
        validator: [],
        mandatory: false,
        hidden: true
      },
      {
        field_name: "Install Notes",
        fieldUniqeName: "tInstallNotes",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.textarea,
        field_placeholder: "Enter Install Notes",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
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
      isTechComponent: false,
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
        field_name: "Store Number",
        fieldUniqeName: "tStoreNumber",
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
        field_name: "Project Go-live Date",
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
        field_name: "Previous Project Manager",
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
        field_name: "Project Manager",
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
      }],
      my_service: "",
      needImport: false
    };
  }

  GetHistoricalProjectsTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Historical Projects",
      tab_header: "Historical Projects",
      tTableName: "tblProject",
      tab_type: TabType.HistoricalProjects,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        fieldUniqeName: "tStoreNumber",
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
        field_name: "Project End Date",
        fieldUniqeName: "dProjEndDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Store End Date",
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
        field_name: "Vendor",
        fieldUniqeName: "tVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter New Vendor",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreServerHandheldTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Server Handheld",
      tab_header: "Server Handheld",
      tTableName: "tblProjectServerHandheld",
      tab_type: TabType.StoreProjectServerHandheld,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: true,
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
        field_name: "aServerHandheldId",
        fieldUniqeName: "aServerHandheldId",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ServerHandheldId",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("SeverHandheldStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Ship Date",
        fieldUniqeName: "dShipDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Ship Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Shipping Carrier",
        fieldUniqeName: "tShippingCarrier",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Shipping Carrier",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Tracking Number",
        fieldUniqeName: "tTrackingNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Tracking Number",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Tablets",
        fieldUniqeName: "nNumberOfTabletsPerStore",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Tablets Per Store",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "5 Bay Charger",
        fieldUniqeName: "n5BayCharger",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter 5 Bay Charger",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Shoulder Strap",
        fieldUniqeName: "nShoulderStrap",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Shoulder Strap",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Protective Case",
        fieldUniqeName: "nProtectiveCase",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Protective Case",
        validator: [],
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
      },
      {
        field_name: "dDateFor_nStatus",
        fieldUniqeName: "dDateFor_nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "dDateFor_nStatus",
        validator: [],
        mandatory: false,
        hidden: true
      }],
      my_service: "",
      needImport: false
    };
  }

  //#region Arbys
  GetStoreNetworkSwitchTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Network Switch",
      tab_header: "Network Switch",
      tTableName: "tblProjectNetworkSwitch",
      tab_type: TabType.StoreNetworkSwitch,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        field_name: "NetworkSwitchId",
        fieldUniqeName: "aNetworkSwitchId",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter NetworkSwitchID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("NetworkSwitchStatus"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "Shipment to Vendor",
        fieldUniqeName: "nShipmenttoVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Shipment to vendor",
        validator: [],
        options: this.commonService.GetDropdown("NetworkSwitchShipmenttoVendor"),
        mandatory: false,
        hidden: false
      }, {
        field_name: "Setup Status",
        fieldUniqeName: "nSetupStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Setup Status",
        validator: [],
        options: this.commonService.GetDropdown("NetworkSwitchSetupStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "New Serial Number",
        fieldUniqeName: "tNewSerialNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter New Serial Number",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Old Serial Number ",
        fieldUniqeName: "tOldSerialNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Old Serial Number ",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }

  GetStoreImageMemoryTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Image Memory",
      tab_header: "Image Memory",
      tTableName: "tblProjectImageMemory",
      tab_type: TabType.StoreImageMemory,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        field_name: "Image Memory Id",
        fieldUniqeName: "aImageMemoryId",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter ImageMemoryId",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("ImageMemoryStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "HP Shipment Tracking",
        fieldUniqeName: "tHPShipmentTracking",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter HP Shipment Tracking",
        validator: [],
        mandatory: false,
        hidden: false
      }, {
        field_name: "Return Shipment",
        fieldUniqeName: "nReturnShipment",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Shipment to vendor",
        validator: [],
        options: this.commonService.GetDropdown("ImageMemoryReturnShipment"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Return Shipment Tracking",
        fieldUniqeName: "tReturnShipmentTracking",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Return Shipment Tracking",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }
  //#endregion

  //#region Dunkin
  GetStoreOrderAccuracyTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Order Accuracy",
      tab_header: "Order Accuracy",
      tTableName: "tblProjectOrderAccuracy",
      tab_type: TabType.StoreOrderAccuracy,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        field_name: "Order Accuracy ID",
        fieldUniqeName: "aProjectOrderAccuracyID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Order Accuracy ID",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("OrderAccuracyStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Bakery Printer",
        fieldUniqeName: "nBakeryPrinter",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Bakery Printer",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Dual Cup Label",
        fieldUniqeName: "nDualCupLabel",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Dual Cup Label",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DT Expo",
        fieldUniqeName: "nDTExpo",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "DT Expo",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "FC Expo",
        fieldUniqeName: "nFCExpo",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Dual FC Expo",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Ship Date",
        fieldUniqeName: "dShipDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Ship Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Shipping Carrier",
        fieldUniqeName: "tShippingCarrier",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Shipping Carrier",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Tracking Number",
        fieldUniqeName: "tTrackingNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Tracking Number",
        validator: [],
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
        field_placeholder: "Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }
  GetStoreOrderStatusBoardTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Order StatusBoard",
      tab_header: "Order StatusBoard",
      tTableName: "tblProjectOrderStatusBoard",
      tab_type: TabType.StoreOrderStatusBoard,
      tab_unique_name: "",
      instanceType: instType,
      isTechComponent: false,
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
        field_name: "Order Status Board",
        fieldUniqeName: "aProjectOrderStatusBoardID",
        defaultVal: "0",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Order Status Board",
        validator: [],
        mandatory: false,
        hidden: true
      }, {
        field_name: "nStoreId",
        fieldUniqeName: "nStoreId",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter StoreId",
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
        options: this.commonService.GetDropdown("OrderAccuracyStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "OSB",
        fieldUniqeName: "nOSB",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter OSB",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Ship Date",
        fieldUniqeName: "dShipDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Dual Ship Date",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Shipping Carrier",
        fieldUniqeName: "tShippingCarrier",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Shipping Carrier",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Tracking Number",
        fieldUniqeName: "tTrackingNumber",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Tracking Number",
        validator: [],
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
        field_placeholder: "Delivery Date",
        validator: [],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }
  //#endregion
}
