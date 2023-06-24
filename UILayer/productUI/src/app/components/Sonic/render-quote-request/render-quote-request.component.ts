import { Component, Inject, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { QuillEditorComponent } from 'ngx-quill';
import Quill from 'quill';

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
        ['code-block'],
        [{ header: 1 }, { header: 2 }],
        [{ list: 'ordered' }, { list: 'bullet' }],
        ['clean'],
        ['link'],
        ['source'],
      ],
      handlers: {
        source: () => {
          this.formatChange();
        },
      },
    },
  };
  constructor(public dialogRef: MatDialogRef<RenderQuoteRequestComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder) {

  }

  onCancelUserDialog(): void {
    this.dialogRef.close();
  }

  SendRequest(){

  }

  ngOnInit() {
    this.form = this.fb.group({
      editor: this.content,
    });
    let icons = Quill.import('ui/icons');
    icons['source'] = '[source]';
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
