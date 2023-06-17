import { Component, EventEmitter, Output } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { QuoteRequestTemplate } from 'src/app/interfaces/models';
import { QuoteRequestWorkflowConfigService } from 'src/app/services/quote-request-workflow-config.service';

@Component({
  selector: 'app-quote-request-template-list',
  templateUrl: './quote-request-template-list.component.html',
  styleUrls: ['./quote-request-template-list.component.css']
})
export class QuoteRequestTemplateListComponent {
  searchText: string;
  @Output()
  moveView = new EventEmitter<number>();
  allRequests: QuoteRequestTemplate[];
  searchField: Fields[];
  allQuotes: {[key: number]: string};
  nBrandId: number;
  nTemplateId: number;
  constructor(private service: QuoteRequestWorkflowConfigService) {
    this.nTemplateId = -1;
    this.nBrandId = 1;
    this.searchText = "";
    this.searchField = [{
      field_name: "Search",
      fieldUniqeName: "tSearchText",
      icon: "search",
      field_type: FieldType.text,
      readOnly: false,
      field_placeholder: "Type a workflow name",
      invalid: false,
      validator: [],
      mandatory: false,
      defaultVal: "",
      hidden: false
    }];
    this.getQuoteRequests();
  }

  getQuoteRequests() {
    this.service.GetAllTemplate(this.nBrandId).subscribe((x=>{
      this.allQuotes = x;
    }));    
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  OpenView(templateId: any) {
    this.nTemplateId = templateId;
    //this.moveView.emit(6);
  }

}
