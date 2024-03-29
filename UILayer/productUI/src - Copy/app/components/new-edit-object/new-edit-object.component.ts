import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Fields, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { Dictionary } from 'src/app/interfaces/commons';
import { FormGroup } from '@angular/forms';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel } from 'src/app/interfaces/models';
import { TechComponenttService } from 'src/app/services/tech-component.service';
import { VendorService } from 'src/app/services/vendor.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { UserService } from 'src/app/services/user.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogControlsComponent } from '../dialog-controls/dialog-controls.component';
import { PartsService } from 'src/app/services/parts.service';
import { AccessService } from 'src/app/services/access.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-new-edit-object',
  templateUrl: './new-edit-object.component.html',
  styleUrls: ['./new-edit-object.component.css']
})
export class NewEditObjectComponent {
  _curTab: HomeTab;
  @Input() set curTab(val: HomeTab) {
    this._curTab = val;
    this.childCount = val.childTabs.length;
    this.initControlValues();
  }
  get curTab(): HomeTab {
    return this._curTab;
  }
  @Output() returnBack = new EventEmitter<any>()
  _controlValues: Dictionary<string>;
  childCount: number;
  @Input() set controlValues(value: Dictionary<string>) {
    this._controlValues = value;
    if (this.childCount > 0 && !this.isEditMode(this._curTab, this._controlValues))
      this.SubmitLabel = "Next";
    this.valueChanged();
  };
  get controlValues(): Dictionary<string> {
    return this._controlValues;
  }
  SubmitLabel: string;
  cService: any;
  childSearchFields: any = {};
  refreshChildTable: any = {};
  nBrandId: number = 0;
  constructor(private dialog: MatDialog, private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseService: FranchiseService, private userSerice: UserService, private partsService: PartsService, public access: AccessService, private commonService: CommonService) {
    this.SubmitLabel = "Save";
    this.controlValues = {};
  }

  valueChanged() {

  }

  initControlValues() {
    if (this.childCount > 0) {
      for (var indx in this._curTab.childTabs) {
        let tTab = this._curTab.childTabs[indx];
        this.refreshChildTable[tTab.tab_name] = {};
        this.childSearchFields[tTab.tab_name] = this.updateChildProperties(tTab, {});
      }
    }
  }

  getService(tab: HomeTab) {
    let tService: any;
    switch (tab.tab_type) {
      case TabType.Brands:
        tService = this.brandService;
        break;
      case TabType.Franchise:
        tService = this.franchiseService;
        break;
      case TabType.TechComponent:
        tService = this.techCompService;
        break;
      case TabType.Users:
        tService = this.userSerice;
        break;
      case TabType.Vendor:
        tService = this.verndorService;
        break;
      case TabType.VendorParts:
        tService = this.partsService;
        break;
    }
    return tService;
  }

  isEditMode(tTab: HomeTab, cVals?: any) {
    let isMode = false;
    if (tTab.tab_type == TabType.Brands) {
      if (cVals["aBrandId"] && parseInt(cVals["aBrandId"]) > 0) {
        isMode = true;
      }
    }
    else if (tTab.tab_type == TabType.TechComponent) {
      if (cVals["aTechCompId"] && parseInt(cVals["aTechCompId"]) > 0) {
        isMode = true;
      }
    }
    else if (tTab.tab_type == TabType.Vendor) {
      if (cVals["aVendorId"] && parseInt(cVals["aVendorId"]) > 0) {
        isMode = true;
      }
    }
    else if (tTab.tab_type == TabType.Franchise) {
      if (cVals["aFranchiseId"] && parseInt(cVals["aFranchiseId"]) > 0) {
        isMode = true;
      }
    }
    else if (tTab.tab_type == TabType.Users) {
      if (cVals["aUserID"] && parseInt(cVals["aUserID"]) > 0) {
        isMode = true;
      }
    }
    else if (tTab.tab_type == TabType.VendorParts) {
      if (cVals["aPartID"] && parseInt(cVals["aPartID"]) > 0) {
        isMode = true;
      }
    }
    return isMode;
  }

  rowClicked(row: any, cTab: HomeTab) {
    this._controlValues[cTab.tab_name] = row;
    this.editChildTab(cTab, false);
  }

  updateChildProperties(tTab: HomeTab, vals: any) {
    switch (tTab.tab_type) {
      case TabType.VendorParts:
        vals["nVendorId"] = this._controlValues["aVendorId"];
        break;
      case TabType.Users:
        let tOptions = this.commonService.GetDropdownOptions(this.nBrandId, "UserRole");
        let brandOptions = this.commonService.GetDropdownOptions(this.nBrandId, "Brand");
        vals["rBrandID"] = [1,2,3,4,5,6];
        if (this.curTab.tab_type == TabType.Vendor) {
          let cOption = tOptions.find(x => x.tDropdownText == "Vendor");
          if (cOption)
            vals["nRole"] = cOption.aDropdownId;
          vals["nVendorId"] = this._controlValues["aVendorId"];
        }
        else if (this.curTab.tab_type == TabType.Franchise) {
          let cOption = tOptions.find(x => x.tDropdownText == "Franchise");
          if (cOption)
            vals["nRole"] = cOption.aDropdownId;
          vals["nFranchiseId"] = this._controlValues["aFranchiseId"];
        }
        break;
    }
    return vals;
  }

  getChildRefreshFlag(tabName: string) {
    if (this.refreshChildTable[tabName])
      return this.refreshChildTable[tabName];
  }

  getChildFields(cTab: HomeTab, isNew: boolean): Fields[] {
    let tFields = cTab.fields;
    if (cTab.tab_type == TabType.Users) {
      if (this._curTab.tab_type == TabType.Franchise || this._curTab.tab_type == TabType.Vendor) {
        for (var indx in tFields) {
          if (tFields[indx].fieldUniqeName == "nRole") {
            tFields[indx].hidden = true;
          }
          // else if (tFields[indx].fieldUniqeName == "rBrandID") {
          //   tFields[indx].hidden = true;
          // }
          // if (!isNew) {
          //   if (tFields[indx].fieldUniqeName == "tUserName") {
          //     tFields[indx].readOnly = true;
          //   }
          // }
        }
      }
    }
    return tFields;
  }

  editChildTab(cTab: HomeTab, isNew: boolean) {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.maxHeight = '90vh';
    dialogConfig.width = '60%';
    if (isNew)
      delete this._controlValues[cTab.tab_name];
    let tVals = this.updateChildProperties(cTab, (typeof this._controlValues[cTab.tab_name] != 'undefined') ? this._controlValues[cTab.tab_name] : {});
    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: cTab.tab_header,
      fields: this.getChildFields(cTab, isNew),
      readOnlyForm: false,
      needButton: true,
      controlValues: tVals,
      SubmitLabel: "Save",
      curBrandId: 0,//NeedToChange
      onSubmit: function (data: any) {
        cthis.saveThisTab(data, cTab, function (val: any) {
          delete cthis._controlValues[cTab.tab_name];
          cthis.refreshChildTable[cTab.tab_name] = new Date();
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

  getChildSearchField(cTab: HomeTab) {
    let aField: any = null;
    switch (cTab.tab_type) {
      case TabType.Users:
      case TabType.VendorParts:
        if (this._curTab.tab_type == TabType.Vendor && typeof this._controlValues[cTab.tab_name] != 'undefined') {
          aField = { nVendorId: this._controlValues["aVendorId"] };
        }
        else if (this._curTab.tab_type == TabType.Franchise && typeof this._controlValues[cTab.tab_name] != 'undefined') {
          aField = { nFranchiseId: this._controlValues["aFranchiseId"] };
        }
        break;
    }
    return aField;
  }

  onCloseClicked(ev: any) {
    ev.closed = true;
    this.returnBack.emit(ev);
  }


  onSubmit(controlVals: FormGroup, tab: HomeTab) {
    let cThis = this;
    let newMode = (!this.isEditMode(controlVals.value, tab) && this._curTab.childTabs.length > 0);
    this.saveThisTab(controlVals, tab, function (resp: any) {
      if (newMode) {
        cThis.controlValues = resp;
        cThis.initControlValues();
        cThis.SubmitLabel = "Save";
      }
      else
        cThis.returnBack.emit(resp);
    });
  }


  saveThisTab(controlVals: FormGroup, tab: HomeTab, callBack: any) {
    this.cService = this.getService(tab);
    if (this.isEditMode(tab, controlVals.value)) {
      this.cService.Update(controlVals.value).subscribe((resp: any) => {
        if(resp == null && tab.childTabs.length > 0){
          alert("Saved successfully");
        }
        else if (typeof resp == 'string') {
          alert(resp);
        }
        else
          callBack(resp);
      });
    }
    else {
      this.cService.Create(controlVals.value).subscribe((resp: any) => {
        if (typeof resp == 'string') {
          alert(resp);
        }
        else
          callBack(resp);
      });
    }
  }
}
