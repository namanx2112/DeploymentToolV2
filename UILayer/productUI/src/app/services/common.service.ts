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
import { AES } from 'crypto-js';
import { sha256, sha224 } from 'js-sha256';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  static dropdownCache: any;
  static ddMonthString: string = "[Day/Month]";
  static allBrands: any;
  static pKey = [100, 101, 112, 108, 117, 116, 105, 111, 110];
  noNeedBlankDropDown: string[] = ["UserRole", "Brand"];
  constructor(private cacheService: CacheService, private ddService: DropdownServiceService) {

  }

  static ConfigUrl: string = "./api/";

  static isValidEmail(email: string) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  static generateGreetings() {
    let currentHour = parseInt(moment().format("HH"));
    if (currentHour >= 3 && currentHour < 12) {
      return "Good Morning";
    } else if (currentHour >= 12 && currentHour < 15) {
      return "Good Afternoon";
    } else if (currentHour >= 15 && currentHour < 22) {
      return "Good Evening";
    } else if (currentHour >= 22 || currentHour < 3) {
      return "Good Night";
    } else {
      return "Hello"
    }
  }

  static getKey() {
    return new TextDecoder().decode(new Uint8Array(CommonService.pKey)).toString()
  }

  static getHashed(strVal: string) {
    return sha256.hmac(CommonService.getKey(), strVal);
  }

  static encrypt(msg: string) {
    return AES.encrypt(msg, CommonService.getKey()).toString();
  }

  static decrypt(msg: string) {
    return AES.decrypt(msg, CommonService.getKey()).toString();
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

  static getFormatedDateString(dString: any, withTime: boolean = false) {
    let format = "MM/DD/YYYY";
    if (withTime)
      return this.getFormatedDateTimeString(dString);
    let rString = "";
    if (typeof dString == 'string') {
      if (dString != "") {
        let momentVal = (dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD hh:mm') : moment(dString, 'DD/MM/YYYY hh:mm');
        rString = momentVal.format(format);
      }
    }
    else if (dString != null)
      rString = moment(dString).format(format);
    return rString;
  }

  static getFormatedDateTimeString(dString: any) {
    let format = "MM/DD/YYYY HH:mm"
    let rString = "";
    if (typeof dString == 'string') {
      if (dString != "") {
        let momentVal = (dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD hh:mm').utcOffset('-0500') : moment(dString, 'DD/MM/YYYY hh:mm').utcOffset('-0500');
        rString = momentVal.format(format);
      }
    }
    else if (dString != null)
      rString = moment(dString).utcOffset('-0500').format(format);
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
      case ProjectTypes.InteriorMenuInstallation:
        pString = "Interior Menu Installation";
        break;
      case ProjectTypes.ExteriorMenuInstallation:
        pString = "Exterior Menu Installation";
        break;
      case ProjectTypes.PaymentTerminalInstallation:
        pString = "Payment Terminal Installation";
        break;
      case ProjectTypes.PartsReplacement:
        pString = "Parts Replacement";
        break;
      case ProjectTypes.ServerHandheldInstallation:
        pString = "Server Handheld Installation";
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

  static getTechComponentOptions() {
    return [{
      tDropdownText: "POS",
      aDropdownId: "0",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Audio",
      aDropdownId: "1",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Exterior Menus",
      aDropdownId: "2",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }];
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
      tDropdownText: "InteriorMenuInstallation",
      aDropdownId: ProjectTypes.InteriorMenuInstallation.toString(),
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
    }, {
      tDropdownText: "ServerHandheldInstallation",
      aDropdownId: ProjectTypes.ServerHandheldInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "ExteriorMenuInstallation",
      aDropdownId: ProjectTypes.ExteriorMenuInstallation.toString(),
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }];;
  }

  static getUserAccessTypes() {
    return [{
      tDropdownText: "Login",
      aDropdownId: "0",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Alerts",
      aDropdownId: "1",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }, {
      tDropdownText: "Both Login and Alerts",
      aDropdownId: "2",
      optionOrder: 1,
      bDeleted: false,
      nFunction: 0
    }];
  }

  public GetDropdownOptions(nBrandId: number, columnName?: string): OptionType[] {
    let ddItems: OptionType[] = [];
    if (columnName) {
      if (columnName == "ProjectType") {
        ddItems = CommonService.getProjectTypeOptions();
      }
      else if (columnName == "UserAccessTypes") {
        ddItems = CommonService.getUserAccessTypes();
      }
      else {
        if (columnName == "Franchise" || columnName == "Vendor" || columnName == "User")// Since Franchise and Vendor is shared
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
