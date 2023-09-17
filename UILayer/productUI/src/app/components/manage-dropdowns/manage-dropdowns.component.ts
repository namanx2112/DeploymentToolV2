import { Component } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { BrandModel, DropdownModule, DropwDown } from 'src/app/interfaces/models';
import { DropdownServiceService } from 'src/app/services/dropdown-service.service';
// import { MatChipEditedEvent, MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { Dictionary } from 'src/app/interfaces/commons';
import { CdkDragDrop, moveItemInArray, CdkDrag, CdkDropList } from '@angular/cdk/drag-drop';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { Validators } from '@angular/forms';
import { DialogControlsComponent } from '../dialog-controls/dialog-controls.component';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-manage-dropdowns',
  templateUrl: './manage-dropdowns.component.html',
  styleUrls: ['./manage-dropdowns.component.css']
})
export class ManageDropdownsComponent {
  allBrands: BrandModel[];
  selectedBrand: BrandModel;
  ddList: DropwDown[];
  moduleList: string[];
  selectedModule: DropdownModule;
  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  moduleGroupList: any;
  modules: string[];
  allModules: DropdownModule[];
  genBrand: BrandModel;
  constructor(private dialog: MatDialog, private service: DropdownServiceService, private commonService: CommonService) {
    this.genBrand = {
      aBrandId: 0,
      tBrandIdentifier: "",
      tBrandName: 'General',
      tBrandDomain: '',
      tBrandAddressLine1: '',
      tBrandAddressLine2: '',
      tBrandCity: '',
      nBrandState: 0,
      nBrandCountry: 0,
      tBrandZipCode: '',
      nBrandLogoAttachmentID: 0,
      nCreatedBy: 0,
      nUpdateBy: 0,
      dtCreatedOn: new Date(),
      dtUpdatedOn: new Date(),
      bDeleted: false,
      tIconURL: '',
      access: true,
      nEnabled: 0,
      tMyClass: ""
    };
    this.loadBrands();
  }

  loadModules() {
    this.service.GetModules(1).subscribe((x: DropdownModule[]) => {
      this.allModules = x;
      this.loadGroup();
    });
  }

  loadGroup() {
    this.moduleGroupList = {};
    this.modules = [];
    let first = true;
    this.selectedModule = {
      aModuleId: -1,
      nBrandId: 0,
      tModuleName: '',
      tModuleDisplayName: '',
      tModuleGroup: '',
      editable: false
    };
    for (var indxi in this.allModules) {
      let tModule = this.allModules[indxi];
      if (tModule.nBrandId == this.selectedBrand.aBrandId) {
        if (first)
          this.selectedModule = tModule;
        first = false;
        if (this.moduleGroupList[tModule.tModuleGroup])
          this.moduleGroupList[tModule.tModuleGroup].push(tModule);
        else {
          this.modules.push(tModule.tModuleGroup);
          this.moduleGroupList[tModule.tModuleGroup] = [tModule];
        }
      }
    }
    this.getLis();
  }

  loadBrands() {
    let cThis = this;
    this.commonService.getBrands(function (x: any) {
      cThis.allBrands = x;
      cThis.selectedBrand = cThis.genBrand;
      cThis.loadModules();
    });
  }

  moduleChanged(ev: any) {
    this.getLis();
  }

  getLis() {
    this.ddList = [];
    if (this.selectedModule.aModuleId > -1) {
      this.service.Get(this.selectedModule.tModuleName).subscribe((resp: DropwDown[]) => {
        this.ddList = (resp != null && resp.length > 0) ? resp.filter(x => x.bDeleted != true && x.nBrandId == this.selectedBrand.aBrandId) : [];
      });
    }
  }

  add(event: any): void {
    if (event.key === "Enter") {
      const value = (event.currentTarget.value || '').trim();
      // Add our fruit
      if (value) {
        let tItem = {
          aDropdownId: -1,
          nBrandId: this.selectedBrand.aBrandId,
          nModuleId: this.selectedModule.aModuleId,
          tModuleName: this.selectedModule.tModuleName,
          tDropdownText: value,
          bDeleted: false,
          nOrder: this.ddList.length,
          nFunction: (value.indexOf("[Day/Month]") > -1) ? 1 : 0
        };
        this.service.Create(tItem).subscribe((x: any) => {
          if (typeof x == 'string')
            alert(x);
          else {
            tItem.aDropdownId = x.aDropdownId;
            this.ddList.push(tItem);
            this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList, this.selectedBrand.aBrandId);
            event.target.value = "";
          }
        });
      }
    }

    // Clear the input value
    // event.chipInput!.clear();
  }

  remove(item: DropwDown): void {

    if (item.aDropdownId >= 0) {
      this.service.Delete(item.aDropdownId).subscribe((x: any) => {
        const index = this.ddList.indexOf(item);

        if (index >= 0) {
          this.ddList.splice(index, 1);
        }
        this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList, this.selectedBrand.aBrandId);
      });
    }
  }

  drop(event: any) {
    let movingItem = event;
    moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    for (let indx in this.ddList) {
      this.ddList[indx].nOrder = parseInt(indx);
    }
    this.service.UpdateOrder(this.ddList).subscribe(x => {
      this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList, this.selectedBrand.aBrandId);
    });
  }

  edit(item: DropwDown) {
    let cThis = this;
    if (item.aDropdownId >= 0) {
      this.loadEditDialog(item.tDropdownText, function (value: string, callBackClose: any) {
        item.tDropdownText = value;
        if (value.indexOf("[Day/Month]") > -1)
          item.nFunction = 1;
        else
          item.nFunction = 0;
        cThis.service.Update(item).subscribe((x: any) => {
          if (typeof x == 'string')
            alert(x);
          else {
            const index = cThis.ddList.indexOf(item);
            if (index >= 0) {
              cThis.ddList[index] = item;
              //item.tDropdownText = event.value;
              callBackClose();
              cThis.commonService.refreshDropdownValue(cThis.selectedModule.tModuleName, cThis.ddList, cThis.selectedBrand.aBrandId);
            }
          }
        });
      })
    }
  }

  loadEditDialog(txtValue: string, callBack: any) {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let fields: Fields[] = [{
      field_name: "Dropdown text",
      fieldUniqeName: "tDropdownText",
      field_type: FieldType.text,
      readOnly: false,
      field_placeholder: "Enter new dropdown item",
      invalid: false,
      validator: [Validators.required],
      mandatory: true,
      defaultVal: txtValue,
      hidden: false
    }];
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    let tVals = { tDropdownText: txtValue };
    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: "Change dropdown Item",
      fields: fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: tVals,
      SubmitLabel: "Save",
      curBrandId: this.selectedBrand.aBrandId,
      onSubmit: function (data: any) {
        let eText = data.value["tDropdownText"];
        if (eText.trim() == "")
          alert("Please enter a text to continue");
        else
          callBack(data.value["tDropdownText"], function () {
            dialogRef.close();
          });
      },
      onClose: function (ev: any) {
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
  }
}
