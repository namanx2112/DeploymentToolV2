import { Injectable } from '@angular/core';
import { OptionType } from '../interfaces/home-tab';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

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
    let dropdownVal = [{
      optionDisplayName: "None",
      optionIndex: "1",
      optionOrder: 1
    }];
    if (columnName == "nDepartment" || columnName == "nRole") { 
      dropdownVal = [{
        optionDisplayName: "Test Val",
        optionIndex: "1",
        optionOrder: 1
      }];
    }
    else {
      dropdownVal = [
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
    }
    return dropdownVal;
  }
}
