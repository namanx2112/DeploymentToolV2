import { Component, Input } from '@angular/core';
import { HomeTab, OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { BrandModel, UserType } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AccessService } from 'src/app/services/access.service';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';

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
    this.getRolloutTab();
    this.GetAllProjectTypes();
    this.loadUserMeta();
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
  defaultConditionForSearch: string;
  showSearchForcefully: boolean;
  rolloutTab: HomeTab;
  constructor(private commonService: CommonService, public access: AccessService, private homeService: HomeService) {
    this.configMenu = "dashboard";
    this.showMode = "dashboard";
    this.techCompType = "all";
    this.defaultConditionForSearch = "";
    this.showSearchForcefully = false;
  }
  GetAllProjectTypes() {
    this.ProjectTypes = this.commonService.GetDropdownOptions(this._curBrand.aBrandId, "ProjectType");
  }

  getRolloutTab() {
    this.rolloutTab = this.homeService.GetProjectRolloutTab(TabInstanceType.Single);
  }

  loadUserMeta() {
    let userMeta = this.access.getMyUserMeta();
    if (userMeta.userType != UserType.User) {
      if (userMeta.userType != UserType.FranchiseUser) {
        this.defaultConditionForSearch = "nVendor=" + userMeta.nOriginatorId;
        this.showSearchForcefully = true;
        // this.showMode = "search";
        // this.configMenu = "search";
      }
    }
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
    if (tMode == "search" || tMode == "importproject"  || tMode == "projectrollout")
      this.configMenu = tMode;
    else
      this.configMenu = "dashboard";
    this.showMode = tMode;
  }

  SearchedResult(val: any) {
    this.curStore = val;
    this.showMode = "storeview";
  }

  ChangeView(view: any) {
    if (this.showSearchForcefully) {
      if (view == "dashboard")
        this.showMode = "search";
      else if (view == "dashboardpane")// From Report table back
        this.showMode = "dashboard";
    }
    else
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
    if (this.showSearchForcefully) {
      // if (param.view == "dashboard") {
      //   this.showMode = "search";
      //   this.configMenu = "search";
      // }
      // else
        this.showMode = param.view;
    }
    else {
      this.curStore = param.curStore;
      this.showMode = param.view;
    }
  }

  BackToStoreView(param: any) {
    this.showMode = "storeview";
    this.curStore = param;
  }
}
