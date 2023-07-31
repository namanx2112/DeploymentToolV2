import { Injectable } from '@angular/core';
import { OptionType } from '../interfaces/home-tab';
import { DropdownServiceService } from './dropdown-service.service';
import { DropwDown } from '../interfaces/models';
import { ProjectTypes } from '../interfaces/sonic';

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
    if (columnName == "ProjectType") {
      //  , , , , , , , 
      ddItems = [{
        optionDisplayName: "New",
        optionIndex: ProjectTypes.New.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "Rebuild",
        optionIndex: ProjectTypes.Rebuild.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "Remodel",
        optionIndex: ProjectTypes.Remodel.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "Relocation",
        optionIndex: ProjectTypes.Relocation.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "Acquisition",
        optionIndex: ProjectTypes.Acquisition.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "POSInstallation",
        optionIndex: ProjectTypes.POSInstallation.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "AudioInstallation",
        optionIndex: ProjectTypes.AudioInstallation.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "MenuInstallation",
        optionIndex: ProjectTypes.MenuInstallation.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "PaymentTerminalInstallation",
        optionIndex: ProjectTypes.PaymentTerminalInstallation.toString(),
        optionOrder: 1
      }, {
        optionDisplayName: "PartsReplacement",
        optionIndex: ProjectTypes.PartsReplacement.toString(),
        optionOrder: 1
      }];
    }
    else {
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
        if (columnName == "YesNo") {
          ddItems = [{
            optionDisplayName: "No",
            optionIndex: "0",
            optionOrder: 1
          },
          {
            optionDisplayName: "Yes",
            optionIndex: "1",
            optionOrder: 2
          }];
        }
        else {
          ddItems = [{
            optionDisplayName: "Dummy",
            optionIndex: "1",
            optionOrder: 1
          }];
        }
      }
    }
    return ddItems;
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
