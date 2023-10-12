import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ProjectTemplateType, ProjectTemplates } from 'src/app/interfaces/models';
import { ProjectTypes, StoreSearchModel } from 'src/app/interfaces/store';
import { StoreService } from 'src/app/services/store.service';
import { RenderQuoteRequestComponent } from '../render-quote-request/render-quote-request.component';
import { RenderPurchaseOrderComponent } from '../render-purchase-order/render-purchase-order.component';
import { AccessService } from 'src/app/services/access.service';
import { RenderDateChangeTemplateComponent } from '../render-date-change-template/render-date-change-template.component';

@Component({
  selector: 'app-project-template-list',
  templateUrl: './project-template-list.component.html',
  styleUrls: ['./project-template-list.component.css']
})
export class ProjectTemplateListComponent {
  _curStore: StoreSearchModel;
  @Input()
  public set curStore(value: StoreSearchModel) {
    this._curStore = value;


    1
    this.getAllTemplates();
  }
  @Output() BackToStoreView = new EventEmitter<any>();
  nBrandId: number = 1;
  notificationTemplates: ProjectTemplates[];
  quoteRequestTemplates: ProjectTemplates[];
  poTemplates: ProjectTemplates[];
  constructor(private service: StoreService, private dialog: MatDialog, public access: AccessService) {

  }

  getAllTemplates() {
    this.service.GetProjectTemplates(this.nBrandId).subscribe((x: ProjectTemplates[]) => {
      this.notificationTemplates = x.filter(x => x.nTemplateType == ProjectTemplateType.Notification);
      this.quoteRequestTemplates = x.filter(x => x.nTemplateType == ProjectTemplateType.QuoteRequest);
      this.setPOTemplates(x.filter(x => x.nTemplateType == ProjectTemplateType.PurchaseOrder));
    });
  }

  setPOTemplates(arr: ProjectTemplates[]) {
    let pTypes: ProjectTypes[] = [];
    for (var indx in this._curStore.lstProjectsInfo) {
      pTypes.push(this._curStore.lstProjectsInfo[indx].nProjectType);
    }
    let outPutArr: ProjectTemplates[] = [];
    var addToArray = function (items: ProjectTemplates[]) {
      for (var indx in items)
        outPutArr.push(items[indx]);
    }
    for (var indx in pTypes) {
      switch (pTypes[indx]) {
        case ProjectTypes.POSInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("pos") > -1));
          break;
        case ProjectTypes.AudioInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("audio") > -1));
          break;
        case ProjectTypes.PaymentTerminalInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("payment") > -1));
          break;
        case ProjectTypes.InteriorMenuInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("menu") > -1));
          break;
        case ProjectTypes.ExteriorMenuInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("menu") > -1));
          break;
        case ProjectTypes.ServerHandheldInstallation:
          addToArray(arr.filter(x => x.tComponent.toLocaleLowerCase().indexOf("server") > -1));
          break;
        default:
          addToArray(arr);
          break;
      }
    }
    if (arr.length > outPutArr.length) { // Means it was some tech project, so add Sonic Radio and Installation type of Project also
      for (var indx in arr) {
        if (arr[indx].tComponent == "Installation" || arr[indx].tComponent.toLocaleLowerCase().indexOf("sonic") > -1)
          outPutArr.push(arr[indx]);
      }
    }
    this.poTemplates = outPutArr;
  }

  goBack() {
    this.BackToStoreView.emit(this._curStore);
  }

  OpenNotification(item: ProjectTemplates) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '80%';
    dialogConfig.width = '60%';
    dialogConfig.data = {
      curStore: this._curStore,
      onSubmit: function (data: any) {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(RenderDateChangeTemplateComponent, dialogConfig);
  }

  OpenQuoteRequest(item: ProjectTemplates) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    //dialogConfig.height = '100%';
    dialogConfig.data = {
      curStore: this._curStore,
      curTemplate: item,
      onSubmit: function (data: any) {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(RenderQuoteRequestComponent, dialogConfig);
  }

  OpenPurchaseOrder(item: ProjectTemplates) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '90%';
    dialogConfig.width = '60%';
    dialogConfig.data = {
      nStoreId: this._curStore.nStoreId,
      nTemplateId: item.nTemplateId,
      onSubmit: function (data: any) {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(RenderPurchaseOrderComponent, dialogConfig);
  }
}
