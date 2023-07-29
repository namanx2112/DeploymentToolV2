import { Component, EventEmitter, Output } from '@angular/core';
import { AccessService } from 'src/app/services/access.service';

@Component({
  selector: 'app-admin-dashboard-shortcuts',
  templateUrl: './admin-dashboard-shortcuts.component.html',
  styleUrls: ['./admin-dashboard-shortcuts.component.css']
})
export class AdminDashboardShortcutsComponent {

  @Output() ShortcutSelection = new EventEmitter<any>();
  tItems: { name: string, title: string, show: boolean, icon: string, tabName: string }[];
  searchText: string;
  constructor(public access: AccessService) {
    this.searchText = "";
    this.tItems = [
      { name: "NewUser", title: "Add New User", show: access.hasAccess('home.dashboard.user', 1), icon: "account_box", tabName: "Users" },
      { name: "NewBrand", title: "Add New Brand", show: access.hasAccess('home.dashboard.brand', 1), icon: "dashboard", tabName: "Brands Profile" },
      { name: "NewVendor", title: "Add New Vendor", show: access.hasAccess('home.dashboard.vendor', 1), icon: "extension", tabName: "Vendors" },
      { name: "NewDropdown", title: "Add Dropdown", show: access.hasAccess('home.dashboard.managedropdown', 1), icon: "shopping_basket", tabName: "Manage Dropdowns" },
      { name: "NewReport", title: "Add New Report", show: access.hasAccess('home.dashboard.report', 1), icon: "featured_play_list", tabName: "Users" },
      { name: "NewTechnologyArea", title: "Add New Technology Area", show: access.hasAccess('home.dashboard.techarea', 1), icon: "settings_input_component", tabName: "Technology Areas" },
      { name: "Setting", title: "Setting", show: access.hasAccess('home.dashboard.setting', 1), icon: "settings", tabName: "Users" },
      { name: "NewFrenchise", title: "Add New Frenchise", show: access.hasAccess('home.dashboard.franchise', 1), icon: "account_circle", tabName: "Franchise" },
      { name: "NewQuoteRequest", title: "Add Quote Request", show: access.hasAccess('home.dashboard.quoterequest', 1), icon: "event_note", tabName: "Quote Request Workflow Config" },
      { name: "NewPO", title: "Add Purchase Order", show: access.hasAccess('home.dashboard.po', 1), icon: "event_note", tabName: "PO Workflow Config" },
      // { name: "NewDummy", title: "Add NewDummy", show: true }
    ];
  }

  ShortcutClick(type: any) {
    this.ShortcutSelection.emit({ tabName: type.tabName, action: type.name });
  }

  onKeydown(ev: any) {
    let tVal = this.searchText.toLowerCase().trim();
    for (var indx in this.tItems) {
      if (tVal == "" || this.tItems[indx].name.toLocaleLowerCase().indexOf(tVal) > -1)
        this.tItems[indx].show = true;
      else
        this.tItems[indx].show = false;
    }
  }

  filterItem(part: any) {
    return (part.show);
  }
}
