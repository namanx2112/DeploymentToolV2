import { Component, Input } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportField, ReportFieldAndOperatorType } from 'src/app/interfaces/report-generator';
import { CommonService } from 'src/app/services/common.service';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';

@Component({
  selector: 'app-filter-table',
  templateUrl: './filter-table.component.html',
  styleUrls: ['./filter-table.component.css']
})
export class FilterTableComponent {

  @Input()
  set request(val: any) {
    this.curBrand = val.curBrand;
    this.initRows();
  }

  curBrand: BrandModel;
  rows: any[] = [];
  group: boolean = false;
  fields: ReportField[] = [];
  operators: ReportFieldAndOperatorType[] = [];
  andOrOperators: any[] = [];
  constructor(private rgService: ReportGeneratorService, private commonService: CommonService) {

  }

  initRows() {
    this.initAndOr();
    this.initFields();
  }

  getNewRow() {
    return {
      andOr: 0,
      fieldId: this.fields[0].aReportFieldID,
      field: this.fields[0],
      nOperator: 1,
      ddOptions: [],
      operators: [],
      nValues: [],
      tValue: ""
    };
  }

  initAndOr() {
    this.andOrOperators = [{
      opId: 0,
      text: "And"
    },
    {
      opId: 1,
      text: "Or"
    }];
  }

  initOperators() {
    this.rgService.GetFieldOperatorType(this.curBrand.aBrandId).subscribe(x => {
      this.operators = x;
      this.rows.push(this.getNewRow());
      this.fieldChanged(this.rows[0]);
    });
  }

  initFields() {
    this.rgService.GetReportFields(this.curBrand.aBrandId).subscribe(x => {
      this.fields = x;
      this.initOperators();
    });
  }

  compareOperatorDropDown(o1: any, o2: any) {
    if (parseInt(o1) == parseInt(o2))
      return true;
    else return false
  }

  fieldChanged(row: any) {
    row.fieldId = row.field.aReportFieldID;
    row.operators = this.getMyOperator(row.field.nFieldTypeID);
    row.nOperator = row.operators[0].aFieldTypeOperatorRelID;
    if (typeof row.field != 'undefined' && (row.field.nFieldTypeID - 1) == FieldType.dropdown)
      row.ddOptions = this.commonService.GetDropdownOptions(this.curBrand.aBrandId, row.field.tConstraint);
  }

  getMyOperator(myTypeId: number) {
    return this.operators.filter(x => x.nFieldTypeID == myTypeId);
  }

  addRow(index: number) {
    this.rows.splice(index, 0, this.getNewRow());
  }

  delRow(index: number) {
    this.rows.splice(index, 1);
  }
}
