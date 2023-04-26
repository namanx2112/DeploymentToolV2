import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';
import { ControlsComponent } from '../../controls/controls.component';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';

@Component({
  selector: 'app-store-view',
  templateUrl: './store-view.component.html',
  styleUrls: ['./store-view.component.css']
})
export class StoreViewComponent {
  allTabs: HomeTab[];
  tabForUI: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  selectedTab: number;
  storeName: string;
  viewName: string;
  constructor(private service: SonicService, private dialog: MatDialog) {
    this.initTab();
    this.viewName = "tabview";
  }

  initTab() {
    this.allTabs = this.service.GetStoretabs();
    this.tValues = {};
    this.tabForUI = [];
    for (var tIndx in this.allTabs) {
      let tTab = this.allTabs[tIndx];
      this.tValues[tTab.tab_name] = this.getValues(tTab);
      tTab = this.changeTab(tTab);
      if (parseInt(tIndx) > 2)
        this.tabForUI.push(tTab);
    }
    this.selectedTab = 3;
  }

  changeView(tName: string) {
    this.viewName = tName;
  }

  changeTab(tTab: HomeTab): HomeTab {
    if (tTab.tab_type == TabType.StoreContact) {
      this.storeName = this.tValues[tTab.tab_name]["tStoreName"];
      for (var indx in tTab.fields) {
        switch (tTab.fields[indx].fieldUniqeName) {
          case "tStoreAddressLine1":
          case "tStoreManager":
          case "tPOCPhone":
          case "tPOCEmail":
            tTab.fields[indx].hidden = false;
            break;
          default:
            tTab.fields[indx].hidden = true;
        }
      }
    }
    else if (tTab.tab_type == TabType.StoreConfiguration) {
      for (var indx in tTab.fields) {
      }
    }
    else if (tTab.tab_type == TabType.StoreStackHolder) {
      for (var indx in tTab.fields) {
        switch (tTab.fields[indx].fieldUniqeName) {
          case "nITPM":
          case "nCD":
            tTab.fields[indx].hidden = false;
            break;
          default:
            tTab.fields[indx].hidden = true;
        }
      }
    }
    return tTab;
  }

  getValues(tTab: HomeTab) {
    let values: any = {};
    for (var tIndx in tTab.fields) {
      values[tTab.fields[tIndx].fieldUniqeName] = "Dummy " + tTab.fields[tIndx].field_name;
    }
    return values;
  }

  onSubmit(tVal: any, tab: HomeTab) { }

  tabClick(tabIndex: number) {
    this.selectedTab = tabIndex;
  }

  editTab(cTab: HomeTab) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '80%';
    dialogConfig.width = '60%';

    dialogConfig.data = {
      numberOfControlsInARow: 1,
      title: cTab.tab_header,
      fields: cTab.fields,
      readOnlyForm: false,
      needButton: true,
      controlValues: this.tValues[cTab.tab_name],
      SubmitLabel: "Save",
      onSubmit: function (data: any) {
        let clickedVal = data.values;
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "grayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
    // dialogRef.afterClosed().subscribe(result => {
    //   //console.log(`Dialog result: ${result}`);
    //   let t = result;
    // });
  }
}
