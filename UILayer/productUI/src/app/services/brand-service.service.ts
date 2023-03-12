import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BrandModel } from '../interfaces/models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BrandServiceService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  CreateBrand(request: any) {
    return this.http.post<BrandModel>(this.configUrl + "Brand/CreateBrand", request, { headers: this.authService.getHttpHeaders() });
  }

  UpdateBrand(request: any) {
    return this.http.post<BrandModel>(this.configUrl + "Brand/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetBrands(request: BrandModel | null){
    return this.http.post<BrandModel[]>(this.configUrl + "Brand/GetBrands", request, { headers: this.authService.getHttpHeaders() });
  }

  GetAllBrands(): BrandModel[] {
    let brands: BrandModel[] = [
      {
        aBrandId: 0,
        tBrandName: "Dunkin",
        tBrandDescription: "Dunkin",
        tBrandWebsite: "Dunkin",
        tBrandCountry: "Dunkin",
        tBrandEstablished: new Date(),
        tBrandCategory: "Dunkin",
        tBrandContact: "Dunkin",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png"
      },
      {
        aBrandId: 0,
        tBrandName: "Baskin Robins",
        tBrandDescription: "Baskin Robins",
        tBrandWebsite: "Baskin Robins",
        tBrandCountry: "Baskin Robins",
        tBrandEstablished: new Date(),
        tBrandCategory: "Baskin Robins",
        tBrandContact: "Baskin Robins",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://1000logos.net/wp-content/uploads/2016/10/Baskin-Robbins-Logo-2020.png"
      },
      {
        aBrandId: 0,
        tBrandName: "Buffalo Wild Wings",
        tBrandDescription: "Buffalo Wild Wings",
        tBrandWebsite: "Buffalo Wild Wings",
        tBrandCountry: "Buffalo Wild Wings",
        tBrandEstablished: new Date(),
        tBrandCategory: "Buffalo Wild Wings",
        tBrandContact: "Buffalo Wild Wings",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://logos-world.net/wp-content/uploads/2022/01/Buffalo-Wild-Wings-Logo.png"
      },
      {
        aBrandId: 0,
        tBrandName: "Arbys",
        tBrandDescription: "Arbys",
        tBrandWebsite: "Arbys",
        tBrandCountry: "Arbys",
        tBrandEstablished: new Date(),
        tBrandCategory: "Arbys",
        tBrandContact: "Arbys",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Arby%27s_logo.svg/2394px-Arby%27s_logo.svg.png"
      },
      {
        aBrandId: 0,
        tBrandName: "Jimmy Johns",
        tBrandDescription: "Jimmy Johns",
        tBrandWebsite: "Jimmy Johns",
        tBrandCountry: "Jimmy Johns",
        tBrandEstablished: new Date(),
        tBrandCategory: "Jimmy Johns",
        tBrandContact: "Jimmy Johns",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Jimmy_Johns_logo.svg/1200px-Jimmy_Johns_logo.svg.png"
      },
      {
        aBrandId: 0,
        tBrandName: "Sonic",
        tBrandDescription: "Sonic",
        tBrandWebsite: "Sonic",
        tBrandCountry: "Sonic",
        tBrandEstablished: new Date(),
        tBrandCategory: "Sonic",
        tBrandContact: "Sonic",
        nBrandLogoAttachmentID: 1,
        nCreatedBy: 1,
        nUpdateBy: 1,
        dtCreatedOn: new Date(),
        dtUpdatedOn: new Date(),
        tIconURL: "https://upload.wikimedia.org/wikipedia/commons/f/ff/SONIC_New_Logo_2020.svg"
      }
    ];
    return brands;
  }
}
