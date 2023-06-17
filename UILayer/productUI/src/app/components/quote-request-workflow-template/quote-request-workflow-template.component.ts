import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Dictionary, checkboxItems } from 'src/app/interfaces/commons';
import { HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { QuoteRequestTemplate } from 'src/app/interfaces/models';
import { QuoteRequestWorkflowConfigService } from 'src/app/services/quote-request-workflow-config.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-quote-request-workflow-template',
  templateUrl: './quote-request-workflow-template.component.html',
  styleUrls: ['./quote-request-workflow-template.component.css']
})

export class QuoteRequestWorkflowTemplateComponent {
  
  @Input() nTemplateId: number;
  curTemplate: QuoteRequestTemplate;
  techCompTabs: HomeTab[];
  selectedTabs: HomeTab[];
  curTabFields: checkboxItems[];
  techCompControl = new FormControl('');
  constructor(private sonicService: SonicService, private quoteService: QuoteRequestWorkflowConfigService) {
    this.techCompTabs = [this.sonicService.GetStoreConfigurationTab(TabInstanceType.Single),
    this.sonicService.GetStoreAudioTab(TabInstanceType.Single), this.sonicService.GetStoreNetworingTab(TabInstanceType.Single), this.sonicService.GetStorePOSTab(TabInstanceType.Single),
    this.sonicService.GetStoreExteriorMenusTab(TabInstanceType.Single), this.sonicService.GetStoreInteriorMenusTab(TabInstanceType.Single),
    this.sonicService.GetStorePaymentSystemTab(TabInstanceType.Single)];
    this.getTemplate();
    this.curTabFields = [];
  }

  getTemplate() {
    let userId = 1;
    this.quoteService.GetTemplate(this.nTemplateId).subscribe((x: QuoteRequestTemplate)=>{
      this.curTemplate = x;
    });    
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
