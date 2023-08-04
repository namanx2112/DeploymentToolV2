import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { POConfigPart, POConfigTemplate, PartsModel, VendorModel } from 'src/app/interfaces/models';
import { PartsService } from 'src/app/services/parts.service';
import { POWorkflowConfigService } from 'src/app/services/poworklow-config.service';
import { SonicService } from 'src/app/services/sonic.service';
import { VendorService } from 'src/app/services/vendor.service';

@Component({
  selector: 'app-purchase-order-workflow-template',
  templateUrl: './purchase-order-workflow-template.component.html',
  styleUrls: ['./purchase-order-workflow-template.component.css']
})
export class PurchaseOrderWorkflowTemplateComponent {
  _nTemplateId: number;
  @Input() set nTemplateId(val: number) {
    this._nTemplateId = val;
    this.getTemplate();
  }
  @Output()
  moveBack = new EventEmitter<number>();
  nBrandId: number = 1;
  curTemplate: POConfigTemplate;
  techCompControl = new FormControl('');
  myControl = new FormControl('');
  filteredOptions: Observable<OptionType[]>;
  allVendors: VendorModel[] = [];
  selectedVendor: VendorModel;
  vendorParts: POConfigPart[];
  allCompAndFields: any[];
  allCompNames: any[];
  curQuantityFields: any[];
  constructor(private poService: POWorkflowConfigService, private vendorService: VendorService, private partsService: PartsService, private sonicService: SonicService) {

  }

  fieldChanged(ev: any, item: POConfigPart, indx: number) {
    let tItem = this.curTemplate.purchaseOrderParts.find(x => x.nPartID == item.nPartID);
    if (tItem) {
      tItem.tTableName = ev.value.tableName;
      tItem.tTechCompField = ev.value.field;
    }
    if (this.vendorParts[indx]) {
      this.vendorParts[indx].tTableName = ev.value.tableName;
      this.vendorParts[indx].tTechCompField = ev.value.field;
    }
  }

  private _filter(value: string): OptionType[] {
    if (typeof value == 'string') {
      const filterValue = value.toLowerCase();

      return this.getOptionsFromVendors(this.allVendors.filter(option => option.tVendorName.toString().toLowerCase().includes(filterValue)));
    }
    else
      return [];
  }

  getQuantityFields() {
    this.allCompAndFields = this.sonicService.GetPOQuantityFields();
    this.allCompNames = this.sonicService.getPOTechConfigs();
  }

  getOptionsFromVendors(vendors: VendorModel[]): OptionType[] {
    let oArr: OptionType[] = [];
    for (var indx in vendors) {
      let tVendor = vendors[indx];
      oArr.push({
        optionDisplayName: tVendor.tVendorName,
        optionIndex: tVendor.aVendorId.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      });
    }
    return oArr;
  }

  getVendorParts(callBack: any, selectedParts: POConfigPart[]) {
    this.partsService.Get({ nVendorId: this.selectedVendor.aVendorId }).subscribe((x: PartsModel[]) => {
      this.vendorParts = [];
      this.curQuantityFields = [];
      var notFoundParts = [];
      for (var indx in x) {
        let tPart = x[indx];
        let selectedPart: POConfigPart = {
          nPartID: 0,
          tPartDesc: "",
          tPartNumber: "",
          cPrice: 0,
          aPurchaseOrderTemplatePartsID: 0, selected: false, tTableName: "", tTechCompField: "",
          cTotal: 0,
          nQuantity: 0
        };
        if (typeof selectedParts.find(x => x.nPartID == tPart.aPartID) != 'undefined') {
          let tmp = selectedParts.find(x => x.nPartID == tPart.aPartID);
          if (tmp) {
            selectedPart = tmp;
            selectedPart.selected = true;
          }
        }
        this.vendorParts.push({
          aPurchaseOrderTemplatePartsID: selectedPart.aPurchaseOrderTemplatePartsID,
          nPartID: tPart.aPartID,
          tPartDesc: tPart.tPartDesc,
          tPartNumber: tPart.tPartNumber,
          cPrice: tPart.cPrice,
          tTableName: selectedPart.tTableName,
          tTechCompField: selectedPart.tTechCompField,
          selected: selectedPart.selected,
          cTotal: 0,
          nQuantity: 0
        });
        this.curQuantityFields.push({ title: "", field: "", tableName: "" });
      }
      if (selectedParts.length > 0) {
        for (var indx in selectedParts) {
          if (typeof x.find(y => y.aPartID == selectedParts[indx].nPartID) == 'undefined')
            notFoundParts.push(selectedParts[indx].nPartID);
        }
      }
      callBack(notFoundParts);
    });
  }

  updateItemFromCheckbox() {
    this.curTemplate.purchaseOrderParts = [];
    for (var indx in this.vendorParts) {
      if (this.vendorParts[indx].selected)
        this.curTemplate.purchaseOrderParts.push(this.vendorParts[indx]);
    }
  }

  getVendors(callBack: any) {
    this.vendorService.Get({ nBrand: 1 }).subscribe((x: VendorModel[]) => {
      this.allVendors = x;
      this.filteredOptions = this.myControl.valueChanges.pipe(
        startWith(''),
        map(value => this._filter(value || '')),
      );
      callBack();
    });
  }

  getTemplate() {
    let cThis = this;
    let userId = 1;
    this.getQuantityFields();
    this.getVendors(function () {
      if (cThis._nTemplateId > 0) {
        cThis.poService.GetTemplate(cThis._nTemplateId).subscribe((x: POConfigTemplate) => {
          cThis.curTemplate = x;
          let cVendor = cThis.allVendors.find(y => y.aVendorId == x.nVendorID);
          if (cVendor) {
            cThis.myControl.setValue(cVendor.tVendorName);
            cThis.selectedVendor = cVendor;
            cThis.getVendorParts(function (notFoundIndex: []) {
              cThis.curQuantityFields = [];
              for (var indx in cThis.curTemplate.purchaseOrderParts) {
                cThis.curQuantityFields.push({ field: cThis.curTemplate.purchaseOrderParts[indx].tTechCompField, tableName: cThis.curTemplate.purchaseOrderParts[indx].tTableName });
                cThis.curTemplate.purchaseOrderParts[indx].selected = true;
              }
              if (notFoundIndex.length > 0) {
                alert("Some of the selected parts are not available in this Vendor!");
                for (var indx in notFoundIndex) {
                  let tIndx = cThis.curTemplate.purchaseOrderParts.findIndex(x => x.nPartID == notFoundIndex[indx]);
                  if (tIndx > -1) {
                    cThis.curTemplate.purchaseOrderParts.splice(tIndx, 1);
                  }
                }
              }
            }, cThis.curTemplate.purchaseOrderParts);
          }
        });
      }
      else {
        cThis.curTemplate = {
          aPurchaseOrderTemplateID: 0,
          tTemplateName: "",
          tCompName: "",
          nVendorID: (cThis.selectedVendor) ? cThis.selectedVendor.aVendorId : 0,
          nBrandID: cThis.nBrandId,
          purchaseOrderParts: []
        }
      }
    });
  }

  onVendorChange(ev: any) {
    let tVendor = this.allVendors.find(x => x.tVendorName == ev.option.value);
    if (tVendor) {
      this.selectedVendor = tVendor;
      this.curTemplate.purchaseOrderParts = [];
      this.curTemplate.nVendorID = this.selectedVendor.aVendorId;
      this.getVendorParts(function () { }, []);
    }
  }

  goBack() {
    this.moveBack.emit(0);
  }

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  canSave() {
    let can = false;
    if (this.curTemplate.purchaseOrderParts.length > 0 && this.curTemplate.tTemplateName.length > 0) {
      let emptyItems = this.curTemplate.purchaseOrderParts.find(x => x.tTableName == "" || x.tTechCompField == "");
      if (typeof emptyItems == 'undefined' || emptyItems == null)
        can = true;
    }
    else if (this.curTemplate.nVendorID > 0 && this.curTemplate.tCompName != "")
      can = false;
    return can;
  }

  submit() {
    this.poService.CreateUpdateTemplate(this.curTemplate).subscribe((x: POConfigTemplate) => {
      alert("Saved succesfully");
      this.moveBack.emit(1);
    });
  }

  customQuantityCompare(v1: any, v2: any) {
    return (v1.tableName == v2.tableName && v1.field == v2.field)
  }

  customComponentCompare(v1: any, v2: any) {
    return (v1 == v2)
  }
}
