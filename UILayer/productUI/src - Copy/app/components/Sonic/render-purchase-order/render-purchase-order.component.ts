import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MergedPO, POConfigPart, POMailMessage, PartsModel } from 'src/app/interfaces/models';
import { CommonService } from 'src/app/services/common.service';
import { PartsService } from 'src/app/services/parts.service';
import { POWorkflowConfigService } from 'src/app/services/poworklow-config.service';


@Component({
  selector: 'app-render-purchase-order',
  templateUrl: './render-purchase-order.component.html',
  styleUrls: ['./render-purchase-order.component.css']
})
export class RenderPurchaseOrderComponent {
  curTemplate: MergedPO;
  nTemplateId: number;
  nStoreId: number;
  onDone: any;
  allParts: PartsModel[];
  partNumber: number;
  tRequest: POMailMessage;
  ckConfig: any;
  constructor(public dialogRef: MatDialogRef<RenderPurchaseOrderComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
    private poService: POWorkflowConfigService, private partService: PartsService, private commonService: CommonService) {
    this.ckConfig = this.commonService.GetCKEditorConfig("calc(100vh - 515px)");
    this.partNumber = 1;
    this.nTemplateId = data.nTemplateId;
    this.nStoreId = data.nStoreId;
    this.onDone = data.onDone;
    this.getMergedContent();
  }




  // First Part Start
  getMergedContent() {
    this.poService.GetMergedPO(this.nStoreId, this.nTemplateId).subscribe(x => {
      this.curTemplate = x;
      this.reCalculateTotal();
      this.getAllParts();
    });
  }

  getAllParts() {
    this.partService.Get({ nVendorId: this.curTemplate.nVendorId }).subscribe(x => {
      this.allParts = x;
      this.filterVendorParts();
    });
  }

  filterVendorParts() {
    for (var indx in this.allParts) {
      if (this.curTemplate.purchaseOrderParts.findIndex(x => x.nPartID == this.allParts[indx].aPartID) > -1)
        this.allParts[indx].show = false;
      else
        this.allParts[indx].show = true;
    }
  }

  filterParts(part: PartsModel) {
    return (part.show);
  }

  downloadPDF(tMyFolderId: string, tFieName: string) {
    this.poService.downloadPO(tMyFolderId, tFieName, this.nStoreId).subscribe(tdata => {
      var newBlob = new Blob([tdata], { type: "application/pdf" });

      // For other browsers: 
      // Create a link pointing to the ObjectURL containing the blob.
      const data = window.URL.createObjectURL(newBlob);

      var link = document.createElement('a');
      link.href = data;
      link.download = tFieName;
      // this is necessary as link.click() does not work on the latest firefox
      link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

      setTimeout(function () {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(data);
        link.remove();
      }, 100);
    }, async (error) => {
      console.log("Error occured:" + error);
    });
  }


  GenerateMail() {
    this.poService.SenMergedPO(this.curTemplate).subscribe(x => {
      this.tRequest = x;
      this.partNumber = 2;
    });
  }

  MoveBack() {
    this.partNumber = 1;
  }

  cannotNext() {
    let cant = false;
    if (typeof this.curTemplate != 'undefined') {
      if (this.curTemplate.tName == "" || this.curTemplate.tAddress == "" || this.curTemplate.tBillToAddress == "" || this.curTemplate.tBillToCity == "" || this.curTemplate.tBillToCompany == ""
        || this.curTemplate.tBillToEmail == "" || this.curTemplate.tBillToState == "" || this.curTemplate.tCity == "" || this.curTemplate.tEmail == "" ||
        this.curTemplate.tPhone == "")
        cant = true;
      else if (this.curTemplate.purchaseOrderParts.length == 0)
        cant = true;
      else {
        if (this.curTemplate.purchaseOrderParts.find(x => x.cPrice < 0 || x.nQuantity < 0 || x.tPartDesc == "" || x.tPartNumber == ""))
          cant = true;
      }
    }
    return cant;
  }

  onCancelUserDialog() {

  }

  validate(input: any) {
    if (/^\s/.test(input.value))
      input.value = '';
  }

  onNumber(event: any) {
    return (event.charCode != 8 && event.charCode == 0 || (event.charCode >= 48 && event.charCode <= 57));
  }

  numberChanged(input: any) {
    this.reCalculateTotal();
  }

  notAddedPars() {
    return this.allParts.filter(x => x.show);
  }

  reCalculateTotal() {
    let aTotal = 0;
    for (var indx in this.curTemplate.purchaseOrderParts) {
      let tItem = this.curTemplate.purchaseOrderParts[indx];
      tItem.cTotal = parseFloat((tItem.cPrice * tItem.nQuantity).toFixed(2));
      aTotal += tItem.cTotal;
    }
    this.curTemplate.cTotal = parseFloat(aTotal.toFixed(2));
  }

  addPart(ev: any) {
    let partId = ev.target.value;
    if (partId) {
      let part = this.allParts.find(x => x.aPartID == partId);
      if (part) {
        this.curTemplate.purchaseOrderParts.push({
          aPurchaseOrderTemplatePartsID: 0,
          nPartID: part.aPartID,
          tPartDesc: part.tPartDesc,
          tPartNumber: part.tPartNumber,
          cPrice: part.cPrice,
          tTableName: "",
          tTechCompField: "",
          nQuantity: 1,
          cTotal: 1,
          selected: false
        });
        this.filterVendorParts();
        this.reCalculateTotal();
      }
    }
  }

  removePart(item: POConfigPart, indx: number) {
    this.curTemplate.purchaseOrderParts.splice(indx, 1);
    this.filterVendorParts();
    this.reCalculateTotal();
  }

  getFormatedDate(dt: Date) {
    return new Date(dt).toLocaleDateString();
  }
  // First Part End
  //Second Part Start
  cannotSend() {
    let cannot = false;
    if (this.tRequest.tSubject == "")
      cannot = true;
    if (!cannot) {
      let eIds = this.tRequest.tTo.split(";");
      for (var indx in eIds) {
        if (!this.isValidEmail(eIds[indx].trim())) {
          cannot = true;
          break;
        }
      }
      if (!cannot && this.tRequest.tCC != null && this.tRequest.tCC != '') {
        let eIds = this.tRequest.tCC.split(";");
        for (var indx in eIds) {
          if (!this.isValidEmail(eIds[indx].trim())) {
            cannot = true;
            break;
          }
        }
      }
    }
    return cannot;
  }

  isValidEmail(email: string) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  SendRequest() {
    this.poService.SendPO(this.tRequest).subscribe(x => {
      alert("PO sent!");
      this.dialogRef.close();
    });
  }
  //Second Part End
}
