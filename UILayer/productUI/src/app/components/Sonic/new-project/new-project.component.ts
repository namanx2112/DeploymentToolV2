import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.css']
})
export class NewProjectComponent {
  allTabs: HomeTab[];
  curTab: HomeTab;
  tValues: Dictionary<string>;
  SubmitLabel: string;
  constructor(private service: SonicService) {
    this.allTabs = this.service.GetStoretabs();
    this.SubmitLabel = "Next";
    this.tValues = {};
    this.curTab = this.allTabs[0];
  }

  clickTab(tTab: HomeTab) {
    this.curTab = tTab;
  }

  onSubmit(controlVals: FormGroup, tab: HomeTab) {
  }

}
