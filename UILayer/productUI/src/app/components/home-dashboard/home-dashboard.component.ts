import { Component } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-home-dashboard',
  templateUrl: './home-dashboard.component.html',
  styleUrls: ['./home-dashboard.component.css']
})
export class HomeDashboardComponent {
  brands: BrandModel[];
  constructor(private homeService: HomeService, private brandService: BrandServiceService) {
    this.getBrands();
  }

  getBrands() {
    // this.brands = this.brandService.GetAllBrands();
    //  {
    //   aBrandId: 0,
    //   tBrandName: '',
    //   tBrandDescription: '',
    //   tBrandWebsite: '',
    //   tBrandCountry: '',
    //   tBrandEstablished: null,
    //   tBrandCategory: '',
    //   tBrandContact: '',
    //   nBrandLogoAttachmentID: 0,
    //   nCreatedBy:0 ,
    //   nUpdateBy: 0,
    //   dtCreatedOn: null,
    //   dtUpdatedOn: null,
    //   tIconURL: ''
    // }
    this.brandService.GetBrands(null).subscribe((resp: BrandModel[]) => {
      this.brands = resp;
      this.updateIconsTemp();
    })
  }

  updateIconsTemp() {
    for (var i in this.brands) {
      if (this.brands[i].tBrandName?.toLowerCase().indexOf("dunkin")) {
        this.brands[i].tIconURL = "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("baskin")) {
        this.brands[i].tIconURL = "https://1000logos.net/wp-content/uploads/2016/10/Baskin-Robbins-Logo-2020.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("buffe")) {
        this.brands[i].tIconURL = "https://logos-world.net/wp-content/uploads/2022/01/Buffalo-Wild-Wings-Logo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("arby")) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Arby%27s_logo.svg/2394px-Arby%27s_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("jimm")) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Jimmy_Johns_logo.svg/1200px-Jimmy_Johns_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("sonic")) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/f/ff/SONIC_New_Logo_2020.svg";
      }      
      else {
        this.brands[i].tIconURL = "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png";
      }
    }
  }
}
