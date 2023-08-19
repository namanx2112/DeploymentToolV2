import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ViewChild, Inject } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { SupportContent } from 'src/app/interfaces/models';
import { SupportService } from 'src/app/services/support.service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import html2canvas from 'html2canvas';
import { SupportPageComponent } from '../support-page/support-page.component';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-ticket-view',
  templateUrl: './ticket-view.component.html',
  styleUrls: ['./ticket-view.component.css']
})
export class TicketViewComponent implements AfterViewInit {
  allTickets: SupportContent[];
  dataSource: MatTableDataSource<SupportContent>;
  init: boolean;
  displayedColumns: string[] = ['aTicketId', 'nPriority', 'tTicketStatus', 'tContent', 'tFixComment', 'tCreatedBy', 'dtCreatedOn'];
  @ViewChild(MatSort) sort: MatSort;
  respValue: any;
  constructor(private dialog: MatDialog, public dialogRef: MatDialogRef<TicketViewComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private supportService: SupportService, private _liveAnnouncer: LiveAnnouncer) {
    this.init = false;
  }

  getTickets() {
    this.supportService.GetAll().subscribe((x: SupportContent[]) => {
      this.respValue = x;
      this.dataSource = new MatTableDataSource(x);
      this.dataSource.filterPredicate = (data: any, filter: string) => {
        filter = filter.toLocaleLowerCase();
        return ((data["tContent"] && data["tContent"].toLowerCase().indexOf(filter) > -1) || (data["tTicketStatus"] && data["tTicketStatus"].toLowerCase().indexOf(filter) > -1)
          || (data["tFixComment"] && data["tFixComment"].toLowerCase().indexOf(filter) > -1))
      };
      this.dataSource.sort = this.sort;
      this.init = true;
    });
  }

  getPriority(nPriority: number) {
    let tVal = "";
    switch (nPriority) {
      case 0:
        tVal = "Low"
        break;
      case 1:
        tVal = "Medium";
        break;
      case 2:
        tVal = "High";
        break;
      case 3:
        tVal = "Show Stopper";
        break;
    }
    return tVal;
  }

  getDateCellVal(colVal: string) {
    let rVal = CommonService.getFormatedDateString(colVal);
    return rVal;
  }


  rowClick(item: SupportContent) {
    let cThis = this;
    this.getScreenshot(function (bytes: any) {
      const dialogConfig = new MatDialogConfig();
      let dialogRef: any;
      dialogConfig.autoFocus = true;
      dialogConfig.height = '80%';
      dialogConfig.width = '60%';
      dialogConfig.data = {
        nTicketId: item.aTicketId,
        onSubmit: function (data: any) {
          dialogRef.close();
        }
      };
      dialogRef = cThis.dialog.open(SupportPageComponent, dialogConfig);
    });
  }

  onNew() {
    let cThis = this;
    this.getScreenshot(function (bytes: any) {
      const dialogConfig = new MatDialogConfig();
      let dialogRef: any;
      dialogConfig.autoFocus = true;
      dialogConfig.height = '80%';
      dialogConfig.width = '60%';
      dialogConfig.data = {
        fileBytes: bytes,
        onSubmit: function (data: any) {
          dialogRef.close();
        }
      };
      dialogRef = cThis.dialog.open(SupportPageComponent, dialogConfig);
    });
  }

  getScreenshot(callBack: any) {
    const div = document.body;
    if (div) {
      const options = {
        background: 'white',
        scale: 3
      };

      html2canvas(div, options).then((canvas) => {
        //var base64URL = canvas.toDataURL('image/jpeg').replace('image/jpeg', 'image/octet-stream');
        callBack(canvas.toDataURL('image/jpeg'))
      });
    }
  }

  applyFilter(event: Event) {
    let filterValue: any;
    filterValue = (event.target as HTMLInputElement).value;
    if (this.dataSource)
      this.dataSource.filter = filterValue.trim().toLowerCase();
  }




  ngAfterViewInit() {
    this.getTickets();
  }

  /** Announce the change in sort state for assistive technology. */
  announceSortChange(sortState: any) {
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
    this.dataSource = new MatTableDataSource(this.respValue.sort(compare));
    this.dataSource.sort = this.sort;
  }
}
