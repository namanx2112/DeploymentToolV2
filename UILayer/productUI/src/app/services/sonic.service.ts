import { Injectable } from '@angular/core';
import { SonicProjectHighlights } from '../interfaces/commons';
import { Observable } from 'rxjs';
import { FieldType, HomeTab, TabInstanceType, TabType } from '../interfaces/home-tab';
import { Validators } from '@angular/forms';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class SonicService {

  constructor(private commonService: CommonService) { }

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

  GetStoretabs(): HomeTab[]{
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
      fields: [{
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
      },{
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
      },{
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
      },{
        field_name: "Store City",
        fieldUniqeName: "tCity",
        defaultVal: "",
        readOnly: true,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Store City",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "Store State",
        fieldUniqeName: "nStoreState",
        defaultVal: "",
        readOnly: true,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Store State",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
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
      },{
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
      },{
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
      },{
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
      },{
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
      },{
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
      },{
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
      },{
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
      }]
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
        field_name: "Project Type",
        fieldUniqeName: "nProjectType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Project Type",
        options: this.commonService.GetDropdown("nProjectType"),
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
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
      },
      {
        field_name: "Drive-Thru",
        fieldUniqeName: "nDriveThrou",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Drive-Thru",
        options: this.commonService.GetDropdown("nDriveThrou"),
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
        options: this.commonService.GetDropdown("nInsideDining"),
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "Ground Break",
        fieldUniqeName: "tGroundBreak",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Ground Break",
        validator: [],
        mandatory: false,
        hidden: false
      },{
        field_name: "Kitchen Install",
        fieldUniqeName: "tKitchenInstall",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Kitchen Install",
        validator: [],
        mandatory: false,
        hidden: false
      },{
        field_name: "Project Cost",
        fieldUniqeName: "nProjectCost",
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
        field_name: "Franchise",
        fieldUniqeName: "tFranchise",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise",
        validator: [Validators.required],
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
        options: this.commonService.GetDropdown("nCD"),
        validator: [],
        mandatory: false,
        hidden: false
      },{
        field_name: "IT PM",
        fieldUniqeName: "nITPM",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter IT PM",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nITPM"),
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
        field_name: "Vendor",
        field_group: "Primary",
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        field_group: "Primary",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Date",
        field_group: "Primary",
        fieldUniqeName: "dDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Date",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Type",
        field_group: "Primary",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Type",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        field_group: "Backup",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Date",
        field_group: "Backup",
        fieldUniqeName: "dDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Date",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Type",
        field_group: "Backup",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Type",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nType"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Status",
        field_group: "Temp",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nStatus"),
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Date",
        field_group: "Temp",
        fieldUniqeName: "dDate",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.date,
        field_placeholder: "Enter Date",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Type",
        field_group: "Temp",
        fieldUniqeName: "nType",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Type",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nType"),
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
        options: this.commonService.GetDropdown("nStatus"),
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
        options: this.commonService.GetDropdown("nPaperworkStatus"),
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
        options: this.commonService.GetDropdown("nStatus"),
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
        options: this.commonService.GetDropdown("nConfiguration"),
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
        options: this.commonService.GetDropdown("dLoopStatus"),
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
        options: this.commonService.GetDropdown("nLoopType"),
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
      },
      {
        field_name: "Patio",
        fieldUniqeName: "tPatio",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Patio",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Flat",
        fieldUniqeName: "tFlat",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Flat",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DT Pops",
        fieldUniqeName: "tDTPops",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter DT Pops",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "DT Menu",
        fieldUniqeName: "tDTMenu",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
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
        options: this.commonService.GetDropdown("nStatus"),
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
        fieldUniqeName: "nFabConCost",
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
        fieldUniqeName: "nIDTechCost",
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
        fieldUniqeName: "nTotalCost",
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
      },{
        field_name: "BuyPass ID",
        fieldUniqeName: "nBuyPassID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter BuyPass ID",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nBuyPassID"),
        mandatory: false,
        hidden: false
      },{
        field_name: "ServerEPS",
        fieldUniqeName: "nServerEPS",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter ServerEPS",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nServerEPS"),
        mandatory: false,
        hidden: false
      },{
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nStatus"),
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
        fieldUniqeName: "tPAYS Units",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter PAYS Units",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "45 Enclosures",
        fieldUniqeName: "t45 Enclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter 45 Enclosures",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "90 Enclosures",
        fieldUniqeName: "t90 Enclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter 90 Enclosures",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "DT Enclosures",
        fieldUniqeName: "tDT Enclosures",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter DT Enclosures",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "15 Sun Shields",
        fieldUniqeName: "t15 Sun Shields",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter 15 Sun Shields",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "UPS",
        fieldUniqeName: "tUPS",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter UPS",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "Shelf",
        fieldUniqeName: "tShelf",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Shelf",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      },{
        field_name: "Cost",
        fieldUniqeName: "Cost",
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
        options: this.commonService.GetDropdown("nStatus"),
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
        options: this.commonService.GetDropdown("nColors"),
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
        options: this.commonService.GetDropdown("nStatus"),
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
      },{
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
      },{
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
        options: this.commonService.GetDropdown("nStatus"),
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
        field_name: "Status",
        fieldUniqeName: "nStatus",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Status",
        validator: [Validators.required],
        options: this.commonService.GetDropdown("nStatus"),
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
}
