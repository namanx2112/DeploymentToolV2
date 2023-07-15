import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-admin-dashboard-shortcuts',
  templateUrl: './admin-dashboard-shortcuts.component.html',
  styleUrls: ['./admin-dashboard-shortcuts.component.css']
})
export class AdminDashboardShortcutsComponent {

  @Output() ShortcutSelection = new EventEmitter<any>();
  tItems: { name: string, title: string, show: boolean, icon: string, tabName: string }[];
  searchText: string;
  constructor() {
    this.searchText = "";
    this.tItems = [
      { name: "NewUser", title: "Add New User", show: true, icon: "account_box", tabName: "Users" },
      { name: "NewBrand", title: "Add New Brand", show: true, icon: "dashboard", tabName: "Brands Profile" },
      { name: "NewVendor", title: "Add New Vendor", show: true, icon: "extension", tabName: "Vendors" },
      { name: "NewDropdown", title: "Add Dropdown", show: true, icon: "shopping_basket", tabName: "Manage Dropdowns" },
      { name: "NewReport", title: "Add New Report", show: true, icon: "featured_play_list", tabName: "Users" },
      { name: "NewTechnologyArea", title: "Add New Technology Area", show: true, icon: "settings_input_component", tabName: "Technology Areas" },
      { name: "Setting", title: "Setting", show: true, icon: "settings", tabName: "Users" },
      { name: "NewFrenchise", title: "Add New Frenchise", show: true, icon: "account_circle", tabName: "Franchise" },
      { name: "NewQuoteRequest", title: "Add Quote Request", show: true, icon: "event_note", tabName: "Quote Request Workflow Config" },
      { name: "NewPO", title: "Add Purchase Order", show: true, icon: "event_note", tabName: "PO Workflow Config" },
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
