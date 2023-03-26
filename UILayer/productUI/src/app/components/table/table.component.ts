import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel, UserModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { TechComponenttService } from 'src/app/services/tech-component.service';
import { UserService } from 'src/app/services/user.service';
import { VendorService } from 'src/app/services/vendor.service';


export interface TableColumnDef {
  columnDef: string,
  header: string
}

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})

export class TableComponent {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @Input() curTab: HomeTab;
  @Input() searchFields: any;
  @Output() rowClicked = new EventEmitter<MatRow>();
  displayedColumns: string[] = [];
  columns: TableColumnDef[] = [];
  dataSource: MatTableDataSource<any>;
  clickedRows = new Set<any>();

  @ViewChild(MatSort) sort: MatSort;

  constructor(private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseSerice: FranchiseService, private userSerice: UserService) {
  }

  rowClick(row: MatRow) {
    this.rowClicked.emit(row);
  }

  getTable() {
    if (typeof this.curTab != 'undefined') {
      this.displayedColumns = [];
      this.columns = [];
      if (this.curTab.tab_type == TabType.Brands) {
        this.getBrands(this.searchFields);
      }
      else if (this.curTab.tab_type == TabType.Vendor) {
        this.getVendor(this.searchFields);
      }
      else if (this.curTab.tab_type == TabType.TechComponent) {
        this.getTechComponent(this.searchFields);
      }
      else if (this.curTab.tab_type == TabType.Franchise) {
        this.getFranchise(this.searchFields);
      }
      else if (this.curTab.tab_type == TabType.Users) {
        this.getUsers(this.searchFields);
      }
    }
  }

  getUsers(searchFields: any) {
    this.userSerice.GetUsers(searchFields).subscribe((resp: UserModel[]) => {
      this.dataSource = new MatTableDataSource(resp);
      for (var indx in this.curTab.fields) {
        this.displayedColumns.push(this.curTab.fields[indx].fieldUniqeName);
        this.columns.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
      }
    });
  }

  getFranchise(searchFields: any) {
    this.franchiseSerice.GetFranchises(searchFields).subscribe((resp: FranchiseModel[]) => {
      this.dataSource = new MatTableDataSource(resp);
      for (var indx in this.curTab.fields) {
        this.displayedColumns.push(this.curTab.fields[indx].fieldUniqeName);
        this.columns.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
      }
      // Assign the data to the data source for the table to render

      //
    });
  }


  getVendor(searchFields: any) {
    this.verndorService.GetVendors(searchFields).subscribe((resp: VendorModel[]) => {
      this.dataSource = new MatTableDataSource(resp);
      let fColumns = this.verndorService.GetTableVisibleColumns();
      for (var indx in this.curTab.fields) {
        if (fColumns.indexOf(this.curTab.fields[indx].fieldUniqeName) == -1)
          continue;
        this.displayedColumns.push(this.curTab.fields[indx].fieldUniqeName);
        this.columns.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
      }
      // Assign the data to the data source for the table to render

      //
    });
  }

  getTechComponent(searchFields: any) {
    this.techCompService.GetTechComponents(searchFields).subscribe((resp: TechComponentModel[]) => {
      this.dataSource = new MatTableDataSource(resp);
      for (var indx in this.curTab.fields) {
        this.displayedColumns.push(this.curTab.fields[indx].fieldUniqeName);
        this.columns.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
      }
      // Assign the data to the data source for the table to render

      //
    });
  }

  getBrands(searchFields: any) {
    this.brandService.GetBrands(searchFields).subscribe((resp: BrandModel[]) => {
      this.dataSource = new MatTableDataSource(resp);
      for (var indx in this.curTab.fields) {
        this.displayedColumns.push(this.curTab.fields[indx].fieldUniqeName);
        this.columns.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
      }
      // Assign the data to the data source for the table to render

      //
    });
  }

  ngOnChanges() {
    this.getTable();
  }
  ngAfterViewInit2() {
    this.getTable();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
