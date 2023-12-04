import { Component, Input } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';

@Component({
  selector: 'app-filter-table',
  templateUrl: './filter-table.component.html',
  styleUrls: ['./filter-table.component.css']
})
export class FilterTableComponent {

  @Input()
  set request(val: any) {
    this.initRows();
  }

  rows: any[] = [];
  group: boolean = false;
  fields: any[] = [];
  constructor() {

  }

  initRows() {
    this.initFields();
    this.rows = [{
      andOr: 0,
      field: 1,
      nOperator: 1,
      nValue: 1
    }];
  }

  initFields() {
    this.fields = [{
      tGroupName: "Store Info",
      nFieldId: 1,
      tFieldName: "Store Number",
      nFieldType: 1
    },
    {
      tGroupName: "Store Info",
      nFieldId: 1,
      tFieldName: "City",
      nFieldType: 1
    },
    {
      tGroupName: "Installation",
      nFieldId: 1,
      tFieldName: "Installation Start Date",
      nFieldType: 1
    },
    {
      tGroupName: "Installation",
      nFieldId: 1,
      tFieldName: "Installation End Date",
      nFieldType: 1
    }];
  }
}
