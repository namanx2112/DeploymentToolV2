import { Component } from '@angular/core';
import { HomeTab } from 'src/app/interfaces/home-tab';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-admin-config',
  templateUrl: './admin-config.component.html',
  styleUrls: ['./admin-config.component.css']
})
export class AdminConfigComponent {
  tabIndex: number;
  tabItems: string[];
  tabs: HomeTab[];
  curTab: HomeTab;
  needNew: boolean;
  constructor(private homeService: HomeService) {
    this.getAllTabs();
    this.tabIndex = -1;
  this.tabItems = ["Dashboard", "Users", "Brands Profile", "Vendors", "Manage Dropdowns", "Technology Areas", "Franchise", "Quote Request Workflow Config", "PO Workflow Config"];
    this.needNew = false;
    //Users, Brands, Vendors, Tech Components, Franchises, Stores, Tech Component Tools, Analytics
  }

  getAllTabs(){
    this.tabs = this.homeService.GetAllTabs();
  }

  clickOption(index: number) {
    this.needNew = false;
    this.tabIndex = index;
    if (index >= 0)
      this.curTab = this.tabs[index];
    else {
      if (index == -1) {

      }
    }
  }

  ShortcutSelection(tName: string) {
    switch (tName) {
      case "NewUser":
        this.tabIndex = 0;
        break;
      case "NewBrand":
        this.tabIndex = 1;
        break;
      case "NewVendor":
        this.tabIndex = 2;
        break;
      case "NewDropdown":
        this.tabIndex = -2;
        break;
      case "NewReport":
        this.tabIndex = -1;
        break;
      case "NewTechnologyArea":
        this.tabIndex = 3;
        break;
      case "Setting":
        this.tabIndex = -1;
        break;
      case "NewFrenchise":
        this.tabIndex = 4;
        break;
      case "Dummy":
        this.tabIndex = -1;
        break;
    }
    this.needNew = true;
  }
}
