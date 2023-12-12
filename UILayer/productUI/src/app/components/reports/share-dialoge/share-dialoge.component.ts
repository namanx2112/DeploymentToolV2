import { Component, Inject } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportInfo } from 'src/app/interfaces/report-generator';
import { CommonService } from '../../../services/common.service';
import { OptionType } from 'src/app/interfaces/home-tab';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';

@Component({
  selector: 'app-share-dialoge',
  templateUrl: './share-dialoge.component.html',
  styleUrls: ['./share-dialoge.component.css']
})
export class ShareDialogeComponent {

  reportIds: number[] = [];
  brands: BrandModel[] = [];
  roles: OptionType[] = [];
  selectedRole: number[] = [];
  selectedBrand: number[] = [];
  afterClose: any;
  curModel: any;
  constructor(private commonService: CommonService, @Inject(MAT_DIALOG_DATA) public data: any, private rgService: ReportGeneratorService) {
    this.brands = CommonService.allBrands;
    this.roles = this.commonService.GetDropdownOptions(0, "UserRole", true);
    if (typeof data != 'undefined') {
      this.selectedBrand = [data.curBrand.aBrandId];
      this.reportIds = data.reportIds;
      this.afterClose = data.afterClose;
      this.curModel = data.curModel;
    }
    this.getSharedDetails();
  }

  compareDropDown(o1: any, o2: any) {
    if (parseInt(o1) == parseInt(o2))
      return true;
    else return false
  }

  getSharedDetails() {
    if (this.reportIds.length == 1) {
      this.rgService.GetShareDetails(this.reportIds[0]).subscribe(x => {
        if (typeof x.brands != 'undefined' && x.brands.length > 0)
          this.selectedBrand = x.brands;
        if (typeof x.roles != 'undefined' && x.roles.length > 0)
          this.selectedRole = x.roles;
      });
    }
    else if (typeof this.curModel != 'undefined' && typeof this.curModel.shareRequest != 'undefined') {
      this.selectedBrand = this.curModel.shareRequest.brands;
      this.selectedRole = this.curModel.shareRequest.roles;
    }
  }

  onShare() {
    let brands: number[] = [];
    let roles: number[] = [];
    for (var indx in this.selectedBrand)
      brands.push(this.selectedBrand[indx]);
    for (var indx in this.selectedRole)
      roles.push(this.selectedRole[indx]);
    if (this.reportIds.length == 0) {
      this.curModel.shareRequest = { brands: brands, roles: roles };
      this.afterClose();
    }
    else
      this.rgService.ShareReport({ reportIds: this.reportIds, brands: brands, roles: roles }).subscribe(x => {
        this.afterClose();
      });
  }
}
