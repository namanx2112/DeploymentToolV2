import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Dictionary, checkboxItems } from 'src/app/interfaces/commons';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { QuoteRequestTemplate } from 'src/app/interfaces/models';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-quote-request-workflow-template',
  templateUrl: './quote-request-workflow-template.component.html',
  styleUrls: ['./quote-request-workflow-template.component.css']
})

export class QuoteRequestWorkflowTemplateComponent {
  _needNew: boolean;
  get NeedNew(): boolean {
    return this._needNew;
  }
  @Input() set NeedNew(val: boolean) {
    this._needNew = val;
  }
  quoteRequestTemplate: QuoteRequestTemplate;
  techCompTabs: HomeTab[];
  selectedTabs: HomeTab[];
  curTabFields: checkboxItems[];
  techCompControl = new FormControl('');
  constructor(private sonicService: SonicService) {
    this.techCompTabs = [this.sonicService.GetStoreConfigurationTab(TabInstanceType.Single),
    this.sonicService.GetStoreAudioTab(TabInstanceType.Single), this.sonicService.GetStoreNetworingTab(TabInstanceType.Single), this.sonicService.GetStorePOSTab(TabInstanceType.Single),
    this.sonicService.GetStoreExteriorMenusTab(TabInstanceType.Single), this.sonicService.GetStoreInteriorMenusTab(TabInstanceType.Single),
    this.sonicService.GetStorePaymentSystemTab(TabInstanceType.Single)];
    this.getNew();
    this.curTabFields = [];
  }

  getNew() {
    let userId = 1;
    this.quoteRequestTemplate = {
      aQuoteTemplateID: -1,
      tName: '',
      nBrandID: 0,
      arTechAreas: [],
      nCreatedBy: userId,
      nUpdateBy: userId,
      dtCreatedOn: new Date(),
      dtUpdatedOn: new Date(),
      bDeleted: false
    }
  }

  onTechCompChange(ev: any) {
    this.selectedTabs = ev;
    // this.curTabFields = [];
    // for (var indx in ev) {
    //   let cTab = ev[indx];
    //   for (var indx in cTab.fields) {
    //     this.curTabFields.push({
    //       name: cTab.fields[indx].fieldUniqeName,
    //       checked: false,
    //       displayName: cTab.fields[indx].field_name
    //     });
    //   }
    // }
  }

  goBack() {

  }

  checkboxFieldChange(item: any) {

  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }
}
