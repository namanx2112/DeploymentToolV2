import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { OptionType, TabInstanceType } from 'src/app/interfaces/home-tab';
import { POConfigPart, POConfigTemplate, VendorModel } from 'src/app/interfaces/models';
import { POWorklowConfigService } from 'src/app/services/poworklow-config.service';
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
  selectedVendor: OptionType;
  selectedParts: POConfigPart[];
  constructor(private poService: POWorklowConfigService, private vendorService: VendorService) {
    
  }

  private _filter(value: string): OptionType[] {
    if (typeof value == 'string') {
      const filterValue = value.toLowerCase();

      return this.getOptionsFromVendors(this.allVendors.filter(option => option.tVendorName.toString().toLowerCase().includes(filterValue)));
    }
    else
      return [];
  }

  getOptionsFromVendors(vendors: VendorModel[]): OptionType[] {
    let oArr: OptionType[] = [];
    for (var indx in vendors) {
      let tVendor = vendors[indx];
      oArr.push({
        optionDisplayName: tVendor.tVendorName,
        optionIndex: tVendor.aVendorId.toString(),
        optionOrder: 1
      });
    }
    return oArr;
  }

  getVendors() {
    this.vendorService.Get({ nBrandID: 1 }).subscribe((x: VendorModel[]) => {
      this.allVendors = x;
      this.filteredOptions = this.myControl.valueChanges.pipe(
        startWith(''),
        map(value => this._filter(value || '')),
      );
    });
  }

  getTemplate() {
    let userId = 1;
    if (this._nTemplateId > 0) {
      this.poService.GetTemplate(this._nTemplateId).subscribe((x: POConfigTemplate) => {
        this.curTemplate = x;
      });
    }
    else {
      this.curTemplate = {
        aPurchaseOrderTemplateID: 0,
        tTemplateName: "",
        nVendorID: parseInt(this.selectedVendor.optionIndex),
        nBrandID: this.nBrandId,
        purchaseOrderParts: []
      }
    }
  }

  onVendorChange(ev: any) {
    this.selectedVendor = ev;
  }

  goBack() {
    this.moveBack.emit(0);
  }  

  onKeydown(event: any) {
    if (event.key === "Enter") {
    }
  }

  canSave() {
    let can = true;
    if (this.curTemplate.purchaseOrderParts.length > 0 && this.curTemplate.tTemplateName.length > 0) {
      can = false;
    }
    return can;
  }

  submit() {
    this.poService.CreateUpdateTemplate(this.curTemplate).subscribe((x: POConfigTemplate) => {
      alert("Saved succesfully");
      this.moveBack.emit(1);
    });
  }
}
