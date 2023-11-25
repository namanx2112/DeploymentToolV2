import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType, Fields, OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { ProjectTypes } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { HomeService } from 'src/app/services/home.service';
import { LoadingService } from 'src/app/services/loading.service';
import { StoreService } from 'src/app/services/store.service';
import { ScrollingModule } from '@angular/cdk/scrolling';

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
    if (typeof val.needCheckBox != 'undefined')
      this.needCheckBox = val.needCheckBox;
    this.getFieldHeaders();
  }
  @Output()
  SelectionChange = new EventEmitter<any[]>();
  selection = new SelectionModel<any>(true, []);
  items: any;
  columns: TableColumnDef[] = [];
  needCheckBox: boolean = false;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private _liveAnnouncer: LiveAnnouncer, private storeService: ExStoreService,
    private commonService: CommonService, private loadingService: LoadingService) {

  }

  rowClick(row: MatRow) {
  }

  announceSortChange(sortState: Sort) {
    // This example uses English messages. If your application supports
    // multiple language, you would internationalize these strings.
    // Furthermore, you can customize the message to add additional
    // details about the values being sorted.
    this.loading(true);
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }

    var compare = function (a: any, b: any) {
      if (a[sortState.active] < b[sortState.active]) {
        return -1;
      }
      if (a[sortState.active] > b[sortState.active]) {
        return 1;
      }
      return 0;
    }
    this.loading(false);
  }

  checkboxChange(event: any, row: any) {
    if (event)
      this.selection.toggle(row);
    this.SelectionChange.emit(this.selection.selected);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.items.length;
    return numSelected === numRows;
  }

  loading(load: boolean) {
    if (load)
      setTimeout(() => {
        this.loadingService.startLoading();
      }, 1);
    else
      setTimeout(() => {
        this.loadingService.stopLoading();
      }, 1);
  }

  toggleAllRows() {
    this.loading(true);
    if (this.isAllSelected()) {
      this.selection.clear();
      this.loading(false);
      this.SelectionChange.emit(this.selection.selected);
      return;
    }
    this.selection.select(...this.items);
    this.loading(false);
    this.SelectionChange.emit(this.selection.selected);
  }

  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
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
      if (cName == "nBrandId" || cName == "nProjectId" || cName == "nStoreId")
        continue;
      if (this.needCheckBox && cName == "nStoreExistStatus")
        continue;
      let dName = this.fetchHeaderName(cName);
      this.columns.push({
        columnDef: cName,
        header: dName
      });
    }
  }
}
