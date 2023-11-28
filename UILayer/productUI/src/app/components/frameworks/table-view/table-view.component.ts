import { animate, style, transition, trigger } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSort, Sort, SortDirection } from '@angular/material/sort';
import { LoadingService } from 'src/app/services/loading.service';

export interface TableColumnDef {
  columnDef: string,
  header: string
}

@Component({
  selector: 'app-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.css'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(500, style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ opacity: 1 }),
        animate(500, style({ opacity: 0 }))
      ])
    ])
  ]
})
export class TableViewComponent {

  @Input()
  set request(val: any) {
    this.items = val.items;
    this.hasHeaderName = (typeof val.hasHeaderName != 'undefined') ? val.hasHeaderName : true;
    this.needCheckBox = (typeof val.needCheckBox != 'undefined') ? val.needCheckBox : false;
    this.exColumns = (typeof val.exColumns != 'undefined') ? val.exColumns : [];
    this.hideColumns = (typeof val.hideColumns != 'undefined') ? val.hideColumns : [];
    this.initializeMe(val.sortColumn);
    this.sortDirection = 'asc';
    this.loaded = true;
  }
  hasHeaderName: boolean;
  items: any;
  exColumns: any;
  columns: TableColumnDef[] = [];
  hideColumns: string[] = [];
  needCheckBox: boolean;
  @Output()
  SelectionChange = new EventEmitter<any[]>();
  selection = new SelectionModel<any>(true, []);
  loaded: boolean;
  sortColumn: string = "";
  sortDirection: SortDirection = '';
  constructor(private loadingService: LoadingService) {

  }

  fetchHeaderName(fieldName: string) {
    let newName = fieldName.substring(1, fieldName.length);
    newName = newName.replace(/([A-Z])/g, ' $&');
    return newName;
  }

  initializeMe(sortCol: any) {
    this.columns = [];
    for (var cName in this.items[0]) {
      if (this.hideColumns.indexOf(cName) > -1)
        continue;
      let dName = (this.hasHeaderName) ? cName : this.fetchHeaderName(cName);
      this.columns.push({
        columnDef: cName,
        header: dName
      });
    }
    if (typeof sortCol == 'undefined')
      this.sortColumn = this.columns[0].columnDef;
    else
      this.sortColumn = sortCol;
  }

  getExColVal(colName: string, row: any) {
    let val = "";
    if (this.exColumns[colName]) {
      let cVal = this.exColumns[colName];
      if (row[cVal.fromColumn] && cVal.values[row[cVal.fromColumn]]) {
        val = cVal.values[row[cVal.fromColumn]];
      }
    }
    return val;
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

  refreshTable() {
    this.loaded = false;
    setTimeout(() => {
      this.loaded = true;
    }, 1);
  }

  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
  }

  sortData(sort: Sort) {
    this.loading(true);
    const data = this.items;
    this.sortColumn = sort.active;
    this.sortDirection = sort.direction;
    if (!sort.active || sort.direction === '') {
      this.items = data;
      this.refreshTable();
      this.loading(false);
      return;
    }
    this.items = [];
    this.items = data.sort((a: any, b: any) => {
      const isAsc = sort.direction === 'asc';
      return this.compare(a[sort.active], b[sort.active], isAsc);
    });
    this.refreshTable();
    this.loading(false);
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
}
