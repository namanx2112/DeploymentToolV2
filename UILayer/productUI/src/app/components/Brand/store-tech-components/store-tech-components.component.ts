import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { Fields, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSearchModel, StoreSonicRadio, StoreStackholders } from 'src/app/interfaces/sonic';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { SonicService } from 'src/app/services/sonic.service';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { AccessService } from 'src/app/services/access.service';

@Component({
  selector: 'app-store-tech-components',
  templateUrl: './store-tech-components.component.html',
  styleUrls: ['./store-tech-components.component.css']
})
export class StoreTechComponentsComponent {
  _curStore: any;
  @Input()
  public set request(value: any) {
    this._curStore = value.element;
    this.needEdit = value.needEdit;
    this.initTab();
  }
  needEdit: boolean;
  allTabs: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  constructor(private dialog: MatDialog, private service: SonicService, private techCompService: AllTechnologyComponentsService, public access: AccessService) {

  }
  initTab() {
    if (typeof this._curStore.nProjectType != 'undefined')
      this.allTabs = this.service.GetStoreTabsForProjectType(this._curStore.nProjectType);
    else
      this.allTabs = this.service.GetStoretabs();
    this.tValues = {};
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      this.getValues(tTab);
    }
  }

  getValues(tabType: HomeTab) {
    let searchField: Dictionary<string> =
      (this.needEdit) ? { "nStoreId": this._curStore.nStoreId.toString() } : { "nProjectID": this._curStore.nProjectId.toString() };
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
    }
  }
}
