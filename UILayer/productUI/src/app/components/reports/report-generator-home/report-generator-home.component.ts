import { Component, Input } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportFolder } from 'src/app/interfaces/report-generator';

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
  titles: string[] = ["Home"];
  constructor() {
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
              aFolderId: 0,
              tFolderName: "",
              tFolderDescription: ""
            }
          });
        break;
      case "newfolder":
        this.editItem(
          {
            report: true,
            item: {
              aReportId: 0,
              tReportName: "",
              tReportDescription: ""
            }
          });
        break;
    }
  }

  editItem(req: any) {
    if (req.report == false) {
      this.curModel = req.item;
      if (this.showView != "editfolder") {
        this.showView = "editfolder";
        if (req.aFolderId > 0)
          this.titles.push("Edit Folder");
        else
          this.titles.push("New Folder");
      }
    }
    else {
      this.curModel = req.item;
      if (this.showView != "editreport") {
        this.showView = "editreport";
        if (req.aFolderId > 0)
          this.titles.push("Edit Report");
        else
          this.titles.push("New Report");
      }
    }
  }

  actionPerformedFolder(ev: any) {
    this.showView = "home";
    this.titles.pop();
  }

  moveView(view: string) { }

}
