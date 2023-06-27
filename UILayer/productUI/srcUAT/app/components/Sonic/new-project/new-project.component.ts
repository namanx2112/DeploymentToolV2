import { A } from '@angular/cdk/keycodes';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, OptionType, TabInstanceType, TabType } from 'src/app/interfaces/home-tab';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { SonicService } from 'src/app/services/sonic.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.css']
})
export class NewProjectComponent {
  _ProjectType: OptionType;
  _NeedTechComponent: string;
  @Input() set ProjectParam(val: any) {
    this._ProjectType = val.ProjectType;
    this._NeedTechComponent = val.TechCompType;
    this.getTabs();
    this.loadCurTab();
  }
  @Output() ChangeView = new EventEmitter<string>();
  allTabs: HomeTab[];
  curTab: HomeTab;
  tValues: Dictionary<Dictionary<string>>;
  SubmitLabel: string;
  curTabIndex: number;
  curStore: StoreSearchModel;
  loadingData: boolean;
  constructor(private service: SonicService, private storeSerice: StoreService, private techCompService: AllTechnologyComponentsService) {
    this.curTabIndex = 0;
    this.SubmitLabel = "Next";
    this.loadingData = false;
    this.tValues = {};
  }

  loadCurTab() {
    this.curTab = this.allTabs[this.curTabIndex];
  }

  SearchedResult(searchedStore: any) {
    this.loadingData = true;
    this.curStore = searchedStore;
    let pStoreTab = this.allTabs[1];
    this.storeSerice.CreateAndGetProjectStoreDetails(this.curStore.nProjectId).subscribe((x: any) => {
      this.tValues[pStoreTab.tab_name] = x;
      this.setProjectId(x["nProjectID"]);
      this.moveNext();
    });
  }

  getChangedStoreTab(): HomeTab {
    let tType = this.service.GetNewStoresTab(TabInstanceType.Single);
    let visibleColumns = "tStoreNumber,tDMA,nDMAID,tStoreAddressLine1,tCity,nState,tPOC,tPOCEmail,tPOCPhone".split(",");
    for (var indx in tType.fields) {
      if (visibleColumns.indexOf(tType.fields[indx].fieldUniqeName) > -1) {
        tType.fields[indx].hidden = false;
        tType.fields[indx].readOnly = true;
      }
      else {
        tType.fields[indx].hidden = true;
      }
    }
    return tType;
  }

  getTabs() {
    this.allTabs = [];
    this.allTabs.push(this.service.GetSearchStoresTab(TabInstanceType.Single));
    this.allTabs.push(this.getChangedStoreTab());
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = { "nProjectType": this._ProjectType.optionIndex };
    this.allTabs.push(this.service.GetStoreStackholderTab(TabInstanceType.Single));
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "networking") {
      this.allTabs.push(this.service.GetStoreNetworingTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "pos") {
      this.allTabs.push(this.service.GetStorePOSTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "audio") {
      this.allTabs.push(this.service.GetStoreAudioTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }
    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "menu") {
      this.allTabs.push(this.service.GetStoreExteriorMenusTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "payment") {
      this.allTabs.push(this.service.GetStorePaymentSystemTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "menu") {
      this.allTabs.push(this.service.GetStoreInteriorMenusTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all") {
      this.allTabs.push(this.service.GetStoreSonicRadioTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      this.allTabs.push(this.service.GetStoreInsallationTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }
  }

  clickTab(indx: number) {
    this.curTabIndex = indx;
    this.loadCurTab();
  }

  setProjectId(nProjectID: string) {
    let counter = 0;
    for (var indx in this.allTabs) {
      if (counter > 1) {
        if (typeof this.tValues[this.allTabs[indx].tab_name] != 'undefined') {
          this.tValues[this.allTabs[indx].tab_name]["nProjectID"] = nProjectID;
          this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this._ProjectType.optionIndex;
        }
        else {
          this.tValues[this.allTabs[indx].tab_name] = { "nProjectID": nProjectID };
          this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this._ProjectType.optionIndex;
        }
      }
      counter++;
    }
  }

  onSubmit(controlVals: FormGroup, tab: HomeTab) {
    let fieldValues = controlVals.value;
    let cThis = this;
    let callBack = function (respValues: any) {
      cThis.tValues[tab.tab_name] = respValues;
      if (cThis.curTabIndex + 1 == cThis.allTabs.length) {
        alert("Created Successfully");
        cThis.ChangeView.emit("dashboard");
      }
      cThis.moveNext();
    }
    switch (tab.tab_type) {
      case TabType.NewStore:
        let nProjectID = parseInt(this.tValues[tab.tab_name]["nProjectID"]);
        if (nProjectID > 0) {
          this.storeSerice.UpdateStore(fieldValues).subscribe((nProjectID: string) => {
            callBack(fieldValues);
          });
        }
        else {
          this.storeSerice.CreateNewStores(fieldValues).subscribe((x: any) => {
            this.tValues[tab.tab_name] = x;
            this.setProjectId(x.nProjectID);
            this.curTabIndex++;
            this.loadCurTab();
          });
        }
        break;
      case TabType.StoreConfiguration:
        let aProjectConfigID = (this.tValues[tab.tab_name]["aProjectConfigID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectConfigID"]) : 0;
        if (aProjectConfigID > 0) {
          this.techCompService.UpdateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreStackHolder:
        let aProjectStakeHolderID = (this.tValues[tab.tab_name]["aProjectStakeHolderID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectStakeHolderID"]) : 0;
        if (aProjectStakeHolderID > 0) {
          this.techCompService.UpdateStackholders(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateStackholders(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreNetworking:
        let aProjectNetworkingID = (this.tValues[tab.tab_name]["aProjectNetworkingID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectNetworkingID"]) : 0;
        if (aProjectNetworkingID > 0) {
          this.techCompService.UpdateNetworking(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateNetworking(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StorePOS:
        let aProjectPOSID = (this.tValues[tab.tab_name]["aProjectPOSID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPOSID"]) : 0;
        if (aProjectPOSID > 0) {
          this.techCompService.UpdatePOS(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreatePOS(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreAudio:
        let aProjectAudioID = (this.tValues[tab.tab_name]["aProjectAudioID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectAudioID"]) : 0;
        if (aProjectAudioID > 0) {
          this.techCompService.UpdateAudio(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateAudio(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreExteriorMenus:
        let aProjectExteriorMenuID = (this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) : 0;
        if (aProjectExteriorMenuID > 0) {
          this.techCompService.UpdateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StorePaymetSystem:
        let aProjectPaymentSystemID = (this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) : 0;
        if (aProjectPaymentSystemID > 0) {
          this.techCompService.UpdatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreInteriorMenus:
        let aProjectInteriorMenuID = (this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) : 0;
        if (aProjectInteriorMenuID > 0) {
          this.techCompService.UpdateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreSonicRadio:
        let aProjectSonicRadioID = (this.tValues[tab.tab_name]["aProjectSonicRadioID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectSonicRadioID"]) : 0;
        if (aProjectSonicRadioID > 0) {
          this.techCompService.UpdateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreInstallation:
        let aProjectInstallationID = (this.tValues[tab.tab_name]["aProjectInstallationID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInstallationID"]) : 0;
        if (aProjectInstallationID > 0) {
          this.techCompService.UpdateInstallation(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateInstallation(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
    }
  }

  backClicked(ev: any, tab: HomeTab) {
    if (this.curTabIndex > 1) {
      this.tValues[tab.tab_name] = ev.value;
      this.curTabIndex--;
      this.loadCurTab();
    }
  }

  moveNext() {
    if (this.curTabIndex == 0) {

    }
    this.curTabIndex++;
    this.loadCurTab();
  }

}
