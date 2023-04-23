import { Component, EventEmitter, Output } from '@angular/core';
import { SonicProjectHighlights } from 'src/app/interfaces/commons';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-sonic-dashboard',
  templateUrl: './sonic-dashboard.component.html',
  styleUrls: ['./sonic-dashboard.component.css']
})
export class SonicDashboardComponent {
  projects: SonicProjectHighlights[];
  @Output() SearchedResult = new EventEmitter<string>();
  constructor(private service: SonicService) {
    this.getProjectHoghlights();
  }

  getProjectHoghlights() {
    this.service.GetProjecthighlights().subscribe((resp: SonicProjectHighlights[]) => {
      this.projects = resp;
    });
  }

  SearchStore(txt: any){
    this.SearchedResult.emit(txt);
  }
}
