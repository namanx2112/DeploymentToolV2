import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { Dictionary, FormsModes } from 'src/app/interfaces/commons';
import { FormGroup } from '@angular/forms';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel } from 'src/app/interfaces/models';
import { TechComponenttService } from 'src/app/services/tech-component.service';
import { VendorService } from 'src/app/services/vendor.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { UserService } from 'src/app/services/user.service';

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
  }
  get curTab(): HomeTab {
    return this._curTab;
  }
  @Output() returnBack = new EventEmitter<any>()
  private _controlValues: Dictionary<string>;
  activeTabIndex: number;
  childCount: number;
  @Input() set controlValues(value: Dictionary<string>) {
    this._controlValues = value;
    this.valueChanged();
  };
  get controlValues(): Dictionary<string> {
    return this._controlValues;
  }
  SubmitLabel: string;
  cService: any;
  _innerControlValues: Dictionary<string>;
  _innerMode: FormsModes;
  get innerControlValues(): Dictionary<string> {
    return this._innerControlValues;
  }
  set innerControlValues(val: Dictionary<string>) {
    this._innerControlValues = val;
    if (Object.keys(val).length > 0)
      this._innerMode = FormsModes.Edit;
    else
      this._innerMode = FormsModes.None;
  };
  constructor(private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseService: FranchiseService, private userSerice: UserService) {
    this.SubmitLabel = "Submit";
    this.controlValues = {};
    this._innerControlValues = {};
    this.activeTabIndex = -1;
  }

  valueChanged() {

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
    }
    return tService;
  }

  showTab(index: number) {
    this.activeTabIndex = index;
  }

  isEditMode() {
    let isMode = false;
    if (this.curTab.tab_type == TabType.Brands) {
      if (this.controlValues["aBrandId"] && parseInt(this.controlValues["aBrandId"]) > 0) {
        isMode = true;
      }
    }
    else if (this.curTab.tab_type == TabType.TechComponent) {
      if (this.controlValues["aTechCompId"] && parseInt(this.controlValues["aTechCompId"]) > 0) {
        isMode = true;
      }
    }
    else if (this.curTab.tab_type == TabType.Vendor) {
      if (this.controlValues["aVendorId"] && parseInt(this.controlValues["aVendorId"]) > 0) {
        isMode = true;
      }
    }
    else if (this.curTab.tab_type == TabType.Franchise) {
      if (this.controlValues["aFranchiseId"] && parseInt(this.controlValues["aFranchiseId"]) > 0) {
        isMode = true;
      }
    }
    return isMode;
  }

  rowClicked(row: any) {
    this.innerControlValues = {};
    for (var i in row) {
      this.innerControlValues[i] = row[i];
    }
  }


  onSubmit(controlVals: FormGroup, tab: HomeTab) {
    this.cService = this.getService(tab);
    if (this.isEditMode()) {
      this.cService.Update(controlVals.value).subscribe((resp: FranchiseModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
    else {
      this.cService.Create(controlVals.value).subscribe((resp: FranchiseModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
  }
}
