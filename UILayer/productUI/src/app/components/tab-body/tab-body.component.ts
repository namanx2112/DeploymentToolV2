import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatRow } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab } from 'src/app/interfaces/home-tab';

@Component({
  selector: 'app-tab-body',
  templateUrl: './tab-body.component.html',
  styleUrls: ['./tab-body.component.css']
})
export class TabBodyComponent {
  @Input() curTab: HomeTab;
  secondPart: string;
  curControlVals: Dictionary<string>;
  searchControlVals: Dictionary<string>;
  SubmitLabel: string;
  searchFields: any | null;

  constructor(private route: ActivatedRoute, public router: Router) {
    this.searchControlVals = {};
    this.curControlVals = {};
    this.secondPart = "table";
    this.SubmitLabel = "Search";
  }

  OpenNew() {
    this.secondPart = "newEdit";
  }

  onSubmit(controlVals: FormGroup) {
    this.searchFields = controlVals.value;
  }

  rowClicked(row: any) {
    for (var i in row) {
      this.curControlVals[i] = row[i];
    }
    this.secondPart = "newEdit";
  }

  returnBack(resp: any) {
    alert("Saved");
    this.secondPart = "table";
  }
}
