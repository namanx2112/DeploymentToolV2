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
      tab_name: "Store Configuratio",
      tab_header: "Store Configuratio",
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
        fieldUniqeName: "nFranchise",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Franchise",
        validator: [Validators.required],
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
        fieldUniqeName: "nVendor",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Vendor",
        validator: [Validators.required],
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
      }]
    };
  }

  GetStoreExteriorMenusTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Contact",
      tab_header: "Store Contact",
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
      }]
    };
  }

  GetStorePaymentSystemTab(instType: TabInstanceType): HomeTab {
    return {
      tab_name: "Store Contact",
      tab_header: "Store Contact",
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
      }]
    };
  }
}
