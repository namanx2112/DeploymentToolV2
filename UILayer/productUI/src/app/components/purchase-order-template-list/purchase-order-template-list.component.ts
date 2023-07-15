import { Component, EventEmitter, Output, Input } from '@angular/core';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { POConfigTemplateTemp } from 'src/app/interfaces/models';
import { POWorkflowConfigService } from 'src/app/services/poworklow-config.service';

@Component({
  selector: 'app-purchase-order-template-list',
  templateUrl: './purchase-order-template-list.component.html',
  styleUrls: ['./purchase-order-template-list.component.css']
})
export class PurchaseOrderTemplateListComponent {
  searchText: string;
  @Output()
  moveView = new EventEmitter<number>();
  @Input()
  set NeedNew(val: boolean) {
    if (val == true)
      this.OpenView(0);
  }
  searchField: Fields[];
  allPO: POConfigTemplateTemp[];
  nBrandId: number;
  nTemplateId: number;
  constructor(private service: POWorkflowConfigService) {
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
    this.service.GetAllTemplate(this.nBrandId).subscribe((x => {
      this.allPO = x;
    }));
  }

  deleteMe(templateId: any) {
    if (confirm("Are you sure you want to delete this Item?")) {
      this.service.Delete(templateId).subscribe((x: any) => {
        this.getQuoteRequests();
      });
    }
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  OpenView(templateId: any) {
    this.nTemplateId = templateId;
    //this.moveView.emit(6);
  }

  moveBack(inst: number) {
    if (inst > 0) {
      this.getQuoteRequests();
    }
    this.nTemplateId = -1;
  }

  getFormatedDate(dt: Date) {
    return (dt) ? new Date(dt).toLocaleDateString() : "";
  }
}
