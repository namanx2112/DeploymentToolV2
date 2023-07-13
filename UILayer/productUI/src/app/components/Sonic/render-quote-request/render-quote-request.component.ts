import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MergedQuoteRequest, ProjectTemplates } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
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
    this.quoteService.GetMergedQuoteRequest(this.curStore.nProjectId, this.curTemplate.nTemplateId).subscribe((x: MergedQuoteRequest) => {
      this.tRequest = x;
    })
  }
  onCancelUserDialog(): void {
    this.dialogRef.close();
  }

  SendRequest() {
    this.quoteService.SendQuoteRequest(this.tRequest).subscribe(x => {
      this.onSubmit();
    })
  }

  ngOnInit() {
  }

  isValid(email: string) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  cannotSend() {
    let cannot = false;
    if (this.tRequest.tContent == '' || this.tRequest.tTo == '' || this.tRequest.tSubject == '')
      cannot = true;
    if (!this.isValid(this.tRequest.tTo))
      cannot = true;
    if (this.tRequest.tCC != null && this.tRequest.tCC != '' && !this.isValid(this.tRequest.tCC))
      cannot = true;
    return cannot;
  }
}
