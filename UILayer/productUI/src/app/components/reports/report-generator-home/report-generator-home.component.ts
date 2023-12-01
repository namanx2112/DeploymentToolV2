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
  curFolder: ReportFolder;
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
        this.editReport({
          aFolderId: 0,
          tFolderName: "",
          tFolderDescription: ""
        });
        break;
    }
  }

  editReport(req: any) {
    this.curFolder = req;
    this.showView = "editfolder";
    if (req.aFolderId > 0)
      this.titles.push("Edit Report");
    else
      this.titles.push("New Report");
  }

  actionPerformedFolder(ev: any) {
    this.showView = "home";
    this.titles.pop();
  }

  moveView(view: string) { }

}
