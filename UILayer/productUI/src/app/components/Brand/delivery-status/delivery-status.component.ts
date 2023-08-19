import { Component, Input } from '@angular/core';
import { DeliveryStatus, StoreSearchModel } from 'src/app/interfaces/sonic';
import { CommonService } from 'src/app/services/common.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-delivery-status',
  templateUrl: './delivery-status.component.html',
  styleUrls: ['./delivery-status.component.css']
})
export class DeliveryStatusComponent {

  _curStore: StoreSearchModel;
  @Input()
  set curStore(val: StoreSearchModel) {
    this._curStore = val;
    this.loadStatus();
  }
  allStatus: DeliveryStatus[]
  constructor(private storeService: StoreService) {

  }

  getFormateDate(str: any) {
    return CommonService.getFormatedDateString(str);
  }

  loadStatus() {
    this.storeService.GetDeliveryStatus(this._curStore.nStoreId).subscribe(x => {
      this.allStatus = x;
    })
  }
}
