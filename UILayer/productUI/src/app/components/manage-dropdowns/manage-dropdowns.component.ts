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
  constructor(private dialog: MatDialog, private service: DropdownServiceService, private commonService: CommonService) {
    this.loadBrands();
  }

  loadModules() {
    this.service.GetModules(1).subscribe((x: DropdownModule[]) => {
      this.loadGroup(x);
      this.selectedModule = x[0];
      this.getLis();
    });
  }

  loadGroup(all: DropdownModule[]) {
    this.moduleGroupList = {};
    this.modules = [];
    for (var indxi in all) {
      let tModule = all[indxi];
      if (this.moduleGroupList[tModule.tModuleGroup])
        this.moduleGroupList[tModule.tModuleGroup].push(tModule);
      else {
        this.modules.push(tModule.tModuleGroup);
        this.moduleGroupList[tModule.tModuleGroup] = [tModule];
      }
    }
  }

  loadBrands() {
    let cThis = this;
    this.commonService.getBrands(function (x: any) {
      cThis.allBrands = x;
      cThis.selectedBrand = cThis.allBrands[0];
      cThis.loadModules();
    });
  }

  moduleChanged(ev: any) {
    this.getLis();
  }

  getLis() {
    this.ddList = [];
    this.service.Get(this.selectedModule.tModuleName).subscribe((resp: DropwDown[]) => {
      this.ddList = (resp.length > 0) ? resp.filter(x => x.bDeleted != true) : [];
    });
  }

  add(event: any): void {
    if (event.key === "Enter") {
      const value = (event.currentTarget.value || '').trim();
      // Add our fruit
      if (value) {
        let tItem = {
          aDropdownId: -1,
          nBrandId: this.selectedBrand.aBrandId,
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
            this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList);
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
        this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList);
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
      this.commonService.refreshDropdownValue(this.selectedModule.tModuleName, this.ddList);
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
              cThis.commonService.refreshDropdownValue(cThis.selectedModule.tModuleName, cThis.ddList);
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
