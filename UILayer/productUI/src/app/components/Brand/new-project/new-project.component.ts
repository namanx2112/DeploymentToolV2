import { A } from '@angular/cdk/keycodes';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, OptionType, TabInstanceType, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { ProjectInfo, ProjectTypes, StoreSearchModel } from 'src/app/interfaces/store';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
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
    this.nProjectType = parseInt(this._ProjectType.aDropdownId);
    this._NeedTechComponent = val.TechCompType;
    this.curBrandId = val.curBrandId;
    this.getTabs(this.curBrandId);
    this.loadCurTab();
  }
  @Output() ChangeView = new EventEmitter<string>();
  curBrandId: number;
  allTabs: HomeTab[];
  curTab: HomeTab;
  tValues: Dictionary<Dictionary<any>>;
  SubmitLabel: string;
  curTabIndex: number;
  curStore: StoreSearchModel;
  loadingData: boolean;
  nProjectType: ProjectTypes;
  constructor(private service: ExStoreService, private storeSerice: StoreService, private techCompService: AllTechnologyComponentsService) {
    this.curTabIndex = 0;
    this.SubmitLabel = "Next";
    this.loadingData = false;
    this.tValues = {};
  }

  loadCurTab() {
    this.curTab = this.allTabs[this.curTabIndex];
  }

  goBack() {
    this.ChangeView.emit("dashboard");
  }

  SearchedResult(searchedStore: any) {
    this.loadingData = true;
    this.curStore = searchedStore;
    let pStoreTab = this.allTabs[1];
    let pStakeHolerTab = this.allTabs[2];
    this.storeSerice.getStoreDetails(this.curStore.nStoreId, this.nProjectType).subscribe((x: any) => {
      x.nProjectType = this.nProjectType.toString();
      this.tValues[pStoreTab.tab_name] = x;
      if (x.tStakeHolder != null)
        this.tValues[pStakeHolerTab.tab_name] = x.tStakeHolder;
      this.setStoreId(x["aStoreId"]);
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

  getTabs(nBrandId: number) {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == nBrandId);
    let tabs: HomeTab[];
    this.allTabs = [];
    this.allTabs.push(this.service.GetSearchStoresTab(TabInstanceType.Single));
    this.allTabs.push(this.getChangedStoreTab());
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = { "nProjectType": this.nProjectType.toString() };
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
    if (tBrand.tBrandName.toLowerCase().indexOf("buffalo") > -1) {
      if (this._NeedTechComponent == "all" || this._NeedTechComponent == "serverhandheld") {
        this.allTabs.push(this.service.GetStoreServerHandheldTab(TabInstanceType.Single));
        this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
      }
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
    }
    this.allTabs.push(this.service.GetStoreInsallationTab(TabInstanceType.Single));
    this.tValues[this.allTabs[this.allTabs.length - 1].tab_name] = {};
  }

  clickTab(indx: number) {
    this.curTabIndex = indx;
    this.loadCurTab();
  }

  setStoreId(nStoreId: string) {
    let counter = 0;
    for (var indx in this.allTabs) {
      if (counter > 1) {
        if (typeof this.tValues[this.allTabs[indx].tab_name] != 'undefined') {
          this.tValues[this.allTabs[indx].tab_name]["nStoreId"] = nStoreId;
          this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this.nProjectType.toString();
        }
        else {
          this.tValues[this.allTabs[indx].tab_name] = { "nStoreId": nStoreId };
          this.tValues[this.allTabs[indx].tab_name]["nProjectType"] = this.nProjectType.toString();
        }
      }
      counter++;
    }
  }

  onNext(controlVals: any, tab: HomeTab) {
    this.tValues[tab.tab_name] = controlVals.value;
    let butttonText = controlVals.butttonText;
    var cThis = this;
    if (this.curTabIndex + 1 == this.allTabs.length || butttonText == "Submit") {
      let tIndex = 1;
      let tTab = this.allTabs[tIndex];
      var saveCallback = function (resp: any, tab: HomeTab) {
        if (tIndex == 1) {
          cThis.setStoreId(resp.aStoreId);
          switch (cThis.nProjectType) {
            case ProjectTypes.AudioInstallation:
            case ProjectTypes.POSInstallation:
            case ProjectTypes.MenuInstallation:
            case ProjectTypes.PaymentTerminalInstallation:
            case ProjectTypes.ServerHandheldInstallation:
              cThis.tValues[cThis.allTabs[3].tab_name]["nProjectID"] = resp.nProjectID;
              break;
          }
        }
        if (tIndex + 1 == cThis.allTabs.length) {// Last Tab
          alert("Created Successfully");
          cThis.ChangeView.emit("dashboard");
        }
        else {
          tIndex++;
          let tTab = cThis.allTabs[tIndex];
          if (tTab.tab_type == TabType.StoreStackHolder) // No need to save again as stakeholder has been save through newProject API
            saveCallback({}, tTab);
          else
            cThis.onSubmit(cThis.tValues[tTab.tab_name], tTab, saveCallback);
        }
      }
      if (tIndex == 1) {
        cThis.tValues[tTab.tab_name]["tStakeHolder"] = cThis.tValues[cThis.allTabs[2].tab_name];// to take latest stakeholder data
      }
      this.onSubmit(cThis.tValues[tTab.tab_name], tTab, saveCallback);
    }
    else {
      cThis.moveNext();
    }
  }

  onSubmit(fieldValues: any, tab: HomeTab, callBack: any) {
    switch (tab.tab_type) {
      case TabType.NewStore:
        // let nStoreId = parseInt(this.tValues[tab.tab_name]["aStoreId"]);
        // if (nStoreId > 0) {
        //   this.storeSerice.UpdateStore(fieldValues).subscribe((nStoreId: string) => {
        //     callBack(fieldValues, tab);
        //   });
        // }
        // else {
        fieldValues["nBrandID"] = this.curBrandId;
        this.storeSerice.CreateNewStores(fieldValues).subscribe((x: any) => {
          callBack(x, tab);
        });
        //}
        break;
      case TabType.StoreConfiguration:
        let aProjectConfigID = (this.tValues[tab.tab_name]["aProjectConfigID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectConfigID"]) : 0;
        if (aProjectConfigID > 0) {
          this.techCompService.UpdateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreStackHolder:
        let aProjectStakeHolderID = (this.tValues[tab.tab_name]["aProjectStakeHolderID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectStakeHolderID"]) : 0;
        if (aProjectStakeHolderID > 0) {
          this.techCompService.UpdateStackholders(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateStackholders(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreNetworking:
        let aProjectNetworkingID = (this.tValues[tab.tab_name]["aProjectNetworkingID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectNetworkingID"]) : 0;
        if (aProjectNetworkingID > 0) {
          this.techCompService.UpdateNetworking(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateNetworking(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StorePOS:
        let aProjectPOSID = (this.tValues[tab.tab_name]["aProjectPOSID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPOSID"]) : 0;
        if (aProjectPOSID > 0) {
          this.techCompService.UpdatePOS(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreatePOS(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
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
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreExteriorMenus:
        let aProjectExteriorMenuID = (this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectExteriorMenuID"]) : 0;
        if (aProjectExteriorMenuID > 0) {
          this.techCompService.UpdateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateExteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StorePaymetSystem:
        let aProjectPaymentSystemID = (this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectPaymentSystemID"]) : 0;
        if (aProjectPaymentSystemID > 0) {
          this.techCompService.UpdatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreatePaymentSystem(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreInteriorMenus:
        let aProjectInteriorMenuID = (this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInteriorMenuID"]) : 0;
        if (aProjectInteriorMenuID > 0) {
          this.techCompService.UpdateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateInteriorMenus(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreSonicRadio:
        let aProjectSonicRadioID = (this.tValues[tab.tab_name]["aProjectSonicRadioID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectSonicRadioID"]) : 0;
        if (aProjectSonicRadioID > 0) {
          this.techCompService.UpdateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateSonicRadio(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreInstallation:
        let aProjectInstallationID = (this.tValues[tab.tab_name]["aProjectInstallationID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectInstallationID"]) : 0;
        if (aProjectInstallationID > 0) {
          this.techCompService.UpdateInstallation(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateInstallation(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
          });
        }
        break;
      case TabType.StoreProjectServerHandheld:
        let aServerHandheldId = (this.tValues[tab.tab_name]["aServerHandheldId"]) ? parseInt(this.tValues[tab.tab_name]["aServerHandheldId"]) : 0;
        if (aServerHandheldId > 0) {
          this.techCompService.UpdateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(fieldValues, tab);
          });
        }
        else {
          this.techCompService.CreateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(x, tab);
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
    if (this.curTabIndex + 1 == this.allTabs.length)
      this.SubmitLabel = "Finish";
    else
      this.SubmitLabel = "Next";
    this.loadCurTab();
  }

}
