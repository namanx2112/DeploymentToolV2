import { Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel, UserModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { PartsService } from 'src/app/services/parts.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { StoreService } from 'src/app/services/store.service';
import { TechComponenttService } from 'src/app/services/tech-component.service';
import { UserService } from 'src/app/services/user.service';
import { VendorService } from 'src/app/services/vendor.service';
import { CommonService } from 'src/app/services/common.service';

export interface TableColumnDef {
  columnDef: string,
  header: string
}

@Component({
  selector: 'app-table-expendable',
  templateUrl: './table-expendable.component.html',
  styleUrls: ['./table-expendable.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class TableExpendableComponent {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @Input() curTab: HomeTab;
  @Input() searchFields: any;
  @Input() clickCol: string = "";
  @Input() _refreshMe: Date;
  @Input() set refreshMe(val: Date) {
    this._refreshMe = val;
  }
  @Input()
  curBrandId: number;
  @Output() rowClicked = new EventEmitter<MatRow>();
  columnsToDisplay: TableColumnDef[] = [];
  clickedRows = new Set<any>();

  @ViewChild(MatSort) sort: MatSort;
  cService: any;
  initVal: Date;
  dataSource: any[];
  expandedElement: any;
  columnsToDisplayWithExpand: any;
  constructor(private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseSerice: FranchiseService, private userSerice: UserService, private exService: ExStoreService, private partsService: PartsService,
    private storeService: StoreService) {
    this.initVal = new Date();
  }

  cellClick(row: MatRow) {
    this.rowClicked.emit(row);
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (colName.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  initTable() {
    if (typeof this.curTab != 'undefined') {
      this.columnsToDisplay = [];
      if (this.curTab.tab_type == TabType.Brands) {
        this.cService = this.brandService;
      }
      else if (this.curTab.tab_type == TabType.Vendor) {
        this.cService = this.verndorService;
      }
      else if (this.curTab.tab_type == TabType.TechComponent) {
        this.cService = this.techCompService;
      }
      else if (this.curTab.tab_type == TabType.Franchise) {
        this.cService = this.franchiseSerice;
      }
      else if (this.curTab.tab_type == TabType.Users) {
        this.cService = this.userSerice;
      }
      else if (this.curTab.tab_type == TabType.StoreProjects || this.curTab.tab_type == TabType.HistoricalProjects) {
        this.cService = this.storeService;
      }
      else if (this.curTab.tab_type == TabType.StoreNotes) {
        this.cService = this.exService;
      }
      else if (this.curTab.tab_type == TabType.VendorParts) {
        this.cService = this.partsService;
      }
    }
    this.getTable();
  }

  getTable() {
    let fColumns = this.cService.GetTableVisibleColumns(this.curTab);
    this.expandedElement = fColumns;
    let tCols: any[] = [];
    this.cService.Get(this.searchFields, this.curTab).subscribe((resp: any[]) => {
      for (var indx in this.curTab.fields) {
        if (fColumns.indexOf(this.curTab.fields[indx].fieldUniqeName) == -1)
          continue;
        this.columnsToDisplay.push({
          columnDef: this.curTab.fields[indx].fieldUniqeName,
          header: this.curTab.fields[indx].field_name
        });
        tCols.push(this.curTab.fields[indx].fieldUniqeName);
      }
      this.columnsToDisplayWithExpand = [...tCols, 'expand'];
      this.dataSource = resp;
    });
  }

  ngOnChanges() {
    if (this.initVal != this._refreshMe) {
      this._refreshMe = this.initVal;
      this.initTable();
    }
  }
  ngAfterViewInit2() {
    //this.getTable();
    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    // this.dataSource.filter = filterValue.trim().toLowerCase();

    // if (this.dataSource.paginator) {
    //   this.dataSource.paginator.firstPage();
    // }
  }
}
