import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatRow } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { AccessService } from 'src/app/services/access.service';

@Component({
  selector: 'app-tab-body',
  templateUrl: './tab-body.component.html',
  styleUrls: ['./tab-body.component.css']
})
export class TabBodyComponent {
  _curTab: HomeTab;
  get curTab(): HomeTab {
    return this._curTab;
  }
  @Input() set curTab(val: HomeTab) {
    this._curTab = val;
    this.NewInstanceName = "New " + TabType[val.tab_type];
  }
  _needNew: boolean;
  get NeedNew(): boolean {
    return this._needNew;
  }
  @Input() set NeedNew(val: boolean) {
    this._needNew = val;
    if (this._needNew == true)
      this.secondPart = "newEdit";
  }
  secondPart: string;
  curControlVals: Dictionary<string>;
  searchControlVals: Dictionary<string>;
  SubmitLabel: string;
  searchFields: any | null;
  NewInstanceName: string;
  isNew: boolean;
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

  onSubmit(controlVals: FormGroup) {
    this.searchFields = controlVals.value;
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
