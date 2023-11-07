import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import moment from 'moment';
import { Observable, map, startWith } from 'rxjs';
import { DashboardTileType, DahboardTile } from 'src/app/interfaces/commons';
import { OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AccessService } from 'src/app/services/access.service';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  selector: 'app-dashboard-pane',
  templateUrl: './dashboard-pane.component.html',
  styleUrls: ['./dashboard-pane.component.css']
})
export class DashboardPaneComponent {
  @Input()
  set request(val: any) {
    this._curBrand = val.curBrand;
    this._needSearch = val.needSearch;
    this.loadProjectTYpes();
    this.getProjectHoghlights();
  };
  _needSearch: boolean;
  _curBrand: BrandModel;
  projects: DahboardTile[];
  @Output() SearchedResult = new EventEmitter<string>();
  @Output() ChangeView = new EventEmitter<any>();
  selectedProjects: any[] = [];
  allProjectTypes: OptionType[] = [];
  campaignOne: FormGroup;
  constructor(private service: ExStoreService, public access: AccessService, private analyticsService: AnalyticsService, private commonService: CommonService) {    
    this.campaignOne = new FormGroup({
      start: new FormControl(new Date(new Date().getFullYear(), 0, 1)),
      end: new FormControl(new Date(new Date().getFullYear(), 11, 31)),
    });
  }

  getClassName(curItem: any, isChart: boolean) {
    let tClass = (isChart) ? 'X' : 'notChart X';
    tClass += curItem.size;
    return tClass;
  }

  loadProjectTYpes() {
    this.allProjectTypes = this.commonService.GetDropdownOptions(-1, "ProjectType");
    if (this._curBrand.tBrandName.toLocaleLowerCase().indexOf("sonic") > -1)
      this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") == -1)
    this.allProjectTypes.filter(x=> x.tDropdownText.toLocaleLowerCase() == "new")[0].tDropdownText = "New Store Opening (NRO)";
    // else
    //   this.allProjectTypes = this.allProjectTypes.filter(x => x.tDropdownText.toLocaleLowerCase().indexOf("server") > -1)
  }

  ngOnInit() {
  }

  dtChanged() {
    this.getProjectHoghlights();
  }

  chartClicked(request: DahboardTile) {
    if (request.count == 0) {
      alert("There is no record to display");
      return;
    }
    let tProjParam = (this.selectedProjects.length > 0) ? this.selectedProjects.join(",") : "";
    this.ChangeView.emit({ viewName: "viewreport", request: request, tParam: tProjParam, fromView: "dashboardpane" });
  }

  filterNotChart(part: DahboardTile) {
    return (part.type != DashboardTileType.Chart);
  }

  filterChart(part: DahboardTile) {
    return (part.type == DashboardTileType.Chart);
  }

  filterClick(filter: string) { }

  changeSelected(val: OptionType) {
    const index = this.selectedProjects.indexOf(val.aDropdownId);
    if (index >= 0) {
      this.selectedProjects.splice(index, 1);
    } else {
      this.selectedProjects.push(val.aDropdownId);
    }
    this.getProjectHoghlights();
  }


  getProjectHoghlights() {
    let tProjTypes = this.selectedProjects.join(",");
    let dtStart = (this.campaignOne.controls['start'].valid) ? this.campaignOne.controls['start'].value : null;
    let dtEnd = (this.campaignOne.controls['end'].valid) ? this.campaignOne.controls['end'].value : null;
    this.analyticsService.GetDashboards(this._curBrand.aBrandId, tProjTypes, dtStart, dtEnd).subscribe((resp: DahboardTile[]) => {
      this.projects = resp;
    });
  }
}
