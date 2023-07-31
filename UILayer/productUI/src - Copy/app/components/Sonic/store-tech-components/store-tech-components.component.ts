import { Component, Input } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { Fields, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSearchModel, StoreSonicRadio, StoreStackholders } from 'src/app/interfaces/sonic';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-store-tech-components',
  templateUrl: './store-tech-components.component.html',
  styleUrls: ['./store-tech-components.component.css']
})
export class StoreTechComponentsComponent {
  _element: any;
  @Input()
  public set element(value: any) {
    this._element = value;
    this.initTab();
  }
  allTabs: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  constructor(private service: SonicService, private techCompService: AllTechnologyComponentsService) {

  }
  initTab() {
    if (typeof this._element.nProjectType != 'undefined')
      this.allTabs = this.service.GetStoreTabsForProjectType(this._element.nProjectType);
    else
      this.allTabs = this.service.GetStoretabs();
    this.tValues = {};
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      this.getValues(tTab);
    }
  }

  getValues(tabType: HomeTab) {
    let searchField: Dictionary<string> = { "nProjectID": this._element.nProjectId.toString() };
    switch (tabType.tab_type) {
      case TabType.StoreContact:
        this.techCompService.GetStoreContact(searchField).subscribe((x: StoreContact[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreConfiguration:
        this.techCompService.GetStoreConfig(searchField).subscribe((x: StoreConfiguration[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreStackHolder:
        this.techCompService.GetStackholders(searchField).subscribe((x: StoreStackholders[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreNetworking:
        this.techCompService.GetNetworking(searchField).subscribe((x: StoreNetworkings[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StorePOS:
        this.techCompService.GetPOS(searchField).subscribe((x: StorePOS[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreAudio:
        this.techCompService.GetAudio(searchField).subscribe((x: StoreAudio[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreExteriorMenus:
        this.techCompService.GetExteriorMenus(searchField).subscribe((x: StoreExteriorMenus[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StorePaymetSystem:
        this.techCompService.GetPaymentSystem(searchField).subscribe((x: StorePaymentSystem[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreInteriorMenus:
        this.techCompService.GetInteriorMenus(searchField).subscribe((x: StoreInteriorMenus[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreSonicRadio:
        this.techCompService.GetSonicRadio(searchField).subscribe((x: StoreSonicRadio[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreInstallation:
        this.techCompService.GetInstallation(searchField).subscribe((x: StoreInstallation[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
    }
  }

  translateValuesToFields(fields: Fields[], resp: any) {
    let values: any = {};
    if (typeof resp == 'undefined') {
      for (var tIndx in fields) {
        let fieldName = fields[tIndx].fieldUniqeName;
        let val = (fieldName.toLocaleLowerCase() == 'nProjectID') ? this._element.nProjectId.toString() : "";
        values[fieldName] = val;
      }
    }
    else {
      for (var tIndx in fields) {
        let fieldName = fields[tIndx].fieldUniqeName;
        values[fieldName] = resp[fieldName];
      }
    }
    return values;
  }
}
