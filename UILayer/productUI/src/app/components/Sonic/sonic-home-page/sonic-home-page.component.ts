import { Component } from '@angular/core';
import { StoreSearchModel } from 'src/app/interfaces/sonic';

@Component({
  selector: 'app-sonic-home-page',
  templateUrl: './sonic-home-page.component.html',
  styleUrls: ['./sonic-home-page.component.css']
})
export class SonicHomePageComponent {
  showMode: string;
  curStore: StoreSearchModel;
  constructor(){
    this.showMode = "dashboard";
  }
  clickOption(val: any){} 


  menuClick(tMode: string){
    this.showMode = tMode;
  }

  SearchedResult(val: any){
    this.curStore = val;
    this.showMode = "storeview";
  }

  ChangeView(view: any){
    this.showMode = view;
  }
}
