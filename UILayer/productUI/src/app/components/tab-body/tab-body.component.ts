import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatRow } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { AccessService } from 'src/app/services/access.service';

@Component({
  selector: 'app-tab-body',
  templateUrl: './tab-body.component.html',
  styleUrls: ['./tab-body.component.css']
})
export class TabBodyComponent {
  @Input()
  set request(val: any) {
    this._curTab = val.curTab;
    this._needNew = val.needNew;

    this.NewInstanceName = "New " + TabType[this._curTab.tab_type];
    if (this._needNew == true)
      this.secondPart = "newEdit";
    this.curBrand = val.curBrand;
    if (typeof val.searchFields != 'undefined')
      this.searchFields = val.searchFields;
  }
  @Output() openStore = new EventEmitter<any>();
  _curTab: HomeTab;
  get curTab(): HomeTab {
    return this._curTab;
  }
  _needNew: boolean;
  get NeedNew(): boolean {
    return this._needNew;
  }
  curBrand: BrandModel;
  secondPart: string;
  curControlVals: Dictionary<string>;
  searchControlVals: Dictionary<string>;
  SubmitLabel: string;
  searchFields: any | null;
  NewInstanceName: string;
  isNew: boolean;
  refreshMe: Date;
  constructor(private route: ActivatedRoute, public router: Router, public access: AccessService) {
    this.searchControlVals = {};
    this.curControlVals = {};
    this.secondPart = "table";
    this.SubmitLabel = "Search";
    this.isNew = true;
  }

  OpenNew() {
    this.curControlVals = {};
    this.secondPart = "newEdit";
    this.isNew = true;
  }

  isRollout() {
    return this.curTab.tab_type == TabType.RolloutProjects;
  }

  openEvent(ev: any){
    this.openStore.emit(ev);
  }

  showNewEdit() {
    return (!this.isRollout() && this.secondPart == 'newEdit');
  }

  showRolloutNewEdit() {
    return (this.isRollout() && this.secondPart == 'newEdit');
  }

  onSubmit(controlVals: FormGroup) {
    let hasBlank = true;
    for (var indx in controlVals.value) {
      if (controlVals.value[indx] != "")
        hasBlank = false;
    }
    this.searchFields = (hasBlank) ? null : controlVals.value;
    this.refreshMe = new Date();
  }

  rowClicked(row: any) {
    for (var i in row) {
      this.curControlVals[i] = row[i];
    }
    this.secondPart = "newEdit";
    this.isNew = false;
  }

  returnBack(resp: any) {
    if (typeof resp == 'undefined' || resp == null || typeof resp.closed == 'undefined')
      alert("Saved Successfully!");
    this.secondPart = "table";
  }
}
