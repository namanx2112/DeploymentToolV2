import { Component, Inject, Input } from '@angular/core';
import { Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { ProjectInfo, ProjectTypes } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-change-golive-date',
  templateUrl: './change-golive-date.component.html',
  styleUrls: ['./change-golive-date.component.css']
})
export class ChangeGoliveDateComponent {
  _curProjInfo: ProjectInfo;
  dtField: Fields[] = [];
  onSave: any;
  onClose: any;
  _curBrandId: number;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private storeService: StoreService) {
    if (typeof data != 'undefined') {
      this._curProjInfo = data.projInfo;
      this.onSave = data.onSave;
      this.onClose = data.onClose;
      this._curBrandId = data.curBrand;
      this.dtField = [];
      this.dtField.push({
        field_name: "Go live date",
        fieldUniqeName: "dGoLiveDate",
        field_type: FieldType.date,
        readOnly: false,
        field_placeholder: "Enter go live date",
        invalid: false,
        validator: [Validators.required],
        mandatory: false,
        defaultVal: "",
        hidden: false
      });
    }
  }

  projectTypeString(pType: number) {
    let pString = "";
    pString = ProjectTypes[pType];
    return pString;
  }

  Save(val: any) {
    if (val.value["dGoLiveDate"]) {
      this._curProjInfo.dGoLiveDate = new Date(CommonService.getFormatedDateString(val.value["dGoLiveDate"]));
      this.storeService.UpdateGoliveDate(this._curProjInfo).subscribe(x => {
        this.onSave(this._curProjInfo.dGoLiveDate);
      });
    }
  }

  Close(val: any) {
    this.onClose(val);
  }
}
