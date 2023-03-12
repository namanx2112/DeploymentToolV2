import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';


export interface TableColumnDef {
  columnDef: string,
  header: string
}
export interface UserData {
  id: string;
  name: string;
  progress: string;
  fruit: string;
}

/** Constants used to fill up our data base. */
const FRUITS: string[] = [
  'blueberry',
  'lychee',
  'kiwi',
  'mango',
  'peach',
  'lime',
  'pomegranate',
  'pineapple',
];
const NAMES: string[] = [
  'Maia',
  'Asher',
  'Olivia',
  'Atticus',
  'Amelia',
  'Jack',
  'Charlotte',
  'Theodore',
  'Isla',
  'Oliver',
  'Isabella',
  'Jasper',
  'Cora',
  'Levi',
  'Violet',
  'Arthur',
  'Mia',
  'Thomas',
  'Elizabeth',
];

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

  constructor(private brandService: BrandServiceService) {
  }

  rowClick(row: MatRow){
      this.rowClicked.emit(row);
  }

  getTable() {
    if (typeof this.curTab != 'undefined') {
      this.displayedColumns= [];
      this.columns = [];
      if (this.curTab.tab_type == TabType.Brands) {
        this.getBrands(this.searchFields);
      }
      else if (this.curTab.tab_type == TabType.Vendor) {

      }
    }
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

/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
  const name =
    NAMES[Math.round(Math.random() * (NAMES.length - 1))] +
    ' ' +
    NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) +
    '.';

  return {
    id: id.toString(),
    name: name,
    progress: Math.round(Math.random() * 100).toString(),
    fruit: FRUITS[Math.round(Math.random() * (FRUITS.length - 1))],
  };

}
