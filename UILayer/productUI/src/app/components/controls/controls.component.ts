import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ControlsErrorMessages, Dictionary } from 'src/app/interfaces/commons';
import { Fields, FieldType, HomeTab } from 'src/app/interfaces/home-tab';

@Component({
  selector: 'app-controls',
  templateUrl: './controls.component.html',
  styleUrls: ['./controls.component.css']
})
export class ControlsComponent {
  private _controlValues: Dictionary<string>;
  @Input() fields: Fields[];
  @Input() set controlValues(value: Dictionary<string>) {
    this._controlValues = value;
    this.valueChanged();
  };
  get controlValues(): Dictionary<string> {
    return this._controlValues;
  }
  @Input() SubmitLabel: string;
  @Output() onSubmit = new EventEmitter<FormGroup>();
  formGroup = new FormGroup({});
  constructor() {
  }

  ngOnChanges(): void {
    this.valueChanged();
  }

  valueChanged() {
    this.formGroup = new FormGroup({});
    for (const formField of this.fields) {
      if (typeof this._controlValues[formField.fieldUniqeName] == 'undefined')
        this._controlValues[formField.fieldUniqeName] = formField.defaultVal;
      this.formGroup.addControl(formField.fieldUniqeName, new FormControl(
        "", formField.validator));
    }
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
    let eMsg = "Error";
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

  onSubmitClick(): void {
    // if (this.formGroup.valid) {
    this.onSubmit.emit(this.formGroup);
    // }
  }
}
