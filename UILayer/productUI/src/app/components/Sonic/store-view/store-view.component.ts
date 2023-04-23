import { Component } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-store-view',
  templateUrl: './store-view.component.html',
  styleUrls: ['./store-view.component.css']
})
export class StoreViewComponent {
  allTabs: HomeTab[];
  tabForUI: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  selectedTab: number;
  storeName: string;
  viewName: string;
  constructor(private service: SonicService) {
    this.initTab();
    this.viewName = "tabview";
  }

  initTab() {
    this.allTabs = this.service.GetStoretabs();
    this.tValues = {};
    this.tabForUI = [];
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      this.tValues[tTab.tab_name] = this.getValues(tTab);
      tTab = this.changeTab(tTab);
      if (parseInt(tIndx) > 2)
        this.tabForUI.push(tTab);
    }
    this.selectedTab = 3;
  }

  changeView(tName: string) {
    this.viewName = tName;
  }

  changeTab(tTab: HomeTab): HomeTab {
    if (tTab.tab_type == TabType.StoreContact) {
      this.storeName = this.tValues[tTab.tab_name]["tStoreName"];
      for (var indx in tTab.fields) {
        switch (tTab.fields[indx].fieldUniqeName) {
          case "tStoreAddressLine1":
          case "tStoreManager":
          case "tPOCPhone":
          case "tPOCEmail":
            tTab.fields[indx].hidden = false;
            break;
          default:
            tTab.fields[indx].hidden = true;
        }
      }
    }
    else if (tTab.tab_type == TabType.StoreConfiguration) {
      for (var indx in tTab.fields) {
      }
    }
    else if (tTab.tab_type == TabType.StoreStackHolder) {
      for (var indx in tTab.fields) {
        switch (tTab.fields[indx].fieldUniqeName) {
          case "nITPM":
          case "nCD":
            tTab.fields[indx].hidden = false;
            break;
          default:
            tTab.fields[indx].hidden = true;
        }
      }
    }
    return tTab;
  }

  getValues(tTab: HomeTab) {
    let values: any = {};
    for (var tIndx in tTab.fields) {
      values[tTab.fields[tIndx].fieldUniqeName] = "Dummy " + tTab.fields[tIndx].field_name;
    }
    return values;
  }

  onSubmit(tVal: any, tab: HomeTab) { }

  tabClick(tabIndex: number) {
    this.selectedTab = tabIndex;
  }
}
