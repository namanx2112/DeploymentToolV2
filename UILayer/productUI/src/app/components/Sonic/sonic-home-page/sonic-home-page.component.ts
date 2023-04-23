import { Component } from '@angular/core';

@Component({
  selector: 'app-sonic-home-page',
  templateUrl: './sonic-home-page.component.html',
  styleUrls: ['./sonic-home-page.component.css']
})
export class SonicHomePageComponent {
  showMode: string;
  constructor(){
    this.showMode = "dashboard";
  }
  clickOption(val: any){} 


  menuClick(tMode: string){
    this.showMode = tMode;
  }

  SearchedResult(txt: any){
    this.showMode = "storeview";
  }
}
