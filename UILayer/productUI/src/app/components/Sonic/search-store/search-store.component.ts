import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-search-store',
  templateUrl: './search-store.component.html',
  styleUrls: ['./search-store.component.css']
})
export class SearchStoreComponent {
  myControl = new FormControl('');
  ddOptions: StoreSearchModel[];
  filteredOptions: Observable<StoreSearchModel[]>;
  @Output() SearchedResult = new EventEmitter<string>();
  constructor(private service: SonicService) {
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
