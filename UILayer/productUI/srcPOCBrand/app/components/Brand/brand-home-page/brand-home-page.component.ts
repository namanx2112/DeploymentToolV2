import { Component, Input } from '@angular/core';
import { OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AccessService } from 'src/app/services/access.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-brand-home-page',
  templateUrl: './brand-home-page.component.html',
  styleUrls: ['./brand-home-page.component.css']
})
export class BrandHomePageComponent {
  _curBrand: BrandModel;
  @Input()
  set curBrand(val: BrandModel) {
    this._curBrand = val;
    this.GetAllProjectTypes();
  }
  @Input()
  set openStore(val: StoreSearchModel) {
    if (val) {
      this.curStore = val;
      this.showMode = "storeview";
    }
  }
  showMode: string;
  curStore: StoreSearchModel;
  ProjectTypes: OptionType[];
  techCompType: string;
  projectType: OptionType;
  configMenu: string;
  reportParam: any;
  constructor(private commonService: CommonService, public access: AccessService) {
    this.configMenu = "dashboard";
    this.showMode = "dashboard";
    this.techCompType = "all";
  }
  GetAllProjectTypes() {
    this.ProjectTypes = this.commonService.GetDropdownOptions(this._curBrand.aBrandId, "ProjectType");
  }
  clickOption(val: any) {
    this.showMode = val;
    this.configMenu = val;
  }

  showStore(item: any) {
    this.curStore = item;
    this.showMode = "storeview";
    this.configMenu = "dashboard";
  }


  menuClick(tMode: string, techComp?: string, newOption?: string) {
    if (typeof newOption != 'undefined' && typeof techComp != 'undefined') {
      this.techCompType = techComp;
      this.projectType = this.ProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase() == newOption.toLocaleLowerCase())[0];
    }
    this.showMode = tMode;
  }

  SearchedResult(val: any) {
    this.curStore = val;
    this.showMode = "storeview";
  }

  ChangeView(view: any) {
    this.showMode = view;
  }

  ChangeViewBrand(request: any) {
    this.showMode = request.viewName;
    if (request.viewName == "viewreport") {
      this.reportParam = {};
      request.nBrandId = this._curBrand.aBrandId;
      this.reportParam = request;
    }
  }

  ChangeFromStoreView(param: any) {
    this.curStore = param.curStore;
    this.showMode = param.view;
  }

  BackToStoreView(param: any) {
    this.showMode = "storeview";
    this.curStore = param;
  }
}
