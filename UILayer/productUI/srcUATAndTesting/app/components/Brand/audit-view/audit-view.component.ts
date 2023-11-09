import { Component, Input } from '@angular/core';
import { Dictionary } from 'src/app/interfaces/commons';
import { FieldType, HomeTab, OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { AuditModel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-audit-view',
  templateUrl: './audit-view.component.html',
  styleUrls: ['./audit-view.component.css']
})
export class AuditViewComponent {

  @Input()
  set request(val: any) {
    this._nStoreId = val.nStoreId;
    this._nBrandId = val.nBrandId;
    this.initMeta();
    this.loadStoreAudit();
  }

  _nBrandId: number;
  _nStoreId: number;
  _curAudits: AuditModel[];
  techComps: Dictionary<HomeTab>;
  allUsers: OptionType[];
  constructor(private storeService: StoreService, private exStore: ExStoreService,
    private commonService: CommonService) {

  }

  initMeta() {
    this.techComps = {};
    this.techComps["Store Contact"] = this.exStore.GetStoreContactTab(TabInstanceType.Single);
    this.techComps["Store Configuration"] = this.exStore.GetStoreConfigurationTab(TabInstanceType.Single);
    this.techComps["Stake Holders"] = this.exStore.GetStoreStackholderTab(TabInstanceType.Single);
    this.techComps["Networking"] = this.exStore.GetStoreNetworingTab(TabInstanceType.Single);
    this.techComps["POS"] = this.exStore.GetStorePOSTab(TabInstanceType.Single);
    this.techComps["Audio"] = this.exStore.GetStoreAudioTab(TabInstanceType.Single);
    this.techComps["Exterior Menus"] = this.exStore.GetStoreExteriorMenusTab(TabInstanceType.Single);
    this.techComps["Payment System"] = this.exStore.GetStorePaymentSystemTab(TabInstanceType.Single);
    this.techComps["Interior Menus"] = this.exStore.GetStoreInteriorMenusTab(TabInstanceType.Single);
    this.techComps["Server Handheld"] = this.exStore.GetStoreServerHandheldTab(TabInstanceType.Single);
    this.techComps["Radio"] = this.exStore.GetStoreRadioTab(TabInstanceType.Single);
    this.techComps["Sonic Radio"] = this.exStore.GetStoreSonicRadioTab(TabInstanceType.Single);
    this.techComps["Installation"] = this.exStore.GetStoreInsallationTab(TabInstanceType.Single);
    this.allUsers = this.commonService.GetDropdownOptions(0, "User");
  }

  loadStoreAudit() {
    this.storeService.GetAudits(this._nBrandId, this._nStoreId).subscribe((x: AuditModel[]) => {
      this._curAudits = x;
    })
  }

  getFormatedDate(sDate: any) {
    return CommonService.getFormatedDateString(sDate, true);
  }

  getUserName(userId: number) {
    let tName = "";
    let tItem = this.allUsers.find(x => x.aDropdownId == userId.toString());
    if (tItem)
      tName = tItem.tDropdownText;
    return tName;
  }

  getFieldName(compName: string, fieldName: string) {
    let tName = "";
    let tField = this.techComps[compName].fields.find(x => x.fieldUniqeName == fieldName);
    if (tField)
      tName = tField.field_name;
    return tName;
  }

  getFormatedVal(compName: string, fieldName: string, fieldVal: string) {
    let tName = "";
    let tField = this.techComps[compName].fields.find(x => x.fieldUniqeName == fieldName);
    if (tField) {
      switch (tField.field_type) {
        case FieldType.date:
          tName = CommonService.getFormatedDateString(fieldVal);
          break;
        case FieldType.currency:
          tName = "$" + fieldVal;
          break;
        case FieldType.dropdown:
          let tOption = this.commonService.GetDropdownOptions(this._nBrandId, tField.options);
          let tItem = tOption.filter(x => x.aDropdownId == fieldVal);
          if (tItem && tItem.length > 0)
            tName = tItem[0].tDropdownText;
          break;
        default:
          tName = fieldVal;
      }
    }
    return tName;
  }
}
