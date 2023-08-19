import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { SonicProjectHighlights } from 'src/app/interfaces/commons';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { AccessService } from 'src/app/services/access.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-brand-dashboard',
  templateUrl: './brand-dashboard.component.html',
  styleUrls: ['./brand-dashboard.component.css']
})
export class BrandDashboardComponent {
  @Input()
  curBrand: BrandModel;
  projects: SonicProjectHighlights[];
  @Output() SearchedResult = new EventEmitter<string>();
  @Output() ChangeView = new EventEmitter<string>();
  constructor(private service: SonicService, public access: AccessService) {
    this.getProjectHoghlights();
  }

  ngOnInit() {
  }


  getProjectHoghlights() {
    this.service.GetProjecthighlights().subscribe((resp: SonicProjectHighlights[]) => {
      this.projects = resp;
    });
  }

  showReport(cur: SonicProjectHighlights) {
    this.ChangeView.emit("viewreport");
  }

  storeSelect(evt: any) {
    this.SearchedResult.emit(evt);
  }
}
