import { Component } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  viewName:string;
  constructor(private homeService: HomeService, private brandService: BrandServiceService, private commonService: CommonService) {
    this.viewName = "Dashboard";
    this.commonService.getAllDropdowns();
  }

  getValue() {
    this.homeService.loginGet().subscribe((res: string) => {
      alert(res);
    });
  }  

  switchView(vName: string){
    this.viewName = vName;
  }

  brandClicked(brand: BrandModel){
    this.viewName = "Sonic";
  }
}
