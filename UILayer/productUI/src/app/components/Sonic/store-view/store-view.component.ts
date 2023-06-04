import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Dictionary } from 'src/app/interfaces/commons';
import { HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { SonicService } from 'src/app/services/sonic.service';
import { ControlsComponent } from '../../controls/controls.component';
import { DialogControlsComponent } from '../../dialog-controls/dialog-controls.component';
import { NotesListComponent } from '../notes-list/notes-list.component';
import { StoreSearchModel } from 'src/app/interfaces/sonic';

@Component({
  selector: 'app-store-view',
  templateUrl: './store-view.component.html',
  styleUrls: ['./store-view.component.css']
})
export class StoreViewComponent {
  @Input()
  curStore: StoreSearchModel;
  allTabs: HomeTab[];
  tabForUI: HomeTab[];
  tValues: Dictionary<Dictionary<string>>;
  selectedTab: number;
  viewName: string;
  @Output() ChangeView = new EventEmitter<any>();
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

  changeTileView(tName: string) {
    this.viewName = tName;
  }

  ShowProject(view: string) {
    this.ChangeView.emit(view);
  }

  changeTab(tTab: HomeTab): HomeTab {
    if (tTab.tab_type == TabType.StoreContact) {
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

  ShowNotes() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';

    dialogConfig.data = {
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(NotesListComponent, dialogConfig);
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
        let clickedVal = data.value;
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = this.dialog.open(DialogControlsComponent, dialogConfig);
    // dialogRef.afterClosed().subscribe(result => {
    //   //console.log(`Dialog result: ${result}`);
    //   let t = result;
    // });
  }

  goBack() {
    this.ChangeView.emit("dashboard");
  }
}
