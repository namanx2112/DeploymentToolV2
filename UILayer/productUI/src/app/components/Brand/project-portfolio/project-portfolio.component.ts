import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { FormGroup } from '@angular/forms';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { CommonService } from 'src/app/services/common.service';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NotesListComponent } from '../notes-list/notes-list.component';
import { AccessService } from 'src/app/services/access.service';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { MatTableDataSource } from '@angular/material/table';
import { BrandModel, Brands } from 'src/app/interfaces/models';

export interface TableColumnDef {
  columnDef: string,
  header: string
}

@Component({
  selector: 'app-project-portfolio',
  templateUrl: './project-portfolio.component.html',
  styleUrls: ['./project-portfolio.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class ProjectPortfolioComponent {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  columnsToDisplay: TableColumnDef[] = [];
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  _curBrand: BrandModel;
  @Input()
  set curBrand(val: BrandModel) {
    this._curBrand = val;
    this.loadColumns();
  }
  dataSource: MatTableDataSource<any>;;
  expandedElement: any;
  columnsToDisplayWithExpand: any;
  totalRows = 0;
  pageSize = 5;
  currentPage = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  constructor(private dialog: MatDialog, private analyticsService: AnalyticsService, public access: AccessService) {
  }

  ngOnChanges() {

  }

  openStoreClicked(item: any) {
    this.openStore.emit(item);
  }

  pageChanged(event: PageEvent) {
    console.log({ event });
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getRecord();
  }

  loadColumns() {
    let tBrand = CommonService.allBrands.find((x: BrandModel) => x.aBrandId == this._curBrand.aBrandId);
    if (tBrand.nBrandType == Brands.Buffalo) {
      this.columnsToDisplay = [
        {
          columnDef: "store",
          header: ""
        },
        {
          columnDef: "networking",
          header: "Networking"
        }
        , {
          columnDef: "pos",
          header: "POS"
        }, {
          columnDef: "audio",
          header: "Audio"
        }, {
          columnDef: "exteriormenu",
          header: "Exterior Menu"
        }, {
          columnDef: "paymentsystem",
          header: "Payment System"
        }, {
          columnDef: "interiormenu",
          header: "Interior Menu"
        }, {
          columnDef: "serverhandheld",
          header: "Server Handheld"
        }, {
          columnDef: "installation",
          header: "Installation"
        }, {
          columnDef: "notes",
          header: "Notes"
        }
      ];
    }
    else if (tBrand.nBrandType == Brands.Arby) {
      this.columnsToDisplay = [
        {
          columnDef: "store",
          header: ""
        },
        {
          columnDef: "networking",
          header: "Networking"
        }
        , {
          columnDef: "pos",
          header: "POS"
        }, {
          columnDef: "audio",
          header: "Audio"
        }, {
          columnDef: "exteriormenu",
          header: "Exterior Menu"
        }, {
          columnDef: "paymentsystem",
          header: "Payment System"
        }, {
          columnDef: "interiormenu",
          header: "Interior Menu"
        }, {
          columnDef: "sonicradio",
          header: "Radio"
        }, {
          columnDef: "networkswitch",
          header: "Network Switch"
        }, {
          columnDef: "imagememory",
          header: "Image Memory"
        }, {
          columnDef: "installation",
          header: "Installation"
        }, {
          columnDef: "notes",
          header: "Notes"
        }
      ];
    }
    else if (tBrand.nBrandType == Brands.Dunkin) {
      this.columnsToDisplay = [
        {
          columnDef: "store",
          header: ""
        },
        {
          columnDef: "networking",
          header: "Networking"
        }
        , {
          columnDef: "pos",
          header: "POS"
        }, {
          columnDef: "audio",
          header: "Audio"
        }, {
          columnDef: "exteriormenu",
          header: "Exterior Menu"
        }, {
          columnDef: "paymentsystem",
          header: "Payment System"
        }, {
          columnDef: "interiormenu",
          header: "Interior Menu"
        }, {
          columnDef: "orderaccuracy",
          header: "Order Accuracy"
        }, {
          columnDef: "orderstatusboard",
          header: "Order Status Board"
        }, {
          columnDef: "sonicradio",
          header: "Radio"
        }, {
          columnDef: "installation",
          header: "Installation"
        }, {
          columnDef: "notes",
          header: "Notes"
        }
      ];
    }
    else {
      this.columnsToDisplay = [
        {
          columnDef: "store",
          header: ""
        },
        {
          columnDef: "networking",
          header: "Networking"
        }
        , {
          columnDef: "pos",
          header: "POS"
        }, {
          columnDef: "audio",
          header: "Audio"
        }, {
          columnDef: "exteriormenu",
          header: "Exterior Menu"
        }, {
          columnDef: "paymentsystem",
          header: "Payment System"
        }, {
          columnDef: "interiormenu",
          header: "Interior Menu"
        }, {
          columnDef: "sonicradio",
          header: "Sonic Radio"
        }, {
          columnDef: "installation",
          header: "Installation"
        }, {
          columnDef: "notes",
          header: "Notes"
        }
      ];
    }
    let tCols: string[] = [];
    for (var indx in this.columnsToDisplay) {
      tCols.push(this.columnsToDisplay[indx].columnDef);
    }

    this.columnsToDisplayWithExpand = [...tCols, 'expand'];
    this.getRecord();
  }

  getRecord() {
    this.analyticsService.Get({ nBrandId: this._curBrand.aBrandId.toString(), 
      pageSize: this.pageSize.toString(), currentPage: this.currentPage.toString() }).subscribe(x => {
      this.totalRows = x.nTotalRows;
      this.dataSource = new MatTableDataSource(x.response);
      this.dataSource.filterPredicate = (data: any, filter: string) => {
        filter = filter.toLocaleLowerCase();
        return ((data["store"].tStoreNumber != null && data["store"].tStoreNumber.toLowerCase().indexOf(filter) > -1) ||
          (data["store"].tFranchise && data["store"].tFranchise.toLowerCase().indexOf(filter) > -1)
          || (data["store"].tProjectManager && data["store"].tProjectManager.toLowerCase().indexOf(filter) > -1)
          || (data["store"].tProjectType && data["store"].tProjectType.toLowerCase().indexOf(filter) > -1))
      };
    });
  }

  ShowNotes(element: any) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    element.nBrandId = this._curBrand.aBrandId;
    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme",
      curStore: element
    };
    dialogRef = this.dialog.open(NotesListComponent, dialogConfig);
    // dialogRef = this.dialog.open(NotImplementedComponent, dialogConfig);
  }

  applyFilter(event: Event) {
    let filterValue: any;
    filterValue = (event.target as HTMLInputElement).value;
    if (this.dataSource)
      this.dataSource.filter = filterValue.trim().toLowerCase();
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
}
