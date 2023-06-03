import { Injectable } from '@angular/core';
import { OptionType } from '../interfaces/home-tab';
import { DropdownServiceService } from './dropdown-service.service';
import { DropwDown } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  allItems: DropwDown[];
  constructor(private ddService: DropdownServiceService) { }

  getAllDropdowns() {
    this.ddService.Get("").subscribe((x: DropwDown[]) => {
      this.allItems = x;
    });
  }

  GetCountryDropdowns(): OptionType[] {
    let contries = [
      {
        optionDisplayName: "India",
        optionIndex: "India",
        optionOrder: 1
      },
      {
        optionDisplayName: "USA",
        optionIndex: "USA",
        optionOrder: 2
      },
      {
        optionDisplayName: "UAE",
        optionIndex: "UAE",
        optionOrder: 3
      }
    ];
    return contries;
  }

  GetDropdown(columnName: string): OptionType[] {

    let ddItems: OptionType[] = [];
    for (var item in this.allItems) {
      if (this.allItems[item].tModuleName == columnName) {
        ddItems.push({
          optionDisplayName: this.allItems[item].tDropdownText,
          optionIndex: this.allItems[item].aDropdownId.toString(),
          optionOrder: 1
        });
      }
    }
    // let dropdownVal = [{
    //   optionDisplayName: "None",
    //   optionIndex: "1",
    //   optionOrder: 1
    // }];
    // if (columnName == "nDepartment" || columnName == "nRole") {
    //   dropdownVal = [{
    //     optionDisplayName: "Test Val",
    //     optionIndex: "1",
    //     optionOrder: 1
    //   }];
    // }
    // else {
    //   dropdownVal = [
    //     {
    //       optionDisplayName: "India",
    //       optionIndex: "India",
    //       optionOrder: 1
    //     },
    //     {
    //       optionDisplayName: "USA",
    //       optionIndex: "USA",
    //       optionOrder: 2
    //     },
    //     {
    //       optionDisplayName: "UAE",
    //       optionIndex: "UAE",
    //       optionOrder: 3
    //     }
    //   ];
    // }
    return ddItems;
  }
}
