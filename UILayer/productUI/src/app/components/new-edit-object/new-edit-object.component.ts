import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { Dictionary } from 'src/app/interfaces/commons';
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
  constructor(private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseService: FranchiseService, private userSerice: UserService) {
    this.SubmitLabel = "Submit";
    this.controlValues = {};
    this.activeTabIndex = -1;
  }

  valueChanged() {

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

  rowClicked(ev: any){

  }

  
  onSubmit(controlVals: FormGroup) {
    if (this.curTab.tab_type == TabType.Brands) {
      this.SaveUpdateBrand(controlVals);
    }
    else if (this.curTab.tab_type == TabType.TechComponent) {
      this.SaveUpdateTechComponent(controlVals);
    }
    else if (this.curTab.tab_type == TabType.Vendor) {
      this.SaveUpdateVendor(controlVals);
    }
    else if (this.curTab.tab_type == TabType.Franchise) {
      this.SaveUpdateFranchise(controlVals);
    }
  }

  SaveUpdateFranchise(controlVals: FormGroup) {
    if (controlVals.value["aFranchiseId"] && parseInt(controlVals.value["aFranchiseId"]) > 0) {
      this.franchiseService.UpdateFranchise(controlVals.value).subscribe((resp: FranchiseModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
    else {
      this.franchiseService.CreateFranchise(controlVals.value).subscribe((resp: FranchiseModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
  }

  SaveUpdateVendor(controlVals: FormGroup) {
    let cThis = this;
    if (controlVals.value["aVendorId"] && parseInt(controlVals.value["aVendorId"]) > 0) {
      this.verndorService.UpdateVendor(controlVals.value).subscribe((resp: VendorModel) => {
        cThis.controlValues = controlVals.value;
        console.log(resp);
        //this.returnBack.emit(resp);
      });
    }
    else {
      this.verndorService.CreateVendor(controlVals.value).subscribe((resp: VendorModel) => {
        cThis.controlValues = controlVals.value;
        console.log(resp);
        //this.returnBack.emit(resp);
      });
    }
  }

  SaveUpdateTechComponent(controlVals: FormGroup) {
    if (controlVals.value["aTechComponentId"] && parseInt(controlVals.value["aTechComponentId"]) > 0) {
      this.techCompService.UpdateTechComponent(controlVals.value).subscribe((resp: TechComponentModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
    else {
      this.techCompService.CreateTechComponent(controlVals.value).subscribe((resp: TechComponentModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
  }

  SaveUpdateBrand(controlVals: FormGroup) {
    if (controlVals.value["aBrandId"] && parseInt(controlVals.value["aBrandId"]) > 0) {
      this.brandService.UpdateBrand(controlVals.value).subscribe((resp: BrandModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
    else {
      this.brandService.CreateBrand(controlVals.value).subscribe((resp: BrandModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
  }
}
