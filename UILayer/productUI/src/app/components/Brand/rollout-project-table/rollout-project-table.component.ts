import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, Input, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType, Fields, OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { ProjectTypes } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { HomeService } from 'src/app/services/home.service';
import { StoreService } from 'src/app/services/store.service';

export interface TableColumnDef {
  columnDef: string,
  header: string
}

@Component({
  selector: 'app-rollout-project-table',
  templateUrl: './rollout-project-table.component.html',
  styleUrls: ['./rollout-project-table.component.css']
})
export class RolloutProjectTableComponent {
  @Input()
  set request(val: any) {
    this.items = val.items;
    this.dataSource = new MatTableDataSource(this.items);
    this.dataSource.sort = this.sort;
    this.projectType = val.projectType;
    this.nBrandId = val.nBrandId;
    this.getFieldHeaders();
  }
  dataSource: MatTableDataSource<any>;
  nBrandId: number;
  projectType: ProjectTypes;
  items: any;
  columns: TableColumnDef[] = [];
  displayedColumns: string[] = [];
  @ViewChild(MatSort) sort: MatSort;
  constructor(private _liveAnnouncer: LiveAnnouncer, private storeService: ExStoreService, private commonService: CommonService) {

  }

  rowClick(row: MatRow) {
  }

  announceSortChange(sortState: Sort) {
    // This example uses English messages. If your application supports
    // multiple language, you would internationalize these strings.
    // Furthermore, you can customize the message to add additional
    // details about the values being sorted.
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }

    var compare = function (a: any, b: any) {
      if (a[sortState.active].toLowerCase() < b[sortState.active].toLowerCase()) {
        return -1;
      }
      if (a[sortState.active].toLowerCase() > b[sortState.active].toLowerCase()) {
        return 1;
      }
      return 0;
    }
    this.dataSource = new MatTableDataSource(this.items.sort(compare));
    this.dataSource.sort = this.sort;
  }

  getCellVal(tField: string, colVal: string) {
    let rVal = colVal;
    if (tField.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (tField.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  fetchHeaderName(fieldName: string) {
    let newName = fieldName.substring(1, fieldName.length);
    newName = newName.replace(/([A-Z])/g, ' $&');
    return newName;
  }

  getFieldHeaders() {
    for (var cName in this.items[0]) {
      if (cName.toLowerCase() == "nbrandid")
        continue;
      let dName = this.fetchHeaderName(cName);
      this.columns.push({
        columnDef: cName,
        header: dName
      });
      this.displayedColumns.push(cName);
    }
    // if (this.projectType == ProjectTypes.OrderAccuracyInstallation) {
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("Project Type");
    //   this.allFiels.push("tStoreNumber"); this.displayedColumns.push("Store Number");
    //   this.allFiels.push("tAddress"); this.displayedColumns.push("Address");
    //   this.allFiels.push("tCity"); this.displayedColumns.push("City");
    //   this.allFiels.push("tState"); this.displayedColumns.push("State");
    //   this.allFiels.push("DMAID"); this.displayedColumns.push("DMAID");
    //   this.allFiels.push("tDMA"); this.displayedColumns.push("DMA");
    //   this.allFiels.push("tRED"); this.displayedColumns.push("RED");
    //   this.allFiels.push("tCM"); this.displayedColumns.push("CM");
    //   this.allFiels.push("tANE"); this.displayedColumns.push("ANE");
    //   this.allFiels.push("tRVP"); this.displayedColumns.push("RVP");
    //   this.allFiels.push("tPrincipalPartner"); this.displayedColumns.push("Principal Partner");
    //   this.allFiels.push("dStatus"); this.displayedColumns.push("Status");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    //   this.allFiels.push("tProjectType"); this.displayedColumns.push("PType");
    // }
    // else if (this.projectType == ProjectTypes.OrderStatusBoardInstallation) {
    //   this.allFiels = this.storeService.GetStoreOrderStatusBoardTab(TabInstanceType.Single).fields;
    //   this.allFiels.push(...this.storeService.GetStoreInsallationTab(TabInstanceType.Single).fields);
    // }
    // else if (this.projectType == ProjectTypes.ArbysHPRolloutInstallation) {
    //   this.allFiels = this.storeService.GetStoreNetworkSwitchTab(TabInstanceType.Single).fields;
    //   this.allFiels.push(...this.storeService.GetStoreImageMemoryTab(TabInstanceType.Single).fields);
    //   this.allFiels.push(...this.storeService.GetStoreInsallationTab(TabInstanceType.Single).fields);
    // }
  }
}
