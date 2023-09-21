import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { DashboardTileType, ProjectHighlights } from 'src/app/interfaces/commons';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { AccessService } from 'src/app/services/access.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-brand-dashboard',
  templateUrl: './brand-dashboard.component.html',
  styleUrls: ['./brand-dashboard.component.css']
})
export class BrandDashboardComponent {
  @Input()
  curBrand: BrandModel;
  projects: ProjectHighlights[];
  @Output() SearchedResult = new EventEmitter<string>();
  @Output() ChangeView = new EventEmitter<string>();
  constructor(private service: ExStoreService, public access: AccessService) {
    this.getProjectHoghlights();
  }

  ngOnInit() {
  }

  chartClicked(label: string) {
    this.ChangeView.emit("viewreport");
  }

  filterNotChart(part: ProjectHighlights) {
    return (part.type != DashboardTileType.Chart);
  }

  filterChart(part: ProjectHighlights) {
    return (part.type == DashboardTileType.Chart);
  }

  filterClick(filter: string) {}

  viewReports(ev: any, report: ProjectHighlights) {
    if (ev.target.className.indexOf("tMenu") == -1)
      this.ChangeView.emit("viewreport");
  }


  getProjectHoghlights() {
    this.service.GetProjecthighlights().subscribe((resp: ProjectHighlights[]) => {
      this.projects = resp;
    });
  }

  showReport(cur: ProjectHighlights) {
    this.ChangeView.emit("viewreport");
  }

  storeSelect(evt: any) {
    this.SearchedResult.emit(evt);
  }
}
