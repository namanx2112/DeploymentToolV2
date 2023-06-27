import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-admin-dashboard-shortcuts',
  templateUrl: './admin-dashboard-shortcuts.component.html',
  styleUrls: ['./admin-dashboard-shortcuts.component.css']
})
export class AdminDashboardShortcutsComponent {

  @Output() ShortcutSelection = new EventEmitter<string>();
  constructor(){}

  ShortcutClick(type: string){
 this.ShortcutSelection.emit(type);
    
  }
}
