import { Component, EventEmitter, Output } from '@angular/core';
import { Fields } from 'src/app/interfaces/home-tab';
import { QuoteRequestTemplate } from 'src/app/interfaces/models';

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
  constructor() {
    this.searchText = "";
    this.getQuoteRequests();
  }

  getQuoteRequests(){
    let userId = 1;
    this.allRequests = [{
      aQuoteTemplateID: 1,
      tName: 'Audio',
      nBrandID: 0,
      arTechAreas: [],
      nCreatedBy: userId,
      nUpdateBy: userId,
      dtCreatedOn: new Date(),
      dtUpdatedOn: new Date(),
      bDeleted: false
    },
    {
      aQuoteTemplateID: 2,
      tName: 'Networking',
      nBrandID: 0,
      arTechAreas: [],
      nCreatedBy: userId,
      nUpdateBy: userId,
      dtCreatedOn: new Date(),
      dtUpdatedOn: new Date(),
      bDeleted: false
    }];
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  OpenNew() {
    this.moveView.emit(6);
  }

}
