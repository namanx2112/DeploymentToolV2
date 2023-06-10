import { Component } from '@angular/core';
import { OptionType } from 'src/app/interfaces/home-tab';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-sonic-home-page',
  templateUrl: './sonic-home-page.component.html',
  styleUrls: ['./sonic-home-page.component.css']
})
export class SonicHomePageComponent {
  showMode: string;
  curStore: StoreSearchModel;
  ProjectTypes: OptionType[];
  techCompType: string;
  projectType: OptionType;
  constructor(private commonService: CommonService) {
    this.showMode = "dashboard";
    this.techCompType = "all";
    this.GetAllProjectTypes();
  }
  GetAllProjectTypes() {
    this.ProjectTypes = this.commonService.GetDropdown("ProjectType");
  }
  clickOption(val: any) { }


  menuClick(tMode: string, techComp?: string, newOption?: string) {
    if (typeof newOption != 'undefined' && typeof techComp != 'undefined') {
      this.techCompType = techComp;
      this.projectType = this.ProjectTypes.filter(x => x.optionDisplayName.toLocaleLowerCase() == newOption.toLocaleLowerCase())[0];
    }
    this.showMode = tMode;
  }

  SearchedResult(val: any) {
    this.curStore = val;
    this.showMode = "storeview";
  }

  ChangeView(view: any) {
    this.showMode = view;
  }
}
