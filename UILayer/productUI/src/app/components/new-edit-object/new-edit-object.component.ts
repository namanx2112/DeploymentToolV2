import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HomeTab } from 'src/app/interfaces/home-tab';
import { Dictionary } from 'src/app/interfaces/commons';
import { FormGroup } from '@angular/forms';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { BrandModel } from 'src/app/interfaces/models';

@Component({
  selector: 'app-new-edit-object',
  templateUrl: './new-edit-object.component.html',
  styleUrls: ['./new-edit-object.component.css']
})
export class NewEditObjectComponent {
  @Input() curTab: HomeTab;
  @Output() returnBack = new EventEmitter<any>()
  private _controlValues: Dictionary<string>;
  @Input() set controlValues(value: Dictionary<string>) {
    this._controlValues = value;
    this.valueChanged();
  };
  get controlValues(): Dictionary<string> {
    return this._controlValues;
  }
  SubmitLabel: string;
  constructor(private brandService: BrandServiceService) {
    this.SubmitLabel = "Submit";
    this.controlValues = {};
  }

  valueChanged() {

  }

  onSubmit(controlVals: FormGroup) {
    if (controlVals.value["aBrandId"] && parseInt(controlVals.value["aBrandId"]) > 0) {
      this.brandService.UpdateBrand(controlVals.value).subscribe((resp: BrandModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
    else {
      this.brandService.CreateBrand(controlVals.value).subscribe((resp: BrandModel) => {
        console.log(resp);
        this.returnBack.emit(resp);
      });
    }
  }
}
