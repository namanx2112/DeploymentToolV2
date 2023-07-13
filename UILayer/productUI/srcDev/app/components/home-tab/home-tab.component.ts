import { Component } from '@angular/core';
import { FieldType, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { ValidatorFn, Validators } from "@angular/forms"

@Component({
  selector: 'app-home-tab',
  templateUrl: './home-tab.component.html',
  styleUrls: ['./home-tab.component.css']
})
export class HomeTabComponent {

  tabsArray: HomeTab[];
  constructor() { }
}
