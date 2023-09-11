import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BrandModel } from 'src/app/interfaces/models';
import { ProjectExcel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-file-store-selection',
  templateUrl: './file-store-selection.component.html',
  styleUrls: ['./file-store-selection.component.css']
})
export class FileStoreSelectionComponent {
  _excelData: ProjectExcel[];
  @Input() set excelData(value: ProjectExcel[]) {
    this._excelData = value;
    this.setColumnHeaders(value[0].nBrandId);
    this.dataSource = new MatTableDataSource<ProjectExcel>(value);
  }
  @Output()
  SelectionChange = new EventEmitter<ProjectExcel[]>();
  displayedColumns: string[] = ['select', 'nStoreExistStatus', 'tProjectType', 'tStoreNumber', 'tAddress',
    'tCity', 'tState', 'nDMAID', 'tDMA', 'tRED', 'tCM', 'tANE', 'tRVP', 'tPrincipalPartner',
    'dStatus', 'dOpenStore', 'tProjectStatus'];
  displayedColumnTitles: string[] = ['nRowIndex', 'Store Exist', 'Project Type', 'Store Number', 'Address',
    'City', 'State', 'DMA ID', 'DMA', 'RED', 'CM', 'A&E', 'RVP', 'Principal Partner',
    'Status', 'Open Store', 'Project Status'];
  dataSource = new MatTableDataSource<ProjectExcel>(this.excelData);
  selection = new SelectionModel<ProjectExcel>(true, []);
  isBuffaloBrand: boolean;
  constructor(private service: ExStoreService) {
  }

  setColumnHeaders(nBrandId: number) {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == nBrandId);
    if (tBrand.tBrandName.toLowerCase().indexOf("buffalo") > -1) {
      this.isBuffaloBrand = true;
      this.displayedColumns = ['select', 'nStoreExistStatus', 'tProjectType', 'tStoreNumber', 'tAddress',
        'tCity', 'tState', 'nDMAID', 'tDMA', 'tRED', 'tCM', 'tANE', 'tRVP', 'tPrincipalPartner',
        'dStatus', 'dOpenStore', 'tProjectStatus', 'nNumberOfTabletsPerStore', 'tEquipmentVendor', 'dDeliveryDate', 'dRevisitDate', 'dInstallDate', 'tInstallationVendor', 'tInstallStatus'];
      this.displayedColumnTitles = ['nRowIndex', 'Store Exist', 'Project Type', 'Store Number', 'Address',
        'City', 'State', 'DMA ID', 'DMA', 'RED', 'CM', 'A&E', 'RVP', 'Principal Partner',
        'Status', 'Open Store', 'Project Status', '#of Teblets per Store', 'Equipment Vendor', 'Delivery Date', 'Revisit Date', 'Install Date', 'Installation Vendor', 'Install Status'];
    }
    else {
      this.isBuffaloBrand = false;
      this.displayedColumns = ['select', 'nStoreExistStatus', 'tProjectType', 'tStoreNumber', 'tAddress',
        'tCity', 'tState', 'nDMAID', 'tDMA', 'tRED', 'tCM', 'tANE', 'tRVP', 'tPrincipalPartner',
        'dStatus', 'dOpenStore', 'tProjectStatus'];
      this.displayedColumnTitles = ['nRowIndex', 'Store Exist', 'Project Type', 'Store Number', 'Address',
        'City', 'State', 'DMA ID', 'DMA', 'RED', 'CM', 'A&E', 'RVP', 'Principal Partner',
        'Status', 'Open Store', 'Project Status'];
    }
  }

  checkboxChange(event: any, row: any) {
    if (event)
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

  checkboxLabel(row?: ProjectExcel): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row`;
  }
}
