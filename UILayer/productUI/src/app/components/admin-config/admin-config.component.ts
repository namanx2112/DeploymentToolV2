import { Component } from '@angular/core';
import { HomeTab } from 'src/app/interfaces/home-tab';
import { AccessService } from 'src/app/services/access.service';
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
  selectedOption: string;
  constructor(private homeService: HomeService, public access: AccessService) {
    this.getAllTabs();
    this.selectedOption = "Dashboard";
    this.tabIndex = -1;
    this.tabItems = ["Dashboard", "Users", "Brands Profile", "Vendors", "Manage Dropdowns", "Technology Areas", "Franchise", "Quote Request Workflow Config", "PO Workflow Config"];
    this.needNew = false;
    //Users, Brands, Vendors, Tech Components, Franchises, Stores, Tech Component Tools, Analytics
  }

  getAllTabs() {
    this.tabs = this.homeService.GetAllTabs();
  }

  clickOption(index: number, tString: string) {
    this.needNew = false;
    this.tabIndex = index;
    if (index >= 0)
      this.curTab = this.tabs[index];
    else {
      if (index == -1) {

      }
    }
    this.selectedOption = tString;
  }

  moveView(tIndex: any) {
    this.tabIndex = tIndex;
  }

  ShortcutSelection(ev: any) {
    this.selectedOption = ev.tabName;
    let tName = ev.action;
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
      case "NewQuoteRequest":
        this.tabIndex = 5;
        break;
      case "NewPO":
        this.tabIndex = 7;
        break;
      case "Dummy":
        this.tabIndex = -1;
        break;
    }
    this.needNew = true;
  }
}
