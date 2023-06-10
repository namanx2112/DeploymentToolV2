import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabInstanceType, TabType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';
import { StoreService } from 'src/app/services/store.service';

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
  constructor(private service: SonicService, private storeSerice: StoreService) {
    this.getTabs();
    this.SubmitLabel = "Next";
    this.tValues = {};
    this.curTab = this.allTabs[0];
  }

  getTabs(){
    this.allTabs = [];
    this.allTabs.push(this.service.GetNewStoresTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreConfigurationTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreStackholderTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreNetworingTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStorePOSTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreAudioTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreExteriorMenusTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStorePaymentSystemTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreInteriorMenusTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreSonicRadioTab(TabInstanceType.Single));
    this.allTabs.push(this.service.GetStoreInsallationTab(TabInstanceType.Single));    
  }

  clickTab(tTab: HomeTab) {
    this.curTab = tTab;
  }

  onSubmit(controlVals: FormGroup, tab: HomeTab) {
    switch(tab.tab_type){
      case TabType.NewStore:
        this.storeSerice.CreateNewStores(controlVals.value).subscribe((x: string)=>{
          
        });
        break;
    }
  }

}
