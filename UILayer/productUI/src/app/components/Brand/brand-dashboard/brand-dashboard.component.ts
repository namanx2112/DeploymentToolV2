import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { DashboardTileType, DahboardTile } from 'src/app/interfaces/commons';
import { OptionType } from 'src/app/interfaces/home-tab';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AccessService } from 'src/app/services/access.service';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-brand-dashboard',
  templateUrl: './brand-dashboard.component.html',
  styleUrls: ['./brand-dashboard.component.css']
})
export class BrandDashboardComponent {
  @Input()
  set curBrand(val: BrandModel) {
    this._curBrand = val;
    this.getProjectHoghlights();
  };
  _curBrand: BrandModel;
  projects: DahboardTile[];
  @Output() SearchedResult = new EventEmitter<string>();
  @Output() ChangeView = new EventEmitter<string>();
  selectedProjects: any[] = [];
  allProjectTypes: OptionType[] = [];
  constructor(private service: ExStoreService, public access: AccessService, private analyticsService: AnalyticsService, private commonService: CommonService) {
    this.loadProjectTYpes();
  }

  loadProjectTYpes() {
    this.allProjectTypes = this.commonService.GetDropdownOptions(-1, "ProjectType");
  }

  ngOnInit() {
  }

  chartClicked(label: string) {
    this.ChangeView.emit("viewreport");
  }

  filterNotChart(part: DahboardTile) {
    return (part.type != DashboardTileType.Chart);
  }

  filterChart(part: DahboardTile) {
    return (part.type == DashboardTileType.Chart);
  }

  filterClick(filter: string) { }

  viewReports(ev: any, report: DahboardTile) {
    if (ev.target.className.indexOf("tMenu") == -1)
      this.ChangeView.emit("viewreport");
  }

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
    this.analyticsService.GetDashboards(this._curBrand.aBrandId, tProjTypes).subscribe((resp: DahboardTile[]) => {
      this.projects = resp;
    });
  }

  showReport(cur: DahboardTile) {
    this.ChangeView.emit("viewreport");
  }

  storeSelect(evt: any) {
    this.SearchedResult.emit(evt);
  }
}
