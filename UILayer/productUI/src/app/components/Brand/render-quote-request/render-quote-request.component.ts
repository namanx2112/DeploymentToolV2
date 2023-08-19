import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MergedQuoteRequest, ProjectTemplates } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { QuoteRequestWorkflowConfigService } from 'src/app/services/quote-request-workflow-config.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-render-quote-request',
  templateUrl: './render-quote-request.component.html',
  styleUrls: ['./render-quote-request.component.css']
})
export class RenderQuoteRequestComponent {
  curStore: StoreSearchModel;
  onSubmit: any;
  tRequest: MergedQuoteRequest;
  curTemplate: ProjectTemplates;
  ckConfig: any;
  constructor(public dialogRef: MatDialogRef<RenderQuoteRequestComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private service: StoreService,
    private quoteService: QuoteRequestWorkflowConfigService, private commonService: CommonService) {
    this.ckConfig = this.commonService.GetCKEditorConfig("256px");
    this.tRequest = {
      tContent: "",
      tTo: "",
      tCC: "",
      tSubject: ""
    };
    this.curStore = data.curStore;
    this.onSubmit = data.onSubmit;
    this.curTemplate = data.curTemplate;
    this.getMergedContent();
  }

  getMergedContent() {
    this.quoteService.GetMergedQuoteRequest(this.curStore.nStoreId, this.curTemplate.nTemplateId).subscribe((x: MergedQuoteRequest) => {
      this.tRequest = x;
    })
  }
  onCancelUserDialog(): void {
    this.dialogRef.close();
  }

  SendRequest() {
    this.quoteService.SendQuoteRequest(this.tRequest).subscribe(x => {
      alert("Quote Request Sent!");
      this.onSubmit();
    })
  }

  ngOnInit() {
  }

  cannotSend() {
    let cannot = false;
    if (this.tRequest.tContent == '' || this.tRequest.tTo == '' || this.tRequest.tSubject == '')
      cannot = true;
    if (!cannot) {
      let eIds = this.tRequest.tTo.split(";");
      for (var indx in eIds) {
        if (!CommonService.isValidEmail(eIds[indx].trim())) {
          cannot = true;
          break;
        }
      }
      if (!cannot && this.tRequest.tCC != null && this.tRequest.tCC != '') {
        let eIds = this.tRequest.tCC.split(";");
        for (var indx in eIds) {
          if (!CommonService.isValidEmail(eIds[indx].trim())) {
            cannot = true;
            break;
          }
        }
      }
    }
    return cannot;
  }
}
