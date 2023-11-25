import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatRow, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { FieldType, HomeTab, OptionType, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel, FranchiseModel, TechComponentModel, VendorModel, UserModel } from 'src/app/interfaces/models';
import { AccessService } from 'src/app/services/access.service';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { CommonService } from 'src/app/services/common.service';
import { FranchiseService } from 'src/app/services/frenchise.service';
import { NotesService } from 'src/app/services/notes.service';
import { PartsService } from 'src/app/services/parts.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { StoreService } from 'src/app/services/store.service';
import { TechComponenttService } from 'src/app/services/tech-component.service';
import { UserService } from 'src/app/services/user.service';
import { VendorService } from 'src/app/services/vendor.service';
import { RolloutProjectsService } from 'src/app/services/rollout-projects.service';


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
  @Input()
  nBrandId: number;
  @Output() rowClicked = new EventEmitter<MatRow>();
  displayedColumns: string[] = [];
  columns: TableColumnDef[] = [];
  dataSource: MatTableDataSource<any>;
  clickedRows = new Set<any>();

  @ViewChild(MatSort) sort: MatSort;
  cService: any;
  initVal: Date;
  responseData: any;
  constructor(private _liveAnnouncer: LiveAnnouncer, private brandService: BrandServiceService, private techCompService: TechComponenttService, private verndorService: VendorService,
    private franchiseSerice: FranchiseService, private userSerice: UserService, private exService: ExStoreService, private partsService: PartsService,
    private rolloutService: RolloutProjectsService, private storeService: StoreService, public access: AccessService, private noteService: NotesService, private commonService: CommonService) {
    this.initVal = new Date();
  }

  ngAfterViewInit() {
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (colName.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    else {
      let tField = this.curTab.fields.find(x => x.fieldUniqeName == colName)
      if (tField && tField.field_type == FieldType.dropdown && tField.options) {
        let opArr: OptionType[] = this.commonService.GetDropdownOptions(this.nBrandId, tField.options);
        let dVal = opArr.find(x => x.aDropdownId == colVal)?.tDropdownText;
        if (dVal)
          rVal = dVal;
      }
    }
    return rVal;
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
    this.dataSource = new MatTableDataSource(this.responseData.sort(compare));
    this.dataSource.sort = this.sort;
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
        this.cService = this.noteService;
      }
      else if (this.curTab.tab_type == TabType.VendorParts) {
        this.cService = this.partsService;
      }
      else if (this.curTab.tab_type == TabType.RolloutProjects) {
        this.cService = this.rolloutService;
      }
    }
    this.getTable();
  }

  getTable() {
    this.cService.Get(this.searchFields, this.curTab).subscribe((resp: any[]) => {
      this.responseData = resp;
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
