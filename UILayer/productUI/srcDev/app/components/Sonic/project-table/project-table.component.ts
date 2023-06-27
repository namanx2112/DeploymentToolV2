import { Component, EventEmitter, Output } from '@angular/core';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.css']
})
export class ProjectTableComponent {

  @Output() ChangeView = new EventEmitter<string>();
  tabName: string;
  curTab: HomeTab;
  curProj: HomeTab;
  historProj: HomeTab;
  constructor(private service: SonicService) {
    this.tabName = "Active";
    this.curProj = this.service.GetProjectsTab(TabInstanceType.Table);
    this.curProj.tab_header = "Active Projects";
    this.historProj = this.service.GetProjectsTab(TabInstanceType.Table);
    this.historProj.tab_header = "Historical Projects";
    this.getTable();
  }

  changeTab(name: string) {
    this.tabName = name;
    this.getTable();
  }

  getTable() {
    if (this.tabName == 'Active') {
      this.curTab = this.curProj;
    }
    else {
      this.curTab = this.historProj;
    }
  }

  rowClicked(row: any) {
    // this.innerControlValues = {};
    // for (var i in row) {
    //   this.innerControlValues[i] = row[i];
    // }
  }

  goBack(){
    this.ChangeView.emit("storeview");
  }
}
