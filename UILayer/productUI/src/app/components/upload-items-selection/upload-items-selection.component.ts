import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { FieldType, HomeTab, OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { ProjectExcel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-upload-items-selection',
  templateUrl: './upload-items-selection.component.html',
  styleUrls: ['./upload-items-selection.component.css']
})
export class UploadItemsSelectionComponent {
  _excelData: any[];
  @Input() set excelData(value: any) {
    this._excelData = value.items;
    this._curTab = value.curTab;
    this.setColumnHeaders();
    this.dataSource = new MatTableDataSource<ProjectExcel>(value.items);
  }
  @Output()
  SelectionChange = new EventEmitter<any[]>();
  _curTab: HomeTab;
  displayedColumns: string[] = [];
  displayedColumnTitles: string[] = [];
  dataSource = new MatTableDataSource<any>(this.excelData);
  selection = new SelectionModel<any>(true, []);
  constructor(private service: ExStoreService, private commonService: CommonService) {
  }

  setColumnHeaders() {
    this.displayedColumns = ['select', 'isExist'];
    this.displayedColumnTitles = ['select', 'Add/Update'];
    for (let indx in this._curTab.fields) {
      let cField = this._curTab.fields[indx];
      if (cField.forImport == true) {
        this.displayedColumnTitles.push(cField.field_name);
        this.displayedColumns.push(cField.fieldUniqeName);
      }
    }
  }

  notFirst(column: string) {
    return column != "select";
  }

  getFormatedDate(colVal: string) {
    return CommonService.getFormatedDateString(colVal);
  }

  checkboxChange(event: any, row: any) {
    if (event)
      this.selection.toggle(row);
    this.SelectionChange.emit(this.selection.selected);
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (colName.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    else if(colName == "isExist"){
      rVal = (colVal) ? "Update" : "Add";
    }
    else {
      let tField = this._curTab.fields.find(x => x.fieldUniqeName == colName)
      if (tField && tField.field_type == FieldType.dropdown && tField.options) {
        let opArr: OptionType[] = this.commonService.GetDropdownOptions(-1, tField.options);
        let dVal = opArr.find(x => x.aDropdownId == colVal)?.tDropdownText;
        if (dVal)
          rVal = dVal;
      }
    }
    return rVal;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      this.SelectionChange.emit(this.selection.selected);
      return;
    }

    this.selection.select(...this.dataSource.data);
    this.SelectionChange.emit(this.selection.selected);
  }

  checkboxLabel(row?: ProjectExcel): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
  }
}
