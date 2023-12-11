import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { ConditionOperatorIds, ReportCondtion, ReportEditorModel, ReportField, ReportFieldAndOperatorType } from 'src/app/interfaces/report-generator';
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
    this.curModel = val.curModel;
    this.initRows();
  }

  @Output()
  buttonClick = new EventEmitter<any>();

  curModel: ReportEditorModel;
  curBrand: BrandModel;
  rows: ReportCondtion[] = [];
  group: boolean = false;
  fields: ReportField[] = [];
  operators: ReportFieldAndOperatorType[] = [];
  andOrOperators: any[] = [];
  fieldsByGroup: any = {};
  constructor(private rgService: ReportGeneratorService, private commonService: CommonService) {

  }

  initRows() {
    this.initAndOr();
    this.initFields();
  }

  getNewRow() {
    return {
      nConditionID: -1,
      nRelatedID: -1,
      nAndOr: 0,
      nFieldID: this.fields[0].aFieldID,
      nFieldTypeID: 0,
      field: {},
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
    if (typeof row.field.nFieldTypeID == 'undefined')
      valid = false;
    else {
      if (!(row.nOperatorID == ConditionOperatorIds.IsEmpty || row.nOperatorID == ConditionOperatorIds.IsNotEmpty)) {
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
          case (FieldType.date + 1):
            let regExWithNumber = /^@StartOfDay+(\('[-/+][0-9]+[a-z]+'\))/gim;
            let regEx = /^@StartOfDay|StartOfWeek|StartOfMonth|StartOfYear/gim
            if (row.tValue == "")
              valid = false;
            else if (!RegExp(regEx).test(row.tValue)) {
              if (!RegExp(regExWithNumber).test(row.tValue)) {
                valid = false;
              }
            }
            break;
          case (FieldType.text + 1):
            if (row.tValue == "")
              valid = false;
            break;
        }
      }
    }
    return valid;
  }

  addARow(tCond: ReportCondtion) {
    let tField = this.fields.find(x => x.aFieldID == tCond.nFieldID);
    if (tField)
      tCond.field = tField;
    switch (tCond.nFieldTypeID - 1) {
      case FieldType.dropdown:
        tCond.nArrValues = tCond.tValue.split(',');
        break;
    }
    this.rows.push(tCond);
    this.fieldChanged(this.rows[this.rows.length - 1]);
  }

  initOperators() {
    this.rgService.GetFieldOperatorType(this.curBrand.aBrandId).subscribe(x => {
      this.operators = x;
      if (this.curModel.conditions.length == 0) {
        this.addARow(this.getNewRow());;
      }
      else {
        for (var indx in this.curModel.conditions) {
          let tCond = this.curModel.conditions[indx];
          this.addARow(tCond);
        }
      }
    });
  }

  initFields() {
    this.rgService.GetReportFields(this.curBrand.aBrandId).subscribe(x => {
      this.fields = x;
      this.fieldsByGroup = this.rgService.getFieldByGroup(x);
      this.initOperators();
    });
  }

  compareOperatorDropDown(o1: any, o2: any) {
    if (parseInt(o1) == parseInt(o2))
      return true;
    else return false
  }

  fieldChanged(row: ReportCondtion) {
    row.nFieldID = row.field.aFieldID;
    row.nFieldTypeID = row.field.nFieldTypeID;
    row.operators = this.getMyOperator(row.field.nFieldTypeID);
    if (typeof row.nOperatorID == 'undefined' || row.operators.findIndex(x => x.aOperatorID == row.nOperatorID) == -1)
      row.nOperatorID = row.operators[0].aOperatorID;
    if (typeof row.field != 'undefined' && (row.field.nFieldTypeID - 1) == FieldType.dropdown)
      row.ddOptions = this.commonService.GetDropdownOptions(this.curBrand.aBrandId, row.field.tConstraint, true);
    if (typeof row.field != 'undefined' && (row.field.nFieldTypeID - 1) == FieldType.date)
      row.ddOptions = this.commonService.GetSearchOptionsForDate();
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
