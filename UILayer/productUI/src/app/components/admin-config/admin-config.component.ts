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
  tabs: HomeTab[]
  constructor(private homeService: HomeService){
    this.getAllTabs();
    this.tabIndex = -1;
    this.tabItems = ["Dashboard", "Users", "Brands Profile", "Vendors", "Manage Dropdowns", "Technology Areas", "Franchise"];
  }

  getAllTabs(){
    this.tabs = this.homeService.GetAllTabs();
  }

  clickOption(index: number){
      this.tabIndex = index;
  }
}
