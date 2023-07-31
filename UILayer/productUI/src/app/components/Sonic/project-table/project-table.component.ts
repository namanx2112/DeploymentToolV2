import { Input, Component, EventEmitter, Output } from '@angular/core';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.css']
})
export class ProjectTableComponent {
  _curStore: StoreSearchModel;
  @Input()
  set curStore(val: StoreSearchModel) {
    this._curStore = val;
    this.tableCondition = { nStoreId: val.nStoreId.toString() };
  }
  @Input()
  set tabName(val: string) {
    this._tabName = val;
    this.getTable();
  }
  get tabName() {
    return this._tabName;
  }
  _tabName: string;
  @Output() ChangeView = new EventEmitter<string>();
  curTab: HomeTab;
  curProj: HomeTab;
  historProj: HomeTab;
  tableCondition: any;
  constructor(private service: SonicService) {
    this.curProj = this.service.GetProjectsTab(TabInstanceType.Table);
    this.curProj.tab_header = "Active Projects";
    this.historProj = this.service.GetHistoricalProjectsTab(TabInstanceType.Table);
    this.historProj.tab_header = "Historical Projects";
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

  goBack() {
    this.ChangeView.emit("storeview");
  }
}
