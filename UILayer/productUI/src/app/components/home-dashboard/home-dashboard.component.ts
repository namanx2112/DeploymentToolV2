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
  @Output() changeView = new EventEmitter<any>();
  @Output() brandSelect = new EventEmitter<any>();
  
  constructor(private homeService: HomeService, public access: AccessService, private commonService: CommonService) {
    this.getBrands();
  }

  getBrands() {
    let cThis = this;
    this.commonService.getBrands(function (resp: any) {
      cThis.brands = resp;
      //cThis.updateIconsTemp();
    });
  }

  filterBrand(brand: BrandModel) {
    return (typeof brand.access == 'undefined') ? true : brand.access;
  }

  showBrand(brand: BrandModel) {
    this.brandSelect.emit(brand);
  }

  showNotification() {
    this.changeView.emit({ view: "notifications", instance: null });
  }

  openStoreFromNotification(item: any) {
    this.changeView.emit({ view: "brandhome", instance: item });
  }
}
