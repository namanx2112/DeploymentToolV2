import { Component, EventEmitter, Output } from '@angular/core';
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
  @Output() brandClicked = new EventEmitter<BrandModel>()
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
    this.brandService.Get(null).subscribe((resp: BrandModel[]) => {
      this.brands = resp;
      this.updateIconsTemp();
    })
  }

  showBrand(brand: BrandModel) {
    this.brandClicked.emit(brand);
  }

  updateIconsTemp() {
    for (var i in this.brands) {
      if (this.brands[i].tBrandName?.toLowerCase().indexOf("dunkin")> -1) {
        this.brands[i].tIconURL = "1602742091_DUNKINLogo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("baskin") > -1) {
        this.brands[i].tIconURL = "Baskin-Robbins-Logo-2020.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("buffe") > -1) {
        this.brands[i].tIconURL = "Buffalo-Wild-Wings-Logo.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("arby") > -1) {
        this.brands[i].tIconURL = "Arby%27s_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("jimm") > -1) {
        this.brands[i].tIconURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Jimmy_Johns_logo.svg/1200px-Jimmy_Johns_logo.svg.png";
      }
      else if (this.brands[i].tBrandName?.toLowerCase().indexOf("sonic") > -1) {
        this.brands[i].tIconURL = "SONIC_New_Logo_2020.svg";
      }
      else {
        this.brands[i].tIconURL = "1602742091_DUNKINLogo.png";
      }
    }
  }
}
