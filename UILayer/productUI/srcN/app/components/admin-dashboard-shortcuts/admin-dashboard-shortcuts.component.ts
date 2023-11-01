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
    this.tItems = []
    if (access.hasAccess('home.configuration.users', 0))
      this.tItems.push({ name: "NewUser", title: "Add New User", show: access.hasAccess('home.configuration.user', 1), icon: "account_box", tabName: "Users" });
    if (access.hasAccess('home.configuration.Brands', 0))
      this.tItems.push({ name: "NewBrand", title: "Add New Brand", show: access.hasAccess('home.configuration.brand', 1), icon: "dashboard", tabName: "Brands Profile" });
    if (access.hasAccess('home.configuration.Vendors', 0))
      this.tItems.push({ name: "NewVendor", title: "Add New Vendor", show: access.hasAccess('home.configuration.vendor', 1), icon: "extension", tabName: "Vendors" });
    if (access.hasAccess('home.configuration.managedropdown', 0))
      this.tItems.push({ name: "NewDropdown", title: "Add Dropdown", show: access.hasAccess('home.configuration.managedropdown', 1), icon: "shopping_basket", tabName: "Manage Dropdowns" });
    if (access.hasAccess('home.configuration.report', 0))
      this.tItems.push({ name: "NewReport", title: "Add New Report", show: access.hasAccess('home.configuration.report', 1), icon: "featured_play_list", tabName: "Users" });
    if (access.hasAccess('home.configuration.Tech Components', 0))
      this.tItems.push({ name: "NewTechnologyArea", title: "Add New Technology Area", show: access.hasAccess('home.configuration.techarea', 1), icon: "settings_input_component", tabName: "Technology Areas" });
    if (access.hasAccess('home.configuration.setting', 0))
      this.tItems.push({ name: "Setting", title: "Setting", show: access.hasAccess('home.configuration.setting', 1), icon: "settings", tabName: "Users" });
    if (access.hasAccess('home.configuration.Franchises', 0))
      this.tItems.push({ name: "NewFrenchise", title: "Add New Frenchise", show: access.hasAccess('home.configuration.franchise', 1), icon: "account_circle", tabName: "Franchise" });
    if (access.hasAccess('home.configuration.quoterequest', 0))
      this.tItems.push({ name: "NewQuoteRequest", title: "Add Quote Request", show: access.hasAccess('home.configuration.quoterequest', 1), icon: "event_note", tabName: "Quote Request Workflow Config" });
    if (access.hasAccess('home.configuration.po', 0))
      this.tItems.push({ name: "NewPO", title: "Add Purchase Order", show: access.hasAccess('home.configuration.po', 1), icon: "event_note", tabName: "PO Workflow Config" });
    // { name: "NewDummy", title: "Add NewDummy", show: true }
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
