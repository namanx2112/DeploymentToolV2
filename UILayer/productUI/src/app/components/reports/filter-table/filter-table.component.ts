import { Component, EventEmitter, Input, Output } from '@angular/core';
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

  @Output()
  buttonClick = new EventEmitter<any>();

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
      nAndOr: 0,
      nFieldID: this.fields[0].aFieldID,
      nFieldTypeID: 0,
      field: this.fields[0],
      nOperatorID: 1,
      ddOptions: [],
      operators: [],
      nArrValues: [],
      nValue: -1,
      tValue: "",
      cValue: -1
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

  cantSubmit() {
    let valid = false;
    for (var indx in this.rows) {
      if (!this.isRowValid(this.rows[indx])) {
        valid = true;
        break;
      }
    }
    return valid;
  }

  submitMe() {
    this.buttonClick.emit({ action: "submit", rows: this.rows });
  }

  cancel() {
    this.buttonClick.emit({ action: "cancel", rows: this.rows });
  }

  isRowValid(row: any) {
    let valid = true;
    switch (row.field.nFieldTypeID) {
      case (FieldType.currency + 1):
        if (row.tValue == "")
          valid = false;
        else
          row.cValue = row.tValue;
        break;
      case (FieldType.number + 1):
        if (row.tValue == "")
          valid = false;
        else
          row.nValue = row.tValue;
        break;
      case (FieldType.dropdown + 1):
        if (row.nArrValues.length == 0)
          valid = false;
        else
          row.tValue = row.nArrValues.join(",");
        break;
    }
    return valid;
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
    row.nFieldID = row.field.aFieldID;
    row.nFieldTypeID = row.field.aFieldID;
    row.operators = this.getMyOperator(row.field.nFieldTypeID);
    row.nOperatorID = row.operators[0].aOperatorID;
    if (typeof row.field != 'undefined' && (row.field.nFieldTypeID - 1) == FieldType.dropdown)
      row.ddOptions = this.commonService.GetDropdownOptions(this.curBrand.aBrandId, row.field.tConstraint, true);
  }

  getMyOperator(myTypeId: number) {
    return this.operators.filter(x => x.nFieldTypeID == myTypeId);
  }

  addRow(index: number) {
    this.rows.splice(index + 1, 0, this.getNewRow());
    this.fieldChanged(this.rows[index + 1]);
  }

  delRow(index: number) {
    this.rows.splice(index, 1);
  }
}
