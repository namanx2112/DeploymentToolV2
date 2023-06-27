import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-workflows',
  templateUrl: './workflows.component.html',
  styleUrls: ['./workflows.component.css']
})
export class WorkflowsComponent {
  @Output() ChangeView = new EventEmitter<any>();
  @Input() StoreName: string;
  constructor(){
    
  }

  goBack() {
    this.ChangeView.emit("storeview");
  }
}
