import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType, Fields, HomeTab } from 'src/app/interfaces/home-tab';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-audio-installation',
  templateUrl: './audio-installation.component.html',
  styleUrls: ['./audio-installation.component.css']
})
export class AudioInstallationComponent {
  @Output() ChangeView = new EventEmitter<any>();
  activeTabName: string;
  allTabs: HomeTab[];
  searchFields: Fields[];
  searchValue: Dictionary<string>;
  fieldValues: Dictionary<Dictionary<string>>;
  constructor(private service: ExStoreService) {
    this.getAudioInstallationTabs();
    this.activeTabName = "searchstore";
    this.searchValue = {};
    this.searchFields = [{
      field_name: "Store Name",
      fieldUniqeName: "tStoreName",
      field_type: FieldType.text,
      readOnly: false,
      field_placeholder: "Store Name",
      invalid: false,
      validator: [],
      mandatory: true,
      defaultVal: "",
      hidden: false
    }];
  }

  getAudioInstallationTabs() {
    this.allTabs = this.service.GetAudioInstallationTabs();
    this.allTabs[0].tab_name = "Store Details";
    this.fieldValues = {};
    for (var indx in this.allTabs) {
      let tTab = this.allTabs[indx];
      this.fieldValues[tTab.tab_name] = {};
    }
  }

  clickTab(tabName: string) {
    this.activeTabName = tabName;
  }

  onSearch(controlVals: FormGroup) {
  }

  onNext(controlVals: FormGroup, tab: HomeTab) {
  }
  goBack() {
    this.ChangeView.emit("storeview");
  }
}
