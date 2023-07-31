import { Component } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { BrandModel, DropwDown } from 'src/app/interfaces/models';
import { DropdownServiceService } from 'src/app/services/dropdown-service.service';
import { MatChipEditedEvent, MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { BrandServiceService } from 'src/app/services/brand-service.service';

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
  selectedModule: string;
  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  constructor(private service: DropdownServiceService, private brandService: BrandServiceService) {
    this.loadBrands();
    this.moduleList = this.service.GetAllModules();
    this.getLis();
  }

  loadBrands() {
    this.brandService.Get(null).subscribe((x: BrandModel[]) => {
      this.allBrands = x;
      this.selectedBrand = this.allBrands[0];
    });
  }

  moduleChanged(ev: any) {
    this.getLis();
  }

  getLis() {
    this.service.Get(this.selectedModule).subscribe((resp: DropwDown[]) => {
      this.ddList = resp.filter(x=>x.bDeleted != true);
    });
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    // Add our fruit
    if (value) {
      let tItem = {
        aDropdownId: -1,
        nBrandId: this.selectedBrand.aBrandId,
        tModuleName: this.selectedModule,
        tDropdownText: value,
        bDeleted: false
      };
      this.service.Create(tItem).subscribe((x: number) => {
        tItem.aDropdownId = x;
        this.ddList.push(tItem);
      });

    }

    // Clear the input value
    event.chipInput!.clear();
  }

  remove(item: DropwDown): void {

    if (item.aDropdownId >= 0) {
      this.service.Delete(item.aDropdownId).subscribe((x: any) => {
        const index = this.ddList.indexOf(item);

        if (index >= 0) {
          this.ddList.splice(index, 1);
        }
      });
    }
  }

  edit(item: DropwDown, event: MatChipEditedEvent) {
    if (item.aDropdownId >= 0) {
      item.tDropdownText = event.value;
      this.service.Update(item).subscribe((x: any) => {
        const index = this.ddList.indexOf(item);

        if (index >= 0) {
          this.ddList[index] = item;
        }
      });
    }
  }
}
