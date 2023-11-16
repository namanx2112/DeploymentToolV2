import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { Dictionary } from 'src/app/interfaces/commons';
import { DatePipe } from '@angular/common';
import * as _moment from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { FieldType, Fields, HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { BrandModel, Brands } from 'src/app/interfaces/models';
import { RolloutProjects } from 'src/app/interfaces/projects';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';
import { ProjectTypes } from 'src/app/interfaces/store';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ProjectRolloutImportComponent } from '../project-rollout-import/project-rollout-import.component';
import { RolloutProjectsService } from 'src/app/services/rollout-projects.service';

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
  selector: 'app-project-rollout-editor',
  templateUrl: './project-rollout-editor.component.html',
  styleUrls: ['./project-rollout-editor.component.css'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE],
    },
    DatePipe,
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class ProjectRolloutEditorComponent {
  @Input()
  set request(val: any) {
    this.curBrand = val.curBrand;
    this.controlValues = (typeof val.rowValue == 'undefined') ? this.getNewRollout() : val.rowValue;
    this.initMe();
  }
  @Output() returnBack = new EventEmitter<any>();
  @Output() openStore = new EventEmitter<any>();
  curBrand: BrandModel;
  tTab: HomeTab;
  formGroup = new FormGroup({});
  controlValues: any;
  formControls: Dictionary<FormControl>;
  myFields: Dictionary<Fields>;
  showPage: number = 0;
  uploadingRows: any[] = [];
  constructor(private dialog: MatDialog,
    private homeService: HomeService, private datePipe: DatePipe
    , private dateAdapter: DateAdapter<Date>, public commonService: CommonService,
    private rolloutService: RolloutProjectsService) {
    this.dateAdapter.setLocale('en-GB'); //dd/MM/yyyy
  }

  getNewRollout() {
    return {
      aProjectsRolloutID: 0,
      tProjectsRolloutName: "",
      tProjectsRolloutDescription: "",
      cHardwareCost: 0,
      cDeploymentCost: 0,
      nNumberOfStore: 0,
      nBrandID: this.curBrand.aBrandId,
      nStatus: 0,
      tEstimateInstallTImePerStore: 0,
      dtStartDate: null,
      dtEndDate: null
    };
  }

  initMe() {
    this.formControls = {};
    this.myFields = {};
    this.initSupportedProject();
    this.tTab = this.homeService.GetProjectRolloutTab(TabInstanceType.Single);
    this.loadFields();
  }

  initSupportedProject() {
    if (this.curBrand.nBrandType == Brands.Buffalo) {
      this.uploadingRows.push({ name: "Server Handheld", type: ProjectTypes.ServerHandheldInstallation, items: [], fileName: "" });
    }
    else if (this.curBrand.nBrandType == Brands.Dunkin) {
      this.uploadingRows.push({ name: "Order Accuracy", type: ProjectTypes.OrderAccuracyInstallation, items: [], fileName: "" });
      this.uploadingRows.push({ name: "Order Status Board", type: ProjectTypes.OrderStatusBoardInstallation, items: [], fileName: "" });
    }
    else if (this.curBrand.nBrandType == Brands.Arby) {
      this.uploadingRows.push({ name: "Arby's HP Rollout", type: ProjectTypes.ArbysHPRolloutInstallation, items: [], fileName: "" });
    }
  }

  showStore(ev: any){
    this.openStore.emit(ev);
  }

  cantMoveNext(page: number) {
    let can = false;
    if (page == 1) {
      if (typeof this.controlValues.tProjectsRolloutName != 'undefined' && this.controlValues.tProjectsRolloutName != null && this.controlValues.tProjectsRolloutName != "")
        can = true;
    }
    else if (page == 2) {
      if (this.controlValues["aProjectsRolloutID"] > 0)
        can = true;
      else {
        for (var indx in this.uploadingRows) {
          if (this.uploadingRows[indx].items.length > 0)
            can = true;
        }
      }
    }
    return can;
  }

  compareDropDown(o1: any, o2: any) {
    if (o1 == o2)
      return true;
    else return false
  }

  loadFields() {
    for (var indx in this.tTab.fields) {
      let tField = this.tTab.fields[indx];
      this.myFields[tField.fieldUniqeName] = tField;
      if (typeof this.controlValues[tField.fieldUniqeName] == 'undefined')
        this.controlValues[tField.fieldUniqeName] = null;
      if (tField.field_type == FieldType.dropdown || tField.field_type == FieldType.multidropdown) {
        if (typeof tField.dropDownOptions == 'undefined') {
          tField.dropDownOptions = this.commonService.GetDropdownOptions(this.curBrand.aBrandId, tField.options);
        }
      }
    }
    this.myFields['nStatus'].dropDownOptions = CommonService.getRolloutProjectStatus();
    this.valueChanged();
  }

  valueChanged() {
    this.formGroup = new FormGroup({});
    for (const formField of this.tTab.fields) {
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
    this.showPage = 1;
  }

  movePage(tIndex: number) {
    this.showPage += tIndex;
  }

  deleteUploadItem(projType: ProjectTypes, index: number) {
    this.uploadingRows[index].items = [];
    this.uploadingRows[index].fileName = "";
  }

  uploadItem(projType: ProjectTypes, index: number) {
    let cThis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '80%';
    dialogConfig.height = '90%';
    dialogConfig.data = {
      title: "Import " + CommonService.getProjectName(projType) + " Projects",
      projectType: projType,
      curBrand: this.curBrand,
      onSubmit: function (pType: any, data: any, fileName: string) {
        cThis.uploadingRows[index].items = data;
        cThis.uploadingRows[index].fileName = fileName;
        dialogRef.close();
      },
      onClose: function (ev: any) {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(ProjectRolloutImportComponent, dialogConfig);
  }

  canSubmit() {
    let can = false;
    if (this.controlValues["aProjectsRolloutID"] > 0)
      can = true;
    else {
      for (var indx in this.uploadingRows) {
        if (this.uploadingRows[indx].items.length > 0)
          can = true;
      }
    }
    return can;
  }

  submitMe() {
    this.controlValues["dtStartDate"] = this.formControls["dtStartDate"].value;
    this.controlValues["dtEndDate"] = this.formControls["dtEndDate"].value;
    this.controlValues["dtEndDate"] = this.formControls["dtEndDate"].value;
    this.controlValues["tProjectsRolloutName"] = this.formControls["tProjectsRolloutName"].value;
    this.controlValues["tProjectsRolloutDescription"] = this.formControls["tProjectsRolloutDescription"].value;
    this.controlValues["tEstimateInstallTImePerStore"] = this.formControls["tEstimateInstallTImePerStore"].value;
    this.controlValues["uploadingRows"] = this.uploadingRows;
    this.controlValues["nBrandID"] = this.curBrand.aBrandId;
    this.rolloutService.Create(this.controlValues).subscribe(x => {
      alert(x);
      this.returnBack.emit({ closed: true });
    });
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