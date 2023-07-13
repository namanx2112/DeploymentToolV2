import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Dictionary, checkboxItems } from 'src/app/interfaces/commons';
import { Fields, HomeTab, TabInstanceType } from 'src/app/interfaces/home-tab';
import { QuoteRequestTechAreas, QuoteRequestTemplate } from 'src/app/interfaces/models';
import { QuoteRequestWorkflowConfigService } from 'src/app/services/quote-request-workflow-config.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-quote-request-workflow-template',
  templateUrl: './quote-request-workflow-template.component.html',
  styleUrls: ['./quote-request-workflow-template.component.css']
})

export class QuoteRequestWorkflowTemplateComponent {
  _nTemplateId: number;
  @Input() set nTemplateId(val: number) {
    this._nTemplateId = val;
    this.getTemplate();
  }
  @Output()
  moveBack = new EventEmitter<number>();
  nBrandId: number = 1;
  curTemplate: QuoteRequestTemplate;
  techCompTabs: HomeTab[];
  selectedTabs: HomeTab[];
  curTabFields: checkboxItems[];
  techCompControl = new FormControl('');
  constructor(private sonicService: SonicService, private quoteService: QuoteRequestWorkflowConfigService) {
    this.techCompTabs = [this.sonicService.GetStoreContactTab(TabInstanceType.Single), this.sonicService.GetStoreConfigurationTab(TabInstanceType.Single),
    this.sonicService.GetStoreAudioTab(TabInstanceType.Single), this.sonicService.GetStoreNetworingTab(TabInstanceType.Single), this.sonicService.GetStorePOSTab(TabInstanceType.Single),
    this.sonicService.GetStoreExteriorMenusTab(TabInstanceType.Single), this.sonicService.GetStoreInteriorMenusTab(TabInstanceType.Single),
    this.sonicService.GetStorePaymentSystemTab(TabInstanceType.Single)];
    this.techCompTabs[0].tab_name = "Store Information";
    this.curTabFields = [];
  }

  getTemplate() {
    let userId = 1;
    if (this._nTemplateId > 0) {
      this.quoteService.GetTemplate(this._nTemplateId).subscribe((x: QuoteRequestTemplate) => {
        this.curTemplate = x;
        this.updateSelectedTabs();
      });
    }
    else {
      this.curTemplate = {
        aQuoteRequestTemplateId: 0,
        tTemplateName: "",
        nBrandId: this.nBrandId,
        quoteRequestTechComps: []
      }
      this.updateSelectedTabs();
    }
  }

  updateSelectedTabs() {
    this.selectedTabs = [];
    for (var indx in this.curTemplate.quoteRequestTechComps) {
      let tTab = this.curTemplate.quoteRequestTechComps[indx];
      let sTab = this.techCompTabs.filter(x => x.tab_name == tTab.tTechCompName);
      if (sTab.length > 0)
        this.selectedTabs.push(sTab[0]);
    }
  }

  updateSelectedComps() {
    let deletedTabs: QuoteRequestTechAreas[] = [];
    for (var tIndx in this.selectedTabs) {
      let tmpTab = this.selectedTabs[tIndx];
      if (this.curTemplate.quoteRequestTechComps.findIndex(x => x.tTechCompName == tmpTab.tab_name) == -1) {
        this.curTemplate.quoteRequestTechComps.push({
          nQuoteRequestTemplateId: this._nTemplateId,
          tTechCompName: tmpTab.tab_name,
          tTableName: tmpTab.tTableName,
          fields: [],
          part: tmpTab
        });
      }
    }
    if (this.selectedTabs.length < this.curTemplate.quoteRequestTechComps.length) {
      for (var indx in this.curTemplate.quoteRequestTechComps) {
        let tmpTab = this.curTemplate.quoteRequestTechComps[indx];
        if (this.selectedTabs.findIndex(x => x.tab_name) == -1) {
          deletedTabs.push(tmpTab);
        }
      }
      if (deletedTabs.length > 0) {
        for (var indx in deletedTabs) {
          let tIndx = this.curTemplate.quoteRequestTechComps.findIndex(x => x.tTechCompName == deletedTabs[indx].tTechCompName);
          this.curTemplate.quoteRequestTechComps.splice(tIndx, 1);
        }
      }
    }
  }

  isChecked(fieldName: string, tTab: HomeTab) {
    let found = false;
    let sTab = this.curTemplate.quoteRequestTechComps.filter(x => x.tTechCompName == tTab.tab_name);
    if (sTab.length > 0) {
      let field = sTab[0].fields.filter(x => x.tTechCompField == fieldName);
      if (field.length > 0)
        found = true;
    }
    return found;
  }

  onTechCompChange(ev: any) {
    this.selectedTabs = ev;
    this.updateSelectedComps();
  }

  goBack() {
    this.moveBack.emit(0);
  }

  checkboxFieldChange(item: any, tTab: HomeTab, field: Fields) {
    let tmpTab = this.curTemplate.quoteRequestTechComps.filter(x => x.tTechCompName == tTab.tab_name);
    if (tmpTab.length > 0) {
      if (item.checked) {
        tmpTab[0].fields.push({
          nQuoteRequestTemplateId: this._nTemplateId,
          tTechCompField: field.fieldUniqeName,
          tTechCompFieldName: field.field_name
        });
      }
      else {
        let indx = tmpTab[0].fields.findIndex(x => x.tTechCompField = field.fieldUniqeName);
        tmpTab[0].fields.splice(indx, 1);
      }
    }
  }

  getAllowedFields(fields: Fields[]) {
    return fields.filter(x => x.hidden == false);
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  canSave() {
    let can = true;
    if (this.curTemplate.quoteRequestTechComps.length > 0 && this.curTemplate.tTemplateName.length > 0) {
      can = false;
    }
    return can;
  }

  submit() {
    this.quoteService.CreateUpdateTemplate(this.curTemplate).subscribe((x: QuoteRequestTemplate) => {
      alert("Saved succesfully");
      this.moveBack.emit(1);
    });
  }
}
