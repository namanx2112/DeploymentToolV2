import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel, Brands } from 'src/app/interfaces/models';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-search-fields',
  templateUrl: './search-fields.component.html',
  styleUrls: ['./search-fields.component.css']
})
export class SearchFieldsComponent {
  @Input()
  set request(val: any) {
    this._curBrand = val.curBrand;
    this.columns = val.columns;
    this.loadProjectTYpes();
  }
  @Output()
  searchClicked = new EventEmitter<any>();
  columns: string[] = [];
  _curBrand: BrandModel;
  defaultCondition: string = '';
  selectedProjects: any[] = [];
  selectedVendors: any[] = [];
  selectedFranchise: any[] = [];
  selectedCity: string = "";
  selectedStoreNumber: string = "";
  allProjectTypes: OptionType[] = [];
  allVendors: OptionType[] = [];
  allFranchise: OptionType[] = [];
  campaignOne: FormGroup;
  reportParam: any;
  constructor(private commonService: CommonService) {
    this.campaignOne = new FormGroup({
      start: new FormControl(null),
      end: new FormControl(null),
    });
  }

  loadProjectTYpes() {
    this.allProjectTypes = this.commonService.GetDropdownOptions(-1, "ProjectType");
    this.allVendors = this.commonService.GetDropdownOptions(-1, "Vendor");
    this.allFranchise = this.commonService.GetDropdownOptions(-1, "Franchise");
    if (this._curBrand.nBrandType == Brands.Sonic)
      this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") == -1)
    this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase() == "new")[0].tDropdownText = "New Store Opening (NRO)";
    // else
    //   this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") > -1)
  }

  callSearch(searchFields: any) {
    this.searchClicked.emit(searchFields);
  }

  getRecord() {
    let tProjTypes = this.selectedProjects.join(",");
    let dtStart = (this.campaignOne.controls['start'].valid) ? CommonService.getFormatedDateStringForDB(this.campaignOne.controls['start'].value) : null;
    let dtEnd = (this.campaignOne.controls['end'].valid) ? CommonService.getFormatedDateStringForDB(this.campaignOne.controls['end'].value) : null;
    let tVendor = this.selectedVendors.join(",");
    let tFranchise = this.selectedFranchise.join(",");
    let tCity = this.selectedCity;
    let tStoreNumber = this.selectedStoreNumber;
    this.callSearch({
      nBrandId: this._curBrand.aBrandId, tProjTypes: tProjTypes, dtStart: dtStart, dtEnd: dtEnd,
      tVendor: tVendor, tFranchise: tFranchise, tCity: tCity, defaultCondition: this.defaultCondition,
      tStoreNumber: tStoreNumber
    });
  }

  resetFields() {
    this.selectedProjects = [];
    this.selectedVendors = [];
    this.selectedFranchise = [];
    this.campaignOne.controls['start'].setValue(null);
    this.campaignOne.controls['end'].setValue(null);
    this.selectedCity = "";
    this.selectedStoreNumber = "";
    this.searchClicked.emit();
  }
}
