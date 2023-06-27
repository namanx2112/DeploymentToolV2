import { Component, Inject, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { QuillEditorComponent } from 'ngx-quill';
import Quill from 'quill';
import { MergedQuoteRequest, StoreSearchModel } from 'src/app/interfaces/sonic';
import { QuoteRequestWorkflowConfigService } from 'src/app/services/quote-request-workflow-config.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-render-quote-request',
  templateUrl: './render-quote-request.component.html',
  styleUrls: ['./render-quote-request.component.css']
})
export class RenderQuoteRequestComponent {
  @ViewChild('editor') editor: QuillEditorComponent | undefined;
  content = '<p>Rich Text Editor Example </p>';
  format = 'html';
  form: any;
  quillConfig = {
    toolbar: {
      container: [
        ['bold', 'italic', 'underline', 'strike'],
        [{ 'list': 'ordered'}, { 'list': 'bullet' }],
        [{ 'size': ['small', false, 'large', 'huge'] }],
        [{ 'color': [] }, { 'background': [] }],
        [{ header: 1 }, { header: 2 }],
        [{ list: 'ordered' }, { list: 'bullet' }],
        ['clean'],
        [{ 'align': [] }],
        ['link']
      ],
      handlers: {
        source: () => {
          this.formatChange();
        },
      },
    },
  };
  curStore: StoreSearchModel;
  onSubmit: any;
  tRequest: MergedQuoteRequest;
  constructor(public dialogRef: MatDialogRef<RenderQuoteRequestComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder, private service: StoreService,
  private quoteService: QuoteRequestWorkflowConfigService) {
    this.tRequest = {
      tContent: "",
      tTo: "",
      tCC: "",
      tSubject: ""
    };
    this.curStore = data.curStore;
    this.onSubmit = data.onSubmit;
    this.getMergedContent();
  }

  getMergedContent() {
    this.quoteService.GetMergedQuoteRequest(this.curStore.nProjectId).subscribe((x: MergedQuoteRequest) => {
      this.tRequest = x;
      this.content = x.tContent;
      this.updateEditorContent();
    })
  }

  updateEditorContent() {
    this.form = this.fb.group({
      editor: this.content,
    });
  }

  onCancelUserDialog(): void {
    this.dialogRef.close();
  }

  SendRequest() {
    //this.tRequest.tContent = this.form.controls['editor'].value;
    this.quoteService.SendQuoteRequest(this.tRequest).subscribe(x => {
      this.onSubmit();
    })
  }

  ngOnInit() {
    this.updateEditorContent();
    let icons = Quill.import('ui/icons');
    icons['source'] = '[source]';
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

  formatChange() {
    this.format = this.format === 'html' ? 'text' : 'html';

    if (this.format === 'text' && this.editor) {
      const htmlText = this.form.get('editor').value;
      this.editor.quillEditor.setText(htmlText);
    } else if (this.format === 'html' && this.editor) {
      const htmlText = this.form.get('editor').value;
      this.editor.quillEditor.setText('');
      // this.editor.quillEditor.pasteHTML(0, htmlText);
    }
  }
  onFocus = () => {
    console.log('On Focus');
  };
  onBlur = () => {
    console.log('Blurred');
  };
}
