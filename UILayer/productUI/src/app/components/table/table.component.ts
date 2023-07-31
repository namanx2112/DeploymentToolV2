import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatRow, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel, UserModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { PartsService } from 'src/app/services/parts.service';
import { SonicService } from 'src/app/services/sonic.service';
import { StoreService } from 'src/app/services/store.service';
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

export class TableComponent implements OnChanges, AfterViewInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @Input() curTab: HomeTab;
  @Input() searchFields: any;
  @Input() clickCol: string = "";
  @Input() _refreshMe: Date;
  @Input() set refreshMe(val: Date) {
    this._refreshMe = val;
  }
  @Output() rowClicked = new EventEmitter<MatRow>();
  displayedColumns: string[] = [];
  columns: TableColumnDef[] = [];
  dataSource: MatTableDataSource<any>;
  clickedRows = new Set<any>();

  @ViewChild(MatSort) sort: MatSort;
  cService: any;
  initVal: Date;
  constructor(private _liveAnnouncer: LiveAnnouncer, private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseSerice: FranchiseService, private userSerice: UserService, private sonicService: SonicService, private partsService: PartsService,
    private storeService: StoreService) {
    this.initVal = new Date();
  }

  ngAfterViewInit() {
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
  }


  cellClick(row: MatRow) {
    this.rowClicked.emit(row);
  }

  cellDelete(row: MatRow) {
    if (confirm("Are you sure you want to delete this item?")) {
      this.cService.Delete(row).subscribe((x: any) => {
        this.initTable();
      });
    }
  }

  rowClick(row: MatRow) {
    if (this.clickCol == 'all')
      this.rowClicked.emit(row);
  }

  initTable() {
    if (typeof this.curTab != 'undefined') {
      this.displayedColumns = [];
      this.columns = [];
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
        this.cService = this.sonicService;
      }
      else if (this.curTab.tab_type == TabType.VendorParts) {
        this.cService = this.partsService;
      }
    }
    this.getTable();
  }

  getTable() {
    this.cService.Get(this.searchFields, this.curTab).subscribe((resp: any[]) => {
      this.dataSource = new MatTableDataSource(resp);
      this.dataSource.sort = this.sort;
      let fColumns = this.cService.GetTableVisibleColumns(this.curTab);
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

  ngOnChanges() {
    if (this.initVal != this._refreshMe) {
      this._refreshMe = this.initVal;
      this.initTable();
    }
  }
  ngAfterViewInit2() {
    //this.getTable();
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
