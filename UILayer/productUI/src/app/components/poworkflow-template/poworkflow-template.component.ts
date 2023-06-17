import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-poworkflow-template',
  templateUrl: './poworkflow-template.component.html',
  styleUrls: ['./poworkflow-template.component.css']
})
export class POWorkflowTemplateComponent {
  @Output()
  moveView = new EventEmitter<number>();
  _needNew: boolean;
  get NeedNew(): boolean {
    return this._needNew;
  }
  @Input() set NeedNew(val: boolean) {
    this._needNew = val;   
  }
}
