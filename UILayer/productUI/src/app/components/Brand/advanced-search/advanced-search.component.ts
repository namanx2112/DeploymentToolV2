import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent {
  @Input()
  set request(val: any) {
    this._curBrand = val.curBrand;
    this.defaultCondition = val.defaultCondition;
    this.loadProjectTYpes();
    this.getStoreTable();
  };
  _curBrand: BrandModel;
  @Output() ChangeView = new EventEmitter<string>();
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  defaultCondition: string = '';
  selectedProjects: any[] = [];
  selectedVendors: any[] = [];
  selectedFranchise: any[] = [];
  selectedCity: string = "";
  allProjectTypes: OptionType[] = [];
  allVendors: OptionType[] = [];
  allFranchise: OptionType[] = [];
  campaignOne: FormGroup;
  reportParam: any;
  constructor(private commonService: CommonService) {
    this.campaignOne = new FormGroup({
      start: new FormControl(new Date(new Date().getFullYear(), 0, 1)),
      end: new FormControl(new Date(new Date().getFullYear(), 11, 31)),
    });
  }

  moveMe(ev: any) {
    this.ChangeView.emit(ev);
  }

  showStore(ev: any) {
    this.openStore.emit(ev);
  }

  loadProjectTYpes() {
    this.allProjectTypes = this.commonService.GetDropdownOptions(-1, "ProjectType");
    this.allVendors = this.commonService.GetDropdownOptions(-1, "Vendor");
    this.allFranchise = this.commonService.GetDropdownOptions(-1, "Franchise");
    if (this._curBrand.tBrandName.toLocaleLowerCase().indexOf("sonic") > -1)
      this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") == -1)
    this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase() == "new")[0].tDropdownText = "New Store Opening (NRO)";
    // else
    //   this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") > -1)
  }

  resetFields() {
    this.selectedProjects = [];
    this.selectedVendors = [];
    this.selectedFranchise = [];
    this.campaignOne.controls['start'].setValue(new Date(new Date().getFullYear(), 0, 1));
    this.campaignOne.controls['end'].setValue(new Date(new Date().getFullYear(), 11, 31));
    this.selectedCity = "";
    this.getStoreTable();
  }

  getStoreTable() {
    let tProjTypes = this.selectedProjects.join(",");
    let dtStart = (this.campaignOne.controls['start'].valid) ? CommonService.getFormatedDateStringForDB(this.campaignOne.controls['start'].value) : null;
    let dtEnd = (this.campaignOne.controls['end'].valid) ? CommonService.getFormatedDateStringForDB(this.campaignOne.controls['end'].value) : null;
    let tVendor = this.selectedVendors.join(",");
    let tFranchise = this.selectedFranchise.join(",");
    let tCity = this.selectedCity;
    this.reportParam = {
      nBrandId: this._curBrand.aBrandId, tProjTypes: tProjTypes, dtStart: dtStart, dtEnd: dtEnd,
      tVendor: tVendor, tFranchise: tFranchise, tCity: tCity, defaultCondition: this.defaultCondition
    };
  }
}
