import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { Fields, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreImageMemory, StoreInstallation, StoreInteriorMenus, StoreNetworkSwitch, StoreNetworkings, StoreOrderAccuracy, StoreOrderStatusBoard, StorePOS, StorePaymentSystem, StoreSearchModel, StoreServerHandheld, StoreSonicRadio, StoreStackholders } from 'src/app/interfaces/store';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { AccessService } from 'src/app/services/access.service';
import { UserAccessMeta, UserType } from 'src/app/interfaces/models';

@Component({
  selector: 'app-store-tech-components',
  templateUrl: './store-tech-components.component.html',
  styleUrls: ['./store-tech-components.component.css']
})
export class StoreTechComponentsComponent {
  _curStore: any;
  curBrandId: number;
  @Input()
  public set request(value: any) {
    this._curStore = value.element;
    this.needEdit = value.needEdit;
    this.curBrandId = value.curBrandId;
    this.getUserMeta();
    this.initTab();
  }
  needEdit: boolean;
  allTabs: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  userMeta: UserAccessMeta;
  fieldRestrictions: any;
  constructor(private dialog: MatDialog, private service: ExStoreService, private techCompService: AllTechnologyComponentsService, public access: AccessService) {

  }
  initTab() {
    if (typeof this._curStore.nProjectType != 'undefined')
      this.allTabs = this.service.GetStoreTabsForProjectType(this._curStore.nProjectType, this.curBrandId);
    else
      this.allTabs = this.service.GetStoretabs(this.curBrandId);
    this.tValues = {};
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      this.getValues(tTab);
    }
  }

  getUserMeta() {
    this.userMeta = this.access.getMyUserMeta();
    this.fieldRestrictions = this.access.getCompFieldAccess();
    if (this.fieldRestrictions == null)
      this.fieldRestrictions = {};
  }

  getValues(tabType: HomeTab) {
    //let searchField: Dictionary<string> = { "nProjectID": this._curStore.nProjectId.toString() };
    let searchField: Dictionary<string> = (this.needEdit) ? { "nStoreId": this._curStore.nStoreId.toString() } : { "nProjectID": this._curStore.nProjectId.toString() };
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
      case TabType.StoreProjectServerHandheld:
        this.techCompService.GetServerHandheld(searchField).subscribe((x: StoreServerHandheld[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreImageMemory:
        this.techCompService.GetImageMemory(searchField).subscribe((x: StoreImageMemory[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreNetworkSwitch:
        this.techCompService.GetNetworkSwitch(searchField).subscribe((x: StoreNetworkSwitch[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreOrderAccuracy:
        this.techCompService.GetOrderAccuracy(searchField).subscribe((x: StoreOrderAccuracy[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreOrderStatusBoard:
        this.techCompService.GetOrderStatusBoard(searchField).subscribe((x: StoreOrderStatusBoard[]) => {
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
        let val = (fieldName.toLocaleLowerCase() == 'nProjectID') ? this._curStore.nProjectId.toString() : "";
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

  editTab(cTab: HomeTab) {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.maxHeight = '90vh';
    dialogConfig.width = '60%';

    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: cTab.tab_header,
      fields: cTab.fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: this.tValues[cTab.tab_name],
      SubmitLabel: "Save",
      curBrandId: this._curStore.nBrandId,
      onSubmit: function (data: any) {
        cthis.SaveTechComp(cTab, data, function (val: any) {
          cthis.tValues[cTab.tab_name] = data.value;
          dialogRef.close();
        });
      },
      onClose: function (ev: any) {
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
    // dialogRef.afterClosed().subscribe(result => {
    //   //console.log(`Dialog result: ${result}`);
    //   let t = result;
    // });
  }

  canEditTab(tab: HomeTab) {
    let can = true;
    can = this.access.hasAccess('home.sonic.project.POS', 1);
    if (!can) {
      if (tab.isTechComponent) {
        if (this.userMeta.userType == UserType.EquipmentVendor || this.userMeta.userType == UserType.EqupmentAndInstallationVendor) {
          let vendorId = this.tValues[tab.tab_name]["nVendor"];
          if (vendorId != null && vendorId != "" && parseInt(vendorId) == this.userMeta.nOriginatorId) {
            can = true;
          }
        }
      }
      else if (tab.tab_name == "Installation") {
        if (this.userMeta.userType == UserType.InstallationVendor || this.userMeta.userType == UserType.EqupmentAndInstallationVendor) {
          let vendorId = this.tValues[tab.tab_name]["nVendor"];
          if (vendorId != null && vendorId != "" && parseInt(vendorId) == this.userMeta.nOriginatorId) {
            can = true;
          }
        }
      }
    }
    return can;
  }

  canShowTabForUser(tab: HomeTab) {
    let can = true;
    if (tab.isTechComponent) {
      if (this.userMeta.userType == UserType.EquipmentVendor || this.userMeta.userType == UserType.EqupmentAndInstallationVendor) {
        let vendorId = this.tValues[tab.tab_name]["nVendor"];
        if (vendorId != null && vendorId != "" && parseInt(vendorId) == this.userMeta.nOriginatorId) {
          can = true;
        }
        else
          can = false;
      }
      else if (this.userMeta.userType == UserType.InstallationVendor)
        can = false;
    }
    else if (tab.tab_name == "Installation") {
      if (this.userMeta.userType == UserType.InstallationVendor || this.userMeta.userType == UserType.EqupmentAndInstallationVendor) {
        let vendorId = this.tValues[tab.tab_name]["nVendor"];
        if (vendorId != null && vendorId != "" && parseInt(vendorId) == this.userMeta.nOriginatorId) {
          can = true;
        } else
          can = false;
      }
    }
    return can;
  }

  SaveTechComp(tab: HomeTab, data: any, callBack: any) {
    let fieldValues = data.value;
    fieldValues["nStoreId"] = this._curStore.nStoreId;
    switch (tab.tab_type) {
      case TabType.StoreContact:
        this.techCompService.UpdateStoreContact(fieldValues).subscribe((x: StoreContact) => {
          callBack(x);
        });
        break;
      case TabType.StoreConfiguration:
        let aProjectConfigID = (this.tValues[tab.tab_name]["aProjectConfigID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectConfigID"]) : 0;
        if (aProjectConfigID > 0) {
          fieldValues["nMyActiveStatus"] = 1;
          this.techCompService.UpdateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          fieldValues["nProjectID"] = this._curStore.nProjectId;
          fieldValues["nMyActiveStatus"] = 1;
          this.techCompService.CreateStoreConfig(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreStackHolder:
        let aProjectStakeHolderID = (this.tValues[tab.tab_name]["aProjectStakeHolderID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectStakeHolderID"]) : 0;
        if (aProjectStakeHolderID > 0) {
          fieldValues["nMyActiveStatus"] = 1;
          this.techCompService.UpdateStackholders(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          fieldValues["nProjectID"] = this._curStore.nProjectId;
          fieldValues["nMyActiveStatus"] = 1;
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
      case TabType.StoreProjectServerHandheld:
        let aServerHandheldId = (this.tValues[tab.tab_name]["aServerHandheldId"]) ? parseInt(this.tValues[tab.tab_name]["aServerHandheldId"]) : 0;
        if (aServerHandheldId > 0) {
          this.techCompService.UpdateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateServerHandheld(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreImageMemory:
        let aProjectImageOrMemoryID = (this.tValues[tab.tab_name]["aProjectImageOrMemoryID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectImageOrMemoryID"]) : 0;
        if (aProjectImageOrMemoryID > 0) {
          this.techCompService.UpdateImageMemory(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateImageMemory(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreNetworkSwitch:
        let aProjectNetworkSwtichID = (this.tValues[tab.tab_name]["aProjectNetworkSwtichID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectNetworkSwtichID"]) : 0;
        if (aProjectNetworkSwtichID > 0) {
          this.techCompService.UpdateNetworkSwitch(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateNetworkSwitch(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreOrderAccuracy:
        let aProjectOrderAccuracyID = (this.tValues[tab.tab_name]["aProjectOrderAccuracyID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectOrderAccuracyID"]) : 0;
        if (aProjectOrderAccuracyID > 0) {
          this.techCompService.UpdateOrderAccuracy(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateOrderAccuracy(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
      case TabType.StoreOrderStatusBoard:
        let aProjectOrderStatusBoardID = (this.tValues[tab.tab_name]["aProjectOrderStatusBoardID"]) ? parseInt(this.tValues[tab.tab_name]["aProjectOrderStatusBoardID"]) : 0;
        if (aProjectOrderStatusBoardID > 0) {
          this.techCompService.UpdateOrderStatusBoard(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          this.techCompService.CreateOrderStatusBoard(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
    }
  }
}
