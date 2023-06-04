import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { SonicProjectHighlights } from 'src/app/interfaces/commons';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-sonic-dashboard',
  templateUrl: './sonic-dashboard.component.html',
  styleUrls: ['./sonic-dashboard.component.css']
})
export class SonicDashboardComponent {
  projects: SonicProjectHighlights[];
  @Output() SearchedResult = new EventEmitter<string>();
  myControl = new FormControl('');
  ddOptions: StoreSearchModel[];
  filteredOptions: Observable<StoreSearchModel[]>;
  constructor(private service: SonicService) {
    this.getProjectHoghlights();
  }

  ngOnInit() {
    this.getAllStores();
  }

  private _filter(value: string): StoreSearchModel[] {
    if (typeof value == 'string') {
      const filterValue = value.toLowerCase();

      return this.ddOptions.filter(option => option.tProjectName.toString().toLowerCase().includes(filterValue) || option.tStoreName.toString().toLowerCase().includes(filterValue) ||
        option.tStoreNumber.toString().toLowerCase().includes(filterValue));
    }
    else
      return [];
  }

  getProjectHoghlights() {
    this.service.GetProjecthighlights().subscribe((resp: SonicProjectHighlights[]) => {
      this.projects = resp;
    });
  }

  getAllStores() {
    this.service.SearchStore('').subscribe((x: StoreSearchModel[]) => {
      this.ddOptions = x;
      this.filteredOptions = this.myControl.valueChanges.pipe(
        startWith(''),
        map(value => this._filter(value || '')),
      );
    });
  }

  projectSelect(evt: any) {
    this.SearchedResult.emit(evt.option.value);
  }
}
