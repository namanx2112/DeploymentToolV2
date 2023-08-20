import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { Fields, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { ControlsComponent } from '../../controls/controls.component';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { NotesListComponent } from '../notes-list/notes-list.component';
import { ProjectInfo, ProjectTypes, StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSearchModel, StoreSonicRadio, StoreStackholders } from 'src/app/interfaces/store';
import { AllTechnologyComponentsService } from 'src/app/services/all-technology-components.service';
import { NotImplementedComponent } from '../../not-implemented/not-implemented.component';
import { ChangeGoliveDateComponent } from '../change-golive-date/change-golive-date.component';
import { AccessService } from 'src/app/services/access.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-store-view',
  templateUrl: './store-view.component.html',
  styleUrls: ['./store-view.component.css']
})
export class StoreViewComponent {
  _curStore: StoreSearchModel;
  @Input()
  public set curStore(value: StoreSearchModel) {
    this._curStore = value;
    this.selectedProject = value.lstProjectsInfo[0];
    this.initTab();
  }
  @Input()
  curBrandId: number;
  allTabs: HomeTab[];
  tabForUI: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  selectedTab: number;
  viewName: string;
  @Output() ChangeFromStoreView = new EventEmitter<any>();
  selectedProject: ProjectInfo;
  constructor(private service: ExStoreService, private dialog: MatDialog, private techCompService: AllTechnologyComponentsService, public access: AccessService) {
    this.viewName = "tabview";
  }

  initTab() {
    this.allTabs = this.service.GetStoretabs();
    this.tValues = {};
    this.tabForUI = [];
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      // this.tValues[tTab.tab_name] = this.getValues(tTab);
      // tTab = this.changeTab(tTab);
      if (parseInt(tIndx) > 2)
        this.tabForUI.push(tTab);
      this.getValues(tTab);
    }
  }

  projectClick(tProject: ProjectInfo) {
    this.selectedProject = tProject;
    let configTab = this.allTabs.find(x => x.tab_type == TabType.StoreConfiguration);
    let stackeTab = this.allTabs.find(x => x.tab_type == TabType.StoreStackHolder);
    let installationTab = this.allTabs.find(x => x.tab_type == TabType.StoreInstallation);
    if (stackeTab && configTab && installationTab) {
      this.getValues(configTab);
      this.getValues(stackeTab);
      this.getValues(installationTab);
    }
  }

  projectTypeString(pType: number) {
    let pString = "";
    pString = CommonService.getProjectName(pType);
    return pString;
  }

  getFormatedDate(dtString: any) {
    return CommonService.getFormatedDateString(dtString);
  }

  changeTileView(tName: string) {
    this.viewName = tName;
  }

  changeGoliveDate(projInfo: ProjectInfo) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';

    dialogConfig.data = {
      projInfo: projInfo,
      curBrand: this._curStore.nBrandId,
      onSave: function (data: any) {
        projInfo.dGoLiveDate = data.toLocaleDateString();
        dialogRef.close();
      },
      onClose: function (data: any) {
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(ChangeGoliveDateComponent, dialogConfig);
  }

  ShowProject(view: string) {
    let param = {
      view: view,
      curStore: this._curStore
    };
    this.ChangeFromStoreView.emit(param);
  }

  canShowTab(tTab: HomeTab) {
    let can = false;
    if (typeof tTab != 'undefined') {
      if (typeof this.tValues[tTab.tab_name] != 'undefined')
        can = true;
    }
    return can;
  }

  changeTab(tTab: HomeTab): HomeTab {
    if (tTab.tab_type == TabType.StoreContact) {
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

  getValues(tabType: HomeTab) {
    let searchField: Dictionary<string> = { "nStoreId": this._curStore.nStoreId.toString() };
    let projIdSearchField: Dictionary<string> = { "nProjectID": this.selectedProject.nProjectId.toString() };
    switch (tabType.tab_type) {
      case TabType.StoreContact:
        this.techCompService.GetStoreContact(searchField).subscribe((x: StoreContact[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreConfiguration:
        this.techCompService.GetStoreConfig(projIdSearchField).subscribe((x: StoreConfiguration[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreStackHolder:
        this.techCompService.GetStackholders(projIdSearchField).subscribe((x: StoreStackholders[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
        });
        break;
      case TabType.StoreNetworking:
        this.techCompService.GetNetworking(searchField).subscribe((x: StoreNetworkings[]) => {
          this.tValues[tabType.tab_name] = this.translateValuesToFields(tabType.fields, x[0]);
          this.selectedTab = 0;
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
        this.techCompService.GetInstallation(projIdSearchField).subscribe((x: StoreInstallation[]) => {
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
        let val = (fieldName.toLocaleLowerCase() == 'nStoreId') ? this._curStore.nStoreId.toString() : "";
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

  onSubmit(tVal: any, tab: HomeTab) {
    let cThis = this;
    this.SaveTechComp(tab, tVal, function (tVal: any) {
      if (cThis.selectedTab < cThis.allTabs.length) {
        cThis.selectedTab++;
      }
    });
  }

  tabClick(tabIndex: number) {
    this.selectedTab = tabIndex;
  }

  ShowNotes() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme",
      curStore: this._curStore
    };
    dialogRef = this.dialog.open(NotesListComponent, dialogConfig);
  }

  NotImplemented() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme",
      curStore: this._curStore
    };
    dialogRef = this.dialog.open(NotImplementedComponent, dialogConfig);
  }

  ShowDailyUpdates() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme",
      curStore: this._curStore
    };
    dialogRef = this.dialog.open(NotImplementedComponent, dialogConfig);
  }

  ShowSignOff() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme",
      curStore: this._curStore
    };
    // dialogRef = this.dialog.open(NotesListComponent, dialogConfig);
    dialogRef = this.dialog.open(NotImplementedComponent, dialogConfig);
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
          fieldValues["nProjectID"] = this.selectedProject.nProjectId;
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
          fieldValues["nProjectID"] = this.selectedProject.nProjectId;
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
          fieldValues["nMyActiveStatus"] = 1;
          this.techCompService.UpdateInstallation(fieldValues).subscribe((x: any) => {
            callBack(fieldValues);
          });
        }
        else {
          fieldValues["nProjectID"] = this.selectedProject.nProjectId;
          fieldValues["nMyActiveStatus"] = 1;
          this.techCompService.CreateInstallation(fieldValues).subscribe((x: any) => {
            callBack(x);
          });
        }
        break;
    }
  }

  moveView(view: string) {
    let param = {
      view: view,
      curStore: this._curStore
    };
    this.ChangeFromStoreView.emit(param);
  }
}
