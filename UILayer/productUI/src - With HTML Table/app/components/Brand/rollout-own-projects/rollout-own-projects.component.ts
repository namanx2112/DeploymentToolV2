import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ReportModel } from 'src/app/interfaces/analytics';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType } from 'src/app/interfaces/home-tab';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { RolloutProjectsService } from 'src/app/services/rollout-projects.service';

@Component({
  selector: 'app-rollout-own-projects',
  templateUrl: './rollout-own-projects.component.html',
  styleUrls: ['./rollout-own-projects.component.css']
})
export class RolloutOwnProjectsComponent {
  @Input()
  set request(val: any) {
    this._nBrandId = val.nBrandId;
    this.nProjectsRolloutID = val.nProjectsRolloutID;
    this.getMyProjects();
  }
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  @ViewChild('TABLE', { read: ElementRef }) table: ElementRef;
  columns: string[];
  nProjectsRolloutID: number;
  reportTable: Dictionary<string>[];
  dataSource: MatTableDataSource<any>;// = new MatTableDataSource(ELEMENT_DATA);
  _nBrandId: number;
  constructor(private _liveAnnouncer: LiveAnnouncer, private rolloutService: RolloutProjectsService, private service: ExStoreService) {

  }

  @ViewChild(MatSort, { static: false }) sort: MatSort;

  ngOnInit() {
    // get data from API 

  }

  openItem(item: any) {
    this.service.SearchStore(item, this._nBrandId).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  getMyProjects() {
    this.rolloutService.GetMyProjects(this.nProjectsRolloutID).subscribe(x => {
      this.reportTable = x;
      this.loadTable();
    });
  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.toLocaleLowerCase().indexOf("cost") > -1) {
      rVal = "$" + colVal;
    }
    else if (colName.toLocaleLowerCase().indexOf("date") > -1 || colName.toLocaleLowerCase().indexOf("go-live") > -1) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  loadTable() {
    this.columns = [];
    for (var indx in this.reportTable[0]) {
      this.columns.push(indx);
    }
    this.dataSource = new MatTableDataSource(this.reportTable);
    this.dataSource.sort = this.sort;
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
    this.dataSource = new MatTableDataSource(this.reportTable.sort(compare));
    this.dataSource.sort = this.sort;
  }
}
