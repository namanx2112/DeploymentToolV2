import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportFolder } from 'src/app/interfaces/report-generator';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';
import { ShareDialogeComponent } from '../share-dialoge/share-dialoge.component';

@Component({
  selector: 'app-report-generator-home',
  templateUrl: './report-generator-home.component.html',
  styleUrls: ['./report-generator-home.component.css']
})
export class ReportGeneratorHomeComponent {

  @Input()
  set request(val: any) {
    this.curBrand = val.curBrand;
  }
  selectedTab: string;
  curBrand: BrandModel;
  curModel: any;
  showView: string = "home";
  titles: any[] = [{ view: "home", title: "Home" }];
  reportRequest: any;
  selectedReports: number[] = [];
  constructor(private rgService: ReportGeneratorService, private dialog: MatDialog) {
    this.selectedTab = "home";
  }
  tabClick(item: string) {
    this.selectedTab = item;
  }

  menuClick(item: string) {
    switch (item) {
      case "newfolder":
        this.editItem(
          {
            report: false,
            item: {
              nBrandId: this.curBrand.aBrandId,
              aFolderId: 0,
              tFolderName: "",
              tFolderDescription: ""
            }
          });
        break;
      case "newreport":
        this.editItem(
          {
            report: true,
            item: {
              nBrandId: this.curBrand.aBrandId,
              conditions: [],
              nFolderId: -1,
              aReportId: 0,
              tReportName: "",
              tReportDescription: "",
              isValid: false
            }
          });
        break;
    }
  }

  goToHome() {
    while (this.showView != "home") {
      this.titles.pop();
      this.showView = this.titles[this.titles.length - 1].view;
    }
    this.selectedReports = [];
  }

  folderListAction(req: any) {
    if (req.action == "editreport") {
      this.editItem(req.item);
    }
    else if (req.action == "openreport") {
      this.reportRequest = {
        nBrandId: this.curBrand.aBrandId,
        fromView: "report",
        request: {
          reportId: req.item.aReportId
        }
      }
      this.titles.push({ view: "showreport", title: req.item.tReportName });
      this.showView = "showreport";
    }
    else if (req.action == "reportselect") {
      this.selectedReports = req.selectedReports;
    }
  }

  openShare() {
    let cthis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.height = '60%';
    dialogConfig.data = {
      selectedBrand: [this.curBrand],
      reportIds: this.selectedReports,
      afterClose: function (data: any) {
        dialogRef.close();
      },
      onClose: function (ev: any) {
        dialogRef.close();
      },
      themeClass: "grayWhite",
      dialogTheme: "lightGrayWhiteTheme"
    };
    dialogRef = cthis.dialog.open(ShareDialogeComponent, dialogConfig);
  }

  showStore(eve: any) {
  }

  editItem(req: any) {
    if (req.report == false) {
      this.curModel = req.item;
      if (this.showView != "editfolder") {
        this.goToHome();
        this.showView = "editfolder";
        if (req.aFolderId > 0)
          this.titles.push({ view: "editfolder", title: "Edit Folder" });
        else
          this.titles.push({ view: "editfolder", title: "New Folder" });
      }
    }
    else {
      this.curModel = req.item;
      if (this.showView != "editreport") {
        var lauchReportView = function (cThis: any) {
          cThis.goToHome();
          cThis.showView = "editreport";
          if (cThis.curModel.aReportId > 0){
            cThis.selectedReports = [cThis.curModel.aReportId];
            cThis.titles.push({ view: "editreport", title: "Edit Report" });
          }
          else
            cThis.titles.push({ view: "editreport", title: "New Report" });
        }
        if (this.curModel.aReportId > 0) {
          this.rgService.GetReportDetails(this.curModel.aReportId).subscribe(x => {
            this.curModel = x;
            lauchReportView(this);
          });
        }
        else {
          lauchReportView(this);
        }
      }
    }
  }

  actionPerformed(ev: any) {
    this.titles.pop();
    this.showView = this.titles[this.titles.length - 1].view;
    this.selectedReports = [];
  }

  moveView(view: string) { }

}
