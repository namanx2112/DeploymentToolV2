import { Component, EventEmitter, Output } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { AccessService } from 'src/app/services/access.service';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-home-dashboard',
  templateUrl: './home-dashboard.component.html',
  styleUrls: ['./home-dashboard.component.css']
})
export class HomeDashboardComponent {
  brands: BrandModel[];
  @Output() brandClicked = new EventEmitter<BrandModel>()
  constructor(private homeService: HomeService, public access: AccessService, private commonService: CommonService) {
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
    let cThis= this;
    this.commonService.getBrands(function(resp: any){
      cThis.brands = resp;
      cThis.updateIconsTemp();
    });
  }

  filterBrand(brand: BrandModel) {
    return (typeof brand.access == 'undefined') ? true : brand.access;
  }

  showBrand(brand: BrandModel) {
    this.brandClicked.emit(brand);
  }

  updateIconsTemp() {
    for (var i in this.brands) {
      if (this.brands[i].tBrandName?.toLowerCase().indexOf("dunkin") > -1) {
        this.brands[i].tIconURL = "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("baskin") > -1) {
        this.brands[i].tIconURL = "https://1000logos.net/wp-content/uploads/2016/10/Baskin-Robbins-Logo-2020.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("buffa") > -1) {
        this.brands[i].tIconURL = "https://logos-world.net/wp-content/uploads/2022/01/Buffalo-Wild-Wings-Logo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("arby") > -1) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Arby%27s_logo.svg/2394px-Arby%27s_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("jimm") > -1) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Jimmy_Johns_logo.svg/1200px-Jimmy_Johns_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("sonic") > -1) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/f/ff/SONIC_New_Logo_2020.svg";
      }
      else {
        this.brands[i].tIconURL = "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png";
      }
    }
  }
}
