import { Injectable } from '@angular/core';
import { OptionType } from '../interfaces/home-tab';
import { DropdownServiceService } from './dropdown-service.service';
import { DropwDown } from '../interfaces/models';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  static allItems: DropwDown[];
  constructor(private ddService: DropdownServiceService) {

  }

  public getAllDropdowns() {
    if (typeof CommonService.allItems == 'undefined') {
      this.ddService.Get("").subscribe((x: DropwDown[]) => {
        CommonService.allItems = x;
      });
    }
  }

  // GetCountryDropdowns(): OptionType[] {
  //   let contries = [
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
  //   return contries;
  // }

  GetDropdown(columnName: string): OptionType[] {
    let ddItems: OptionType[] = [];
    for (var item in CommonService.allItems) {
      if (CommonService.allItems[item].tModuleName == columnName) {
        ddItems.push({
          optionDisplayName: CommonService.allItems[item].tDropdownText,
          optionIndex: CommonService.allItems[item].aDropdownId.toString(),
          optionOrder: 1
        });
      }
    }
    if (ddItems.length == 0) {
      ddItems = [{
        optionDisplayName: "Dummy",
        optionIndex: "1",
        optionOrder: 1
      }];
    }
    return ddItems;
  }
}
