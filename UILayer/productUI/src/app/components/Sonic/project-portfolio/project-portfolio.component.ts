import { Component, ViewChild } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { FormGroup } from '@angular/forms';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { MatPaginator } from '@angular/material/paginator';
import { CommonService } from 'src/app/services/common.service';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NotesListComponent } from '../notes-list/notes-list.component';
import { AccessService } from 'src/app/services/access.service';

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
  dataSource: any[];
  expandedElement: any;
  columnsToDisplayWithExpand: any;
  constructor(private dialog: MatDialog, private analyticsService: AnalyticsService, public access: AccessService) {
    this.loadColumns();
  }

  ngOnChanges() {

  }

  loadColumns() {
    this.columnsToDisplay = [
      {
        columnDef: "store",
        header: ""
      },
      {
        columnDef: "networking",
        header: "Netorking"
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
    let tCols: string[] = [];
    for (var indx in this.columnsToDisplay) {
      tCols.push(this.columnsToDisplay[indx].columnDef);
    }

    this.columnsToDisplayWithExpand = [...tCols, 'expand'];
    this.getRecord();
  }

  getRecord() {
    this.analyticsService.Get(null).subscribe(x => {
      this.dataSource = x;
    });
  }

  ShowNotes(element: any) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

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
