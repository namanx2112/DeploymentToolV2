import { Injectable } from '@angular/core';
import { Fields, OptionType } from '../interfaces/home-tab';
import { DropdownServiceService } from './dropdown-service.service';
import { BrandModel, DropwDown } from '../interfaces/models';
import { ProjectTypes } from '../interfaces/store';
import moment from 'moment';
import { BrandServiceService } from './brand-service.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  static dropdownCache: any;
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
    CommonService.dropdownCache = {};
    for (let indx in x) {
      let tItem = x[indx];
      let cItem = [];
      if (typeof CommonService.dropdownCache[tItem.nBrandId] == 'undefined')
        CommonService.dropdownCache[tItem.nBrandId] = [];
      cItem = CommonService.dropdownCache[tItem.nBrandId];
      if (cItem[tItem.tModuleName])
        cItem[tItem.tModuleName].push(tItem);
      else {
        cItem[tItem.tModuleName] = [];
        cItem[tItem.tModuleName].push(tItem);
      }
    }
  }

  public getAllDropdowns(callBack?: any) {
    if (typeof CommonService.dropdownCache == 'undefined' || CommonService.dropdownCache.length == 0) {
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

  public refreshDropdownValue(module: string, ddVal: DropwDown[], nBrandId: number) {
    CommonService.dropdownCache[nBrandId][module] = ddVal;
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
  //       tDropdownText: "India",
  //       aDropdownId: "India",
  //       optionOrder: 1
  //     },
  //     {
  //       tDropdownText: "USA",
  //       aDropdownId: "USA",
  //       optionOrder: 2
  //     },
  //     {
  //       tDropdownText: "UAE",
  //       aDropdownId: "UAE",
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

  compareByText(a: OptionType, b: OptionType) {
    if (a.tDropdownText.toLocaleLowerCase() < b.tDropdownText.toLocaleLowerCase()) {
      return -1;
    }
    if (a.tDropdownText.toLocaleLowerCase() > b.tDropdownText.toLocaleLowerCase()) {
      return 1;
    }
    return 0;
  }

  static getProjectTypeOptions() {
    return [{
      tDropdownText: "New",
      aDropdownId: ProjectTypes.New.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Rebuild",
      aDropdownId: ProjectTypes.Rebuild.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Remodel",
      aDropdownId: ProjectTypes.Remodel.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Relocation",
      aDropdownId: ProjectTypes.Relocation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Acquisition",
      aDropdownId: ProjectTypes.Acquisition.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "POSInstallation",
      aDropdownId: ProjectTypes.POSInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "AudioInstallation",
      aDropdownId: ProjectTypes.AudioInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "MenuInstallation",
      aDropdownId: ProjectTypes.MenuInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "PaymentTerminalInstallation",
      aDropdownId: ProjectTypes.PaymentTerminalInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "PartsReplacement",
      aDropdownId: ProjectTypes.PartsReplacement.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }];;
  }

  public GetDropdownOptions(nBrandId: number, columnName?: string): OptionType[] {
    let ddItems: OptionType[] = [];
    if (columnName) {
      if (columnName == "ProjectType") {
        ddItems = CommonService.getProjectTypeOptions();
      }
      else {
        if (columnName == "Franchise" || columnName == "Vendor")// Since Franchise and Vendor is shared
          nBrandId = 0;
        if (CommonService.dropdownCache[nBrandId][columnName]) {
          var tItem = CommonService.dropdownCache[nBrandId][columnName];
          for (var item in tItem) {
            ddItems.push({
              tDropdownText: tItem[item].tDropdownText,
              aDropdownId: tItem[item].aDropdownId.toString(),
              optionOrder: tItem[item].nOrder,
              bDeleted: tItem[item].bDeleted,
              nFunction: tItem[item].nFunction
            });
          }
          if (columnName == "Franchise" || columnName == "Vendor")
            ddItems.sort(this.compareByText);
          else
            ddItems.sort(this.compare);
          if (this.noNeedBlankDropDown.indexOf(columnName) == -1)
            ddItems.unshift({
              tDropdownText: "",
              aDropdownId: "0",
              optionOrder: 1,
              bDeleted: false,
              nFunction: 0
            });
          else {
            if (columnName == "UserRole")
              ddItems = ddItems.filter(x => x.tDropdownText.indexOf("Vendor") == -1 && x.tDropdownText.indexOf("Franchise") == -1)
          }
        }
      }
      if (ddItems.length == 0) {
        if (columnName == "YesNo") {
          ddItems = [{
            tDropdownText: "No",
            aDropdownId: "0",
            optionOrder: 1,
            bDeleted: false,
            nFunction: 0
          },
          {
            tDropdownText: "Yes",
            aDropdownId: "1",
            optionOrder: 2,
            bDeleted: false,
            nFunction: 0
          }];
        }
        else {
          ddItems = CommonService.getDefaultOptions();
        }
      }
    }
    return ddItems;
  }

  static getDefaultOptions(): OptionType[] {
    return [{
      tDropdownText: "None",
      aDropdownId: "0",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Dummy",
      aDropdownId: "1",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }];
  }

  GetDropdown(columnName: string): string {
    return columnName;
  }

  static GetDropdownDMonthFieldName(field: Fields) {
    let tPrefix = "dDateFor_";
    return tPrefix + field.fieldUniqeName;
  }

  static GetDropDownValueFromControl(curControl: Fields, optVal: string, _controlValues: any, nBrandId: number) {
    let outputVal = optVal;
    if (curControl.options) {
      if (curControl.options == "ProjectType") {
        outputVal = this.getProjectTypeOptions().filter(x => x.aDropdownId == optVal)[0].tDropdownText;
      }
      else {
        if (curControl.options == "Vendor" || curControl.options == "Franchise")
          nBrandId = 0;
        let opArr: OptionType[] = CommonService.dropdownCache[nBrandId][curControl.options];
        if (typeof opArr == 'undefined')
          opArr = CommonService.getDefaultOptions();
        if (opArr) {
          let tOpton = opArr.find(x => x.aDropdownId == optVal);
          if (tOpton) {
            outputVal = CommonService.GetDropDownValueFromControlOption(curControl, tOpton, _controlValues);
          }
        }
      }
    }
    return outputVal;
  }

  static GetDropDownValueFromControlOption(curControl: Fields, opt: OptionType, _controlValues: any) {
    let val = opt.tDropdownText;
    if (opt.nFunction == 1) {
      let newVal = val;
      if (_controlValues[curControl.fieldUniqeName] && _controlValues[curControl.fieldUniqeName] == opt.aDropdownId) {
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
