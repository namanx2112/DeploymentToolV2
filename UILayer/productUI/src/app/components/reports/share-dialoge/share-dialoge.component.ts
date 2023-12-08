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
  selectedRole: OptionType[] = [];
  selectedBrand: BrandModel[] = [];
  afterClose: any;
  constructor(private commonService: CommonService, @Inject(MAT_DIALOG_DATA) public data: any, private rgService: ReportGeneratorService) {
    this.brands = CommonService.allBrands;
    this.roles = this.commonService.GetDropdownOptions(0, "UserRole", true);
    if (typeof data != 'undefined') {
      this.selectedBrand = [data.curBrand];
      this.reportIds = data.reportIds;
      this.afterClose = data.afterClose;
    }
  }

  onShare() {
    let brands: number[] = [];
    let roles: number[] = [];
    for (var indx in this.selectedBrand)
      brands.push(this.selectedBrand[indx].aBrandId);
    for (var indx in this.selectedRole)
      roles.push(parseInt(this.selectedRole[indx].aDropdownId));
    this.rgService.ShareReport({reportIds: this.reportIds, brands: brands, roles: roles}).subscribe(x=>{
      this.afterClose();
    });
  }
}
