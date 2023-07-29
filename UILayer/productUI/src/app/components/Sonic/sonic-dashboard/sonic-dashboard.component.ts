import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { SonicProjectHighlights } from 'src/app/interfaces/commons';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { AccessService } from 'src/app/services/access.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-sonic-dashboard',
  templateUrl: './sonic-dashboard.component.html',
  styleUrls: ['./sonic-dashboard.component.css']
})
export class SonicDashboardComponent {
  projects: SonicProjectHighlights[];
  @Output() SearchedResult = new EventEmitter<string>();
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

  storeSelect(evt: any) {
    this.SearchedResult.emit(evt);
  }
}
