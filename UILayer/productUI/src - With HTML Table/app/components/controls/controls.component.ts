import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { ControlsErrorMessages, Dictionary } from 'src/app/interfaces/commons';
import { Fields, FieldType, HomeTab, OptionType } from 'src/app/interfaces/home-tab';
import * as _moment from 'moment';
import { Moment } from 'moment';
import { CommonService } from 'src/app/services/common.service';

const moment = _moment;

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/DD/YYYY',
  },
  display: {
    dateInput: 'MM/DD/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'DD MM YYYY',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-controls',
  templateUrl: './controls.component.html',
  styleUrls: ['./controls.component.css'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE],
    },
    DatePipe,
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class ControlsComponent implements AfterViewChecked {
  @Input()
  set request(val: any) {
    this.fields = val.fields;
    this.needButton = val.needButton;
    this.themeClass = val.themeClass;
    this.numberOfControlsInARow = val.numberOfControlsInARow;
    this.controlValues = val.controlValues;
    this.SubmitLabel = val.SubmitLabel;
    this.readOnlyForm = val.readOnlyForm;
    this.CloseLabel = val.CloseLabel;
    this.nBrandId = val.nBrandId;
    this.exButtonLabel = val.exButtonLabel;
    this.fieldRestrictions = val.fieldRestrictions;
    this.loadFields();
    this.loadValues();
  }
  fields: Fields[];
  needButton: boolean;
  themeClass: string;
  numberOfControlsInARow: number;
  controlValues: Dictionary<string>;
  exButtonLabel: string;
  SubmitLabel: string;
  readOnlyForm: boolean;
  @Output() onSubmit = new EventEmitter<any>();
  @Output() onClose = new EventEmitter<FormGroup>();
  CloseLabel?: string;
  formGroup = new FormGroup({});
  fieldClass: string;
  groupLabel: string;
  formControls: Dictionary<FormControl>;
  regexPattern =
    /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/gi;
  nBrandId: number;
  fieldRestrictions: any;
  constructor(private readonly changeDetectorRef: ChangeDetectorRef, private datePipe: DatePipe, private dateAdapter: DateAdapter<Date>, public commonService: CommonService) {
    this.formControls = {};
    this.fieldClass = "curField";
    this.groupLabel = "";
    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy
    this.nBrandId = 1;
  }

  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }

  ngAfterContentInit(): void {
    // this.valueChanged();
  }

  compareDropDown(o1: any, o2: any) {
    if (o1 == o2)
      return true;
    else return false
  }

  loadFields() {
    for (var indx in this.fields) {
      let tField = this.fields[indx];
      if (tField.field_type == FieldType.dropdown || tField.field_type == FieldType.multidropdown) {
        if (typeof tField.dropDownOptions == 'undefined') {
          tField.dropDownOptions = this.commonService.GetDropdownOptions(this.nBrandId, tField.options);
        }
      }
      if (typeof this.fieldRestrictions != 'undefined' && typeof this.fieldRestrictions[tField.fieldUniqeName] != 'undefined') {
        if (this.fieldRestrictions[tField.fieldUniqeName] == 1)
          tField.readOnly = true;
        else if (this.fieldRestrictions[tField.fieldUniqeName] == 0)
          tField.hidden = true;
      }
    }
  }

  loadValues() {
    if (typeof this.controlValues == 'undefined' || this.controlValues == null) {
      this.controlValues = {};
      for (var indx in this.fields) {
        let tField = this.fields[indx];
        this.controlValues[tField.fieldUniqeName] = "";
      }
    }
    this.valueChanged();
  }

  valueChanged() {
    this.formGroup = new FormGroup({});
    for (const formField of this.fields) {
      let tmpVal = (formField.field_type == FieldType.date) ? (typeof this.controlValues[formField.fieldUniqeName] == 'undefined' || this.controlValues[formField.fieldUniqeName] == null) ? null : this.controlValues[formField.fieldUniqeName] : (typeof this.controlValues[formField.fieldUniqeName] == 'undefined') ? formField.defaultVal : this.controlValues[formField.fieldUniqeName];
      if (formField.field_type == FieldType.time) {
        this.controlValues[formField.fieldUniqeName] = CommonService.getTimeControlVal(tmpVal);
      }
      let tFormControl = new FormControl(
        tmpVal, formField.validator);
      if (typeof this.controlValues[formField.fieldUniqeName] == 'undefined')
        this.controlValues[formField.fieldUniqeName] = formField.defaultVal;
      if (formField.field_type == FieldType.date)
        tFormControl.addValidators(dateRegexValidator);
      this.formGroup.addControl(formField.fieldUniqeName, tFormControl);
      this.formControls[formField.fieldUniqeName] = tFormControl;
    }
    if (this.numberOfControlsInARow > 0) {
      if (this.numberOfControlsInARow == 1)
        this.fieldClass = "curSingleField";
      else if (this.numberOfControlsInARow == 3)
        this.fieldClass = "curThreeField";
    }
    for (let formField of this.fields) {
      let tControl = this.formGroup.get(formField.fieldUniqeName);
      if (typeof formField.conditionals != 'undefined') {
        this.handleConditionals(tControl?.value.toString(), formField.conditionals);
      }
    }
  }

  getReadOnlyVal(field: Fields, val: string) {
    let sVal = val;
    if (field.field_type == FieldType.dropdown) {
      if (typeof field.options != 'undefined') {
        sVal = CommonService.GetDropDownValueFromControl(field, val, this.controlValues, this.nBrandId);
      }
    }
    else if (field.field_type == FieldType.date) {
      sVal = CommonService.getFormatedDateString(val);
    }
    else if (field.field_type == FieldType.time) {
      sVal = CommonService.getTimeControlVal(val);
    }
    return sVal;
  }

  hasEror(cControl: Fields): boolean {
    let has = false;
    let control = this.formGroup.get(cControl.fieldUniqeName);
    if (typeof control != 'undefined' && control != null) {
      has = !control.valid;
    }
    return has;
  }

  getErrorMessage(cControl: Fields): string {
    let eMsg = "Please enter correct value for " + cControl.field_name;
    let control = this.formGroup.get(cControl.fieldUniqeName);
    if (typeof control != 'undefined' && control != null && control.errors != null) {
      if (control.errors['required'])
        eMsg = ControlsErrorMessages.Requird;
      else if (control.errors['email'])
        eMsg = ControlsErrorMessages.Email;
      else if (control.errors['range'])
        eMsg = ControlsErrorMessages.Range;
    }
    return eMsg;
  }

  getLabelClass(field: Fields): string {
    let clssName = this.fieldClass;
    if (field.hidden)
      clssName = 'curField hidden';
    else if (this.readOnlyForm || field.readOnly) {
      clssName = this.fieldClass + " readOnlyField";
    }
    return clssName;
  }

  getFieldControlValues() {
    var cVal: Dictionary<any> = {};
    let dateValueOptions: string[];
    Object.keys(this.formGroup.controls).forEach(key => {
      if (typeof this.formGroup.get(key)?.value == 'object' && this.formGroup.get(key)?.value != null) {
        if (key.indexOf("d") == 0)
          cVal[key] = CommonService.getFormatedDateStringForDB(this.formGroup.get(key)?.value);
        else
          cVal[key] = this.formGroup.get(key)?.value;
      }
      else
        cVal[key] = this.formGroup.get(key)?.value;
    });
    return cVal;
  }

  dropdownChange(val: any, curControl: Fields) {
    let tItem = curControl.dropDownOptions?.filter(x => x.aDropdownId == val);
    if (tItem && tItem[0].nFunction == 1) {
      let dFieldName = CommonService.GetDropdownDMonthFieldName(curControl);
      this.formGroup.get(dFieldName)?.setValue(new Date());
    }
    else {
      if (curControl.dropDownOptions?.filter(x => x.nFunction == 1)) {
        let dFieldName = CommonService.GetDropdownDMonthFieldName(curControl);
        this.formGroup.get(dFieldName)?.setValue(null);
      }
    }
    if (typeof curControl.conditionals != 'undefined') {
      this.handleConditionals(val, curControl.conditionals);
    }
  }

  handleConditionals(val: string, conditions: any) {
    if (typeof conditions != 'undefined' && typeof conditions["disable"] != 'undefined') {
      let onValues = conditions["disable"].onValues;
      let controls = conditions["disable"].controls;
      let canDisable = (onValues.indexOf(val) > -1);
      for (var indx in controls) {
        let control = this.formGroup.get(controls[indx]);
        if (control != null) {
          if (canDisable)
            control.disable();
          else
            control.enable();
        }
      }
    }
  }

  getOptionValue(curControl: Fields, opt: OptionType) {
    return CommonService.GetDropDownValueFromControlOption(curControl, opt, this.controlValues);
  }

  onSubmitClick(event: any): void {
    if (this.formGroup.valid) {
      this.onSubmit.emit({ value: this.getFieldControlValues(), butttonText: (event.submitter != null) ? event.submitter.innerText : "" });
    }
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
      // if (this.formGroup.valid)
      //   this.onSubmit.emit({ value: this.getFieldControlValues() });
    }
  }

  showGroupHeader(curField: Fields): boolean {
    let show = false;
    if (curField.field_group) {
      if (this.groupLabel != curField.field_group) {
        this.groupLabel = curField.field_group;
        show = true;
      }
    }
    return show;
  }

  CloseClicked(ev: any) {
    this.onClose?.emit(this.formGroup);
  }

  filterOption(request: OptionType) {
    return (typeof request.bDeleted == 'undefined' || request.bDeleted == null || request.bDeleted == false) ? true : false;
  }
}

export function dateRegexValidator(
  control: AbstractControl
): { [key: string]: boolean } | null {
  if (control.value == null || control.value.toString() == 'Invalid Date')
    return null;
  const regexPattern = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/gi;
  let val = control.value;
  const isValid = regexPattern.test(val);
  if (isValid) {
    return { dateRegex: true };
  }
  return null;
}