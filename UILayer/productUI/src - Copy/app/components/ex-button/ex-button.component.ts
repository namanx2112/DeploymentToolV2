import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-ex-button',
  templateUrl: './ex-button.component.html',
  styleUrls: ['./ex-button.component.css']
})
export class ExButtonComponent {

  @Input()
  label: string;
  @Input()
  disabled: boolean;
  @Output()
  Clicked = new EventEmitter<any>()
  @Input()
  submitted: boolean;
  constructor() { }

  buttonClicked(e: any) {
    this.submitted = true;
    this.Clicked.emit(e);
  }
}
