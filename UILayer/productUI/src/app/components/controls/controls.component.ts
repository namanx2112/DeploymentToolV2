import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { ControlsErrorMessages, Dictionary } from 'src/app/interfaces/commons';
import { Fields, FieldType, HomeTab } from 'src/app/interfaces/home-tab';
import * as _moment from 'moment';
import { Moment } from 'moment';

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
  private _controlValues: Dictionary<string>;
  @Input() fields: Fields[];
  @Input() needButton: boolean;
  @Input() themeClass: string;
  @Input() numberOfControlsInARow: number;
  @Input() set controlValues(value: Dictionary<string>) {
    this._controlValues = value;
    this.checkBlankAndSet();
    this.valueChanged();
  };
  get controlValues(): Dictionary<any> {
    return this._controlValues;
  }
  @Input() SubmitLabel: string;
  @Input() readOnlyForm: boolean;
  @Output() onSubmit = new EventEmitter<any>();
  @Output() onClose = new EventEmitter<FormGroup>();
  @Input() CloseLabel?: string;
  formGroup = new FormGroup({});
  fieldClass: string;
  groupLabel: string;
  formControls: Dictionary<FormControl>;
  regexPattern =
    /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/gi;
  constructor(private readonly changeDetectorRef: ChangeDetectorRef, private datePipe: DatePipe) {
    this.formControls = {};
    this.fieldClass = "curField";
    this.groupLabel = "";
  }

  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }

  ngAfterContentInit(): void {
    this.valueChanged();
  }

  compareDropDown(o1: any, o2: any) {
    if (o1 == o2)
      return true;
    else return false
  }

  checkBlankAndSet() {
    for (const formField of this.fields) {
      if (typeof this._controlValues[formField.fieldUniqeName] == 'undefined') {
        this._controlValues[formField.fieldUniqeName] = "";
      }
    }
  }

  valueChanged() {
    this.formGroup = new FormGroup({});
    for (const formField of this.fields) {
      let tmpVal = (formField.field_type == FieldType.date) ? (typeof this._controlValues[formField.fieldUniqeName] == 'undefined') ? new Date() : new Date(this._controlValues[formField.fieldUniqeName]) : (typeof this._controlValues[formField.fieldUniqeName] == 'undefined') ? formField.defaultVal : this._controlValues[formField.fieldUniqeName];
      let tFormControl = new FormControl(
        tmpVal, formField.validator);
      if (typeof this._controlValues[formField.fieldUniqeName] == 'undefined')
        this._controlValues[formField.fieldUniqeName] = formField.defaultVal;
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
  }

  getReadOnlyVal(field: Fields, val: string) {
    let sVal = val;
    if (field.field_type == FieldType.dropdown) {
      if (typeof field.options != 'undefined') {
        for (var indx in field.options) {
          if (field.options[indx].optionIndex == val) {
            sVal = field.options[indx].optionDisplayName;
            break;
          }
        }
      }
    }
    else if (field.field_type == FieldType.date) {
      if (typeof val != 'undefined' && val != null) {
        if (typeof val == 'object')
          sVal = val["_i"];
        else {
          if (val != "")
            sVal = new Date(val).toLocaleDateString();
        }
      }
    }
    return sVal;
  }

  getFormatedValue(field: Fields, val: string) {
    let retVal = val;
    switch (field.field_type) {
      case FieldType.date:
        if (typeof val != 'undefined' && val != null) {
          if (typeof val == 'object')
            retVal = val["_i"];
          else
            retVal = val.split('T')[0];
        }
        break;
    }
    return retVal;
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
    Object.keys(this.formGroup.controls).forEach(key => {
      if (typeof this.formGroup.get(key)?.value == 'object' && this.formGroup.get(key)?.value != null)
        cVal[key] = new Date(this.formGroup.get(key)?.value).toLocaleDateString();
      else
        cVal[key] = this.formGroup.get(key)?.value;
    });
    return cVal;
  }

  onSubmitClick(): void {
    // if (this.formGroup.valid) {
    this.onSubmit.emit({ value: this.getFieldControlValues() });
    // }
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
}

export function dateRegexValidator(
  control: AbstractControl
): { [key: string]: boolean } | null {
  if (control.value.toString() == 'Invalid Date')
    return null;
  const regexPattern = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/gi;
  let val = control.value;
  const isValid = regexPattern.test(val); 
  if (isValid) {
    return { dateRegex: true };
  }
  return null;
}