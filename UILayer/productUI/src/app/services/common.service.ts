import { Injectable } from '@angular/core';
import { Fields, OptionType } from '../interfaces/home-tab';
import { DropdownServiceService } from './dropdown-service.service';
import { BrandModel, DropwDown } from '../interfaces/models';
import { ProjectTypes } from '../interfaces/sonic';
import moment from 'moment';
import { BrandServiceService } from './brand-service.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  static allItems: any;
  static ddMonthString: string = "[Day/Month]";
  static allBrands: any;
  noNeedBlankDropDown: string[] = ["UserRole", "Brand"];
  constructor(private cacheService: CacheService, private ddService: DropdownServiceService) {

  }

  static ConfigUrl: string = "./api/";

  static isValidEmail(email: string) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  getBrands(callBack: any) {
    this.cacheService.getBrands(callBack);
  }

  loadDropdownArray(x: DropwDown[]) {
    CommonService.allItems = {};
    for (let indx in x) {
      let tItem = x[indx];
      if (CommonService.allItems[tItem.tModuleName])
        CommonService.allItems[tItem.tModuleName].push(tItem);
      else {
        CommonService.allItems[tItem.tModuleName] = [];
        CommonService.allItems[tItem.tModuleName].push(tItem);
      }
    }
  }

  public getAllDropdowns(callBack?: any) {
    if (typeof CommonService.allItems == 'undefined' || CommonService.allItems.length == 0) {
      let cThis = this;
      this.ddService.Get("").subscribe((x: DropwDown[]) => {
        cThis.loadDropdownArray(x);
        if (typeof callBack != 'undefined')
          callBack();
      });
    }
    else if (typeof callBack != 'undefined')
      callBack();
  }

  public refreshDropdownValue(module: string, ddVal: DropwDown[]) {
    CommonService.allItems[module] = ddVal;
  }

  static getFormatedDateString(dString: any) {
    let rString = "";
    if (typeof dString == 'string') {
      if (dString != "") {
        let momentVal = (dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD') : moment(dString, 'DD/MM/YYYY');
        rString = momentVal.format('MM/DD/YYYY');
      }
    }
    else if (dString != null)
      rString = moment(dString).format('MM/DD/YYYY');
    return rString;
  }

  static getFormatedDateStringForDB(dString: any) {
    let rString = "";
    if (typeof dString == 'string') {
      if (dString != "") {
        let momentVal = moment(dString, 'MM/DD/YYYY')
        rString = momentVal.format('YYYY-MM-DD');
      }
    }
    else if (dString != null)
      rString = moment(dString).format('YYYY-MM-DD');
    return rString;
  }

  static getProjectName(projectType: number) {
    let pString = "";
    let pType = projectType;
    switch (pType) {
      case ProjectTypes.New:
        pString = "New";
        break;
      case ProjectTypes.Rebuild:
        pString = "Rebuild";
        break;
      case ProjectTypes.Remodel:
        pString = "Remodel";
        break;
      case ProjectTypes.Relocation:
        pString = "Relocation";
        break;
      case ProjectTypes.Acquisition:
        pString = "Acquisition";
        break;
      case ProjectTypes.POSInstallation:
        pString = "POS Installation";
        break;
      case ProjectTypes.AudioInstallation:
        pString = "Audio Installation";
        break;
      case ProjectTypes.MenuInstallation:
        pString = "Menu Installation";
        break;
      case ProjectTypes.PaymentTerminalInstallation:
        pString = "Payment Terminal Installation";
        break;
      case ProjectTypes.PartsReplacement:
        pString = "Parts Replacement";
        break;

    }
    return pString;
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
  compare(a: OptionType, b: OptionType) {
    if (a.optionOrder < b.optionOrder) {
      return -1;
    }
    if (a.optionOrder > b.optionOrder) {
      return 1;
    }
    return 0;
  }

  GetDropdown(columnName: string): OptionType[] {
    let ddItems: OptionType[] = [];
    if (columnName == "ProjectType") {
      //  , , , , , , , 
      ddItems = [{
        optionDisplayName: "New",
        optionIndex: ProjectTypes.New.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "Rebuild",
        optionIndex: ProjectTypes.Rebuild.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "Remodel",
        optionIndex: ProjectTypes.Remodel.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "Relocation",
        optionIndex: ProjectTypes.Relocation.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "Acquisition",
        optionIndex: ProjectTypes.Acquisition.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "POSInstallation",
        optionIndex: ProjectTypes.POSInstallation.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "AudioInstallation",
        optionIndex: ProjectTypes.AudioInstallation.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "MenuInstallation",
        optionIndex: ProjectTypes.MenuInstallation.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "PaymentTerminalInstallation",
        optionIndex: ProjectTypes.PaymentTerminalInstallation.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }, {
        optionDisplayName: "PartsReplacement",
        optionIndex: ProjectTypes.PartsReplacement.toString(),
        optionOrder: 1,
        bDeleted: false,
        nFunction: 0
      }];
    }
    else {
      if (CommonService.allItems[columnName]) {
        var tItem = CommonService.allItems[columnName];
        for (var item in tItem) {
          ddItems.push({
            optionDisplayName: tItem[item].tDropdownText,
            optionIndex: tItem[item].aDropdownId.toString(),
            optionOrder: tItem[item].nOrder,
            bDeleted: tItem[item].bDeleted,
            nFunction: tItem[item].nFunction
          });
        }
        ddItems.sort(this.compare);
        if (this.noNeedBlankDropDown.indexOf(columnName) == -1)
          ddItems.unshift({
            optionDisplayName: "",
            optionIndex: "0",
            optionOrder: 1,
            bDeleted: false,
            nFunction: 0
          });
        else {
          if (columnName == "UserRole")
            ddItems = ddItems.filter(x => x.optionDisplayName.indexOf("Vendor") == -1 && x.optionDisplayName.indexOf("Franchise") == -1)
        }
      }
    }
    if (ddItems.length == 0) {
      if (columnName == "YesNo") {
        ddItems = [{
          optionDisplayName: "No",
          optionIndex: "0",
          optionOrder: 1,
          bDeleted: false,
          nFunction: 0
        },
        {
          optionDisplayName: "Yes",
          optionIndex: "1",
          optionOrder: 2,
          bDeleted: false,
          nFunction: 0
        }];
      }
      else {
        ddItems = [{
          optionDisplayName: "None",
          optionIndex: "0",
          optionOrder: 1,
          bDeleted: false,
          nFunction: 0
        }, {
          optionDisplayName: "Dummy",
          optionIndex: "1",
          optionOrder: 1,
          bDeleted: false,
          nFunction: 0
        }];
      }
    }
    return ddItems;
  }

  static GetDropdownDMonthFieldName(field: Fields) {
    let tPrefix = "dDateFor_";
    return tPrefix + field.fieldUniqeName;
  }

  static GetDropDownValueFromControl(curControl: Fields, optVal: string, _controlValues: any) {
    let outputVal = optVal;
    let tOpton = curControl.options?.find(x => x.optionIndex == optVal);
    if (tOpton) {
      outputVal = CommonService.GetDropDownValueFromControlOption(curControl, tOpton, _controlValues);
    }
    return outputVal;
  }

  static GetDropDownValueFromControlOption(curControl: Fields, opt: OptionType, _controlValues: any) {
    let val = opt.optionDisplayName;
    if (opt.nFunction == 1) {
      let newVal = val;
      if (_controlValues[curControl.fieldUniqeName] && _controlValues[curControl.fieldUniqeName] == opt.optionIndex) {
        let dString = _controlValues[CommonService.GetDropdownDMonthFieldName(curControl)];
        if (dString) {
          let momentVal = (typeof dString == 'object') ? moment(dString) : (dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD') : moment(dString, 'DD/MM/YYYY');
          newVal = "[" + momentVal.format('DD/MMM') + "]";
        }
      }
      else {
        let tDate = moment(new Date());
        newVal = "[" + tDate.format('DD/MMM') + "]";
      }
      val = val.replace("[Day/Month]", newVal)
    }
    return val;
  }

  static GetDateMonthForDropdown(dString: any, curArr: any) {
    let momentVal = (typeof dString == 'object') ? moment(dString) : (dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD') : moment(dString, 'DD/MM/YYYY');
    return "[" + momentVal.format('DD/MMM') + "]";
  }

  GetCKEditorConfig(height: string) {
    return {
      allowedContent: true,
      height: height,
      width: '100%',
      removePlugins: 'resize, elementspath',
      removeButtons: 'Source,JustifyCenter',
      toolbar: [
        // { name: 'document', groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates' ] },
        // { name: 'clipboard', groups: [ 'clipboard', 'undo' ], items: [ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo' ] },
        // { name: 'editing', groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', '-', 'SelectAll', '-', 'Scayt' ] },
        // { name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
        // '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
        {
          name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
            '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language']
        },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        // { name: 'insert', items: [ 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe' ] },
        // '/',
        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        // { name: 'tools', items: [ 'Maximize', 'ShowBlocks' ] },
        // { name: 'others', items: [ '-' ] },
        // { name: 'about', items: [ 'About' ] }
      ]
    }
  }
}
