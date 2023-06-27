import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SonicProjectExcel } from 'src/app/interfaces/sonic';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-file-store-selection',
  templateUrl: './file-store-selection.component.html',
  styleUrls: ['./file-store-selection.component.css']
})
export class FileStoreSelectionComponent {
  _excelData: SonicProjectExcel[];
  @Input() set excelData(value: SonicProjectExcel[]) {
    this._excelData = value;
    this.dataSource = new MatTableDataSource<SonicProjectExcel>(value);
  }
  @Output()
  SelectionChange = new EventEmitter<SonicProjectExcel[]>();
  displayedColumns: string[] = ['select', 'nStoreExistStatus', 'tProjectType', 'tStoreNumber', 'tAddress',
    'tCity', 'tState', 'nDMAID', 'tDMA', 'tRED', 'tCM', 'tANE', 'tRVP', 'tPrincipalPartner',
    'dStatus', 'dOpenStore', 'tProjectStatus'];
  displayedColumnTitles: string[] = ['nRowIndex', 'Store Exist', 'Project Type', 'Store Number', 'Address',
    'City', 'State', 'DMA', 'ID', 'DMA', 'RED', 'CM', 'A&E', 'RVP', 'Principal Partner',
    'Status', 'Open Store', 'Project Status'];
  dataSource = new MatTableDataSource<SonicProjectExcel>(this.excelData);
  selection = new SelectionModel<SonicProjectExcel>(true, []);
  constructor(private service: SonicService) {

  }

  checkboxChange(event: any, row: any){
    if(event)
      this.selection.toggle(row);
    this.SelectionChange.emit(this.selection.selected);
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

  checkboxLabel(row?: SonicProjectExcel): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
  }
}
