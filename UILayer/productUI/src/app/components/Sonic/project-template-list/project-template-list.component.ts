import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ProjectTemplateType, ProjectTemplates } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { StoreService } from 'src/app/services/store.service';
import { RenderQuoteRequestComponent } from '../render-quote-request/render-quote-request.component';
import { RenderPurchaseOrderComponent } from '../render-purchase-order/render-purchase-order.component';

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
  }
  @Output() BackToStoreView = new EventEmitter<any>();
  nBrandId: number = 1;
  notificationTemplates: ProjectTemplates[];
  quoteRequestTemplates: ProjectTemplates[];
  poTemplates: ProjectTemplates[];
  constructor(private service: StoreService, private dialog: MatDialog) {
    this.getAllTemplates();
  }

  getAllTemplates() {
    this.service.GetProjectTemplates(this.nBrandId).subscribe((x: ProjectTemplates[]) => {
      this.notificationTemplates = x.filter(x => x.nTemplateType == ProjectTemplateType.Notification);
      this.quoteRequestTemplates = x.filter(x => x.nTemplateType == ProjectTemplateType.QuoteRequest);
      this.poTemplates = x.filter(x => x.nTemplateType == ProjectTemplateType.PurchaseOrder);
    });
  }

  goBack() {
    this.BackToStoreView.emit(this._curStore);
  }

  OpenNotification(item: ProjectTemplates) {

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
    dialogConfig.height = '100%';
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
