import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, OptionType, TabInstanceType, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel, Brands } from 'src/app/interfaces/models';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-new-store',
  templateUrl: './new-store.component.html',
  styleUrls: ['./new-store.component.css']
})
export class NewStoreComponent {
  _ProjectType: OptionType;
  _NeedTechComponent: string;
  @Input() set ProjectParam(val: any) {
    this._ProjectType = val.ProjectType;
    this._NeedTechComponent = val.TechCompType;
    this.curBrandId = val.curBrandId;
    this.getTabs(this.curBrandId);
    this.loadCurTab();
  }
  @Output() ChangeView = new EventEmitter<string>();
  curBrandId: number;
  allTabs: HomeTab[];
  curTab: HomeTab;
  tValues: Dictionary<Dictionary<string>>;
  SubmitLabel: string;
  curTabIndex: number;
  canMove: boolean = false;
  constructor(private service: ExStoreService, private storeSerice: StoreService, private techCompService: AllTechnologyComponentsService) {
    this.curTabIndex = 0;
    this.SubmitLabel = "Next";
    this.tValues = {};
  }

  loadCurTab() {
    this.curTab = this.allTabs[this.curTabIndex];
  }

  goBack() {
    this.ChangeView.emit("dashboard");
  }

  selectTab(index: number) {
    if (this.canMove) {
      this.curTabIndex = index;
      this.loadCurTab();
    }
  }

  checkMandatory() {
    let yes = true;
    let firstTab = this.allTabs[0];
    let tabValues = this.tValues[firstTab.tab_name];
    for (var indx in firstTab.fields) {
      if (firstTab.fields[indx].mandatory) {
        if (typeof tabValues[firstTab.fields[indx].fieldUniqeName] == 'undefined' || tabValues[firstTab.fields[indx].fieldUniqeName] == "") {
          yes = false;
          break;
        }
      }
    }
    if (yes)
      this.canMove = true;
    else
      this.canMove = false;
    return yes;
  }

  getTabs(nBrandId: number) {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == nBrandId);
    this.allTabs = [];
    this.allTabs.push(this.service.GetNewStoresTab(TabInstanceType.Single));
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = { "nProjectType": this._ProjectType.aDropdownId };
    this.allTabs.push(this.service.GetStoreConfigurationTab(TabInstanceType.Single));
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
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
    if (tBrand.nBrandType == Brands.Buffalo) {
      if (this._NeedTechComponent == "all" || this._NeedTechComponent == "serverhandheld") {
        this.allTabs.push(this.service.GetStoreServerHandheldTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
    }

    if (tBrand.nBrandType == Brands.Dunkin) {
      if (this._NeedTechComponent == "all" || this._NeedTechComponent == "orderaccuracy") {
        this.allTabs.push(this.service.GetStoreOrderAccuracyTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
    }

    if (tBrand.nBrandType == Brands.Dunkin) {
      if (this._NeedTechComponent == "all" || this._NeedTechComponent == "orderstatusboard") {
        this.allTabs.push(this.service.GetStoreOrderStatusBoardTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
    }

    if (tBrand.nBrandType == Brands.Arby) {
      if (this._NeedTechComponent == "all" || this._NeedTechComponent == "hprollout") {
        this.allTabs.push(this.service.GetStoreNetworkSwitchTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
        this.allTabs.push(this.service.GetStoreImageMemoryTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "exteriormenu") {
      this.allTabs.push(this.service.GetStoreExteriorMenusTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "payment") {
      this.allTabs.push(this.service.GetStorePaymentSystemTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all" || this._NeedTechComponent == "interiormenu") {
      this.allTabs.push(this.service.GetStoreInteriorMenusTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }

    if (this._NeedTechComponent == "all") {
      if (tBrand.nBrandType == Brands.Arby) {
        this.allTabs.push(this.service.GetStoreRadioTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
      else if (tBrand.nBrandType == Brands.Sonic) {
        this.allTabs.push(this.service.GetStoreSonicRadioTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
      this.allTabs.push(this.service.GetStoreInsallationTab(TabInstanceType.Single));
      this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
    }
  }

  clickTab(indx: number) {
    this.curTabIndex = indx;
    this.loadCurTab();
  }

  setStoreId(nStoreId: string) {
    for (var indx in this.allTabs) {
      if (typeof this.tValues[this.allTabs[indx].tab_name] != 'undefined') {
        this.tValues[this.allTabs[indx].tab_name]["nStoreId"] = nStoreId;
        this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this._ProjectType.aDropdownId;
      }
      else {
        this.tValues[this.allTabs[indx].tab_name] = { "nStoreId": nStoreId };
        this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this._ProjectType.aDropdownId;
      }
    }
  }

  onSubmit(controlVals: any, tab: HomeTab) {
    this.tValues[tab.tab_name] = controlVals.value;
    let butttonText = controlVals.butttonText;
    if (butttonText == "Submit")
      this.saveAll();
    else {
      if (this.curTabIndex == this.allTabs.length - 1)
        this.saveAll();
      else {
        this.curTabIndex++;
        this.loadCurTab();
      }
    }
  }

  saveAll() {
    let cThis = this;
    let tIndx = 0;
    let tab = this.allTabs[tIndx];
    let tmpVal = cThis.tValues[tab.tab_name];
    let tStoreNumber = tmpVal["tStoreNumber"];
    let callBack = function (respValues: any) {
      tab.done = true;
      if (tab.tab_type == TabType.NewStore) {
        if (typeof respValues == 'number') {
          alert("The store number " + cThis.tValues[tab.tab_name]["tStoreNumber"] + " already exists!");
          return;
        }
        cThis.setStoreId(respValues.aStoreId);
      }
      if (tIndx + 1 == cThis.allTabs.length) {
        alert("The store number " + tStoreNumber + " created Successfully!");
        cThis.ChangeView.emit("dashboard");
      }
      else {
        tIndx++;
        tab = cThis.allTabs[tIndx];
        cThis.saveMe(tab, cThis.tValues[tab.tab_name], callBack);
      }
      //cThis.checkMandatory();
    }
    this.saveMe(tab, tmpVal, callBack);
  }

  saveMe(tab: HomeTab, fieldValues: any, callBack: any) {
    switch (tab.tab_type) {
      case TabType.NewStore:
        let aStoreId = parseInt(this.tValues[tab.tab_name]["aStoreId"]);
        if (aStoreId > 0) {
          this.storeSerice.UpdateStore(fieldValues).subscribe((nStoreId: string) => {
            callBack(fieldValues, false);
          });
        }
        else {
          fieldValues["nBrandID"] = this.curBrandId;
          this.storeSerice.CreateNewStores(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreConfiguration:
        let aProjectConfigID = (this.tValues[tab.tab_name]["aProjectConfigID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectConfigID"]) : 0;
        if (aProjectConfigID > 0) {
          this.techCompService.UpdateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreStackHolder:
        let aProjectStakeHolderID = (this.tValues[tab.tab_name]["aProjectStakeHolderID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectStakeHolderID"]) : 0;
        if (aProjectStakeHolderID > 0) {
          this.techCompService.UpdateStackholders(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateStackholders(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreNetworking:
        let aProjectNetworkingID = (this.tValues[tab.tab_name]["aProjectNetworkingID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectNetworkingID"]) : 0;
        if (aProjectNetworkingID > 0) {
          this.techCompService.UpdateNetworking(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateNetworking(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StorePOS:
        let aProjectPOSID = (this.tValues[tab.tab_name]["aProjectPOSID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPOSID"]) : 0;
        if (aProjectPOSID > 0) {
          this.techCompService.UpdatePOS(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreatePOS(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreAudio:
        let aProjectAudioID = (this.tValues[tab.tab_name]["aProjectAudioID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectAudioID"]) : 0;
        if (aProjectAudioID > 0) {
          this.techCompService.UpdateAudio(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateAudio(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreExteriorMenus:
        let aProjectExteriorMenuID = (this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) : 0;
        if (aProjectExteriorMenuID > 0) {
          this.techCompService.UpdateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StorePaymetSystem:
        let aProjectPaymentSystemID = (this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) : 0;
        if (aProjectPaymentSystemID > 0) {
          this.techCompService.UpdatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreInteriorMenus:
        let aProjectInteriorMenuID = (this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) : 0;
        if (aProjectInteriorMenuID > 0) {
          this.techCompService.UpdateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreSonicRadio:
        let aProjectSonicRadioID = (this.tValues[tab.tab_name]["aProjectSonicRadioID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectSonicRadioID"]) : 0;
        if (aProjectSonicRadioID > 0) {
          this.techCompService.UpdateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreInstallation:
        let aProjectInstallationID = (this.tValues[tab.tab_name]["aProjectInstallationID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInstallationID"]) : 0;
        if (aProjectInstallationID > 0) {
          this.techCompService.UpdateInstallation(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateInstallation(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreProjectServerHandheld:
        let aServerHandheldId = (this.tValues[tab.tab_name]["aServerHandheldId"]) ? parseInt(this.tValues[tab.tab_name]["aServerHandheldId"]) : 0;
        if (aServerHandheldId > 0) {
          this.techCompService.UpdateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreNetworkSwitch:
        let aProjectNetworkSwtichID = (this.tValues[tab.tab_name]["aProjectNetworkSwtichID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectNetworkSwtichID"]) : 0;
        if (aProjectNetworkSwtichID > 0) {
          this.techCompService.UpdateNetworkSwitch(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateNetworkSwitch(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreImageMemory:
        let aProjectImageOrMemoryID = (this.tValues[tab.tab_name]["aProjectImageOrMemoryID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectImageOrMemoryID"]) : 0;
        if (aProjectImageOrMemoryID > 0) {
          this.techCompService.UpdateImageMemory(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateImageMemory(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreOrderAccuracy:
        let aProjectOrderAccuracyID = (this.tValues[tab.tab_name]["aProjectOrderAccuracyID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectOrderAccuracyID"]) : 0;
        if (aProjectOrderAccuracyID > 0) {
          this.techCompService.UpdateOrderAccuracy(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateOrderAccuracy(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
      case TabType.StoreOrderStatusBoard:
        let aProjectOrderStatusBoardID = (this.tValues[tab.tab_name]["aProjectOrderStatusBoardID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectOrderStatusBoardID"]) : 0;
        if (aProjectOrderStatusBoardID > 0) {
          this.techCompService.UpdateOrderStatusBoard(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, false);
          });
        }
        else {
          this.techCompService.CreateOrderStatusBoard(fieldValues).subscribe((x: any) => {
            callBack(x, true);
          });
        }
        break;
    }
  }

  backClicked(ev: any, tab: HomeTab) {
    if (this.curTabIndex > 0) {
      this.tValues[tab.tab_name] = ev.value;
      this.curTabIndex--;
      this.loadCurTab();
    }
  }
}
