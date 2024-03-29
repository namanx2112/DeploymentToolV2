import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { BrandModel } from 'src/app/interfaces/models';
import { StoreSearchModel } from 'src/app/interfaces/store';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-search-store',
  templateUrl: './search-store.component.html',
  styleUrls: ['./search-store.component.css']
})
export class SearchStoreComponent {
  myControl = new FormControl('');
  ddOptions: StoreSearchModel[];
  filteredOptions: Observable<StoreSearchModel[]>;
  _curBrandId: number;
  _defaultConditionForSearch: string;
  @Input()
  set request(val: any) {
    this._curBrandId = val.curBrandId;
    if (typeof val.defaultConditionForSearch != 'undefined')
      this._defaultConditionForSearch = val.defaultConditionForSearch;
    else
      this._defaultConditionForSearch = "";
    this.getAllStores();
  }
  @Output() SearchedResult = new EventEmitter<string>();
  constructor(private service: ExStoreService) {
  }

  compareStore(s1: string, s2: string) {
    return (s2 == "") ? true : s1.startsWith(s2);
  }

  private _filter(value: string): StoreSearchModel[] {
    if (typeof value == 'string') {
      return this.ddOptions.filter(option => this.compareStore(option.tStoreNumber, value));
    }
    else
      return [];
  }

  getAllStores() {
    this.service.SearchStore(this._defaultConditionForSearch, this._curBrandId).subscribe((x: StoreSearchModel[]) => {
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
