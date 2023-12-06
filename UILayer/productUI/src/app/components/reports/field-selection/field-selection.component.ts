import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDrag,
  CdkDropList
} from '@angular/cdk/drag-drop';
import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DisplayColumn, ReportField, SortColumns } from 'src/app/interfaces/report-generator';

@Component({
  selector: 'app-field-selection',
  templateUrl: './field-selection.component.html',
  styleUrls: ['./field-selection.component.css']
})
export class FieldSelectionComponent {

  allFields: ReportField[] = [];
  sortColumns: SortColumns[] = [];
  selectedColumns: DisplayColumn[] = [];
  @ViewChild('input') input: ElementRef<HTMLInputElement>;
  filteredOptionsSort: ReportField[] = [];
  filteredOptionsColumn: ReportField[] = [];
  aferSave: any;
  showAddFor: string;
  fieldsByGroup: any = {};
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    if (typeof data != 'undefined') {
      this.allFields = data.allFields;
      this.sortColumns = (typeof data.srtClmn != 'undefined') ? data.srtClmn : [];
      this.selectedColumns = (typeof data.spClmn != 'undefined') ? data.spClmn : [];
      this.filteredOptionsSort = data.allFields;
      this.filteredOptionsColumn = data.allFields;
      this.fieldsByGroup = data.fieldsByGroup;
      this.aferSave = data.aferSave;
      this.initItems();
    }
  }

  initItems() {
    for (var indx in this.selectedColumns) {
      this.pushUpdateArray(this.selectedColumns[indx].nFieldID, false);
    }

    for (var indx in this.sortColumns) {
      this.pushUpdateArray(this.sortColumns[indx].nFieldID, true);
    }
  }

  pushUpdateArray(fieldId: number, sort: boolean) {
    if (sort) {
      let tItem = this.allFields.find(x => x.aFieldID == fieldId);
      if (tItem) {
        let indx = this.sortColumns.findIndex(x => x.nFieldID == fieldId);
        if (indx > -1)
          this.sortColumns[indx].tFieldName = tItem.tFieldName;
        else
          this.sortColumns.push({
            aSortColumnsID: -1,
            nFieldID: fieldId,
            nOrder: 0,
            tFieldName: tItem.tFieldName,
            nRelatedType: tItem.nFieldTypeID,
            nRelatedID: -1
          });
      }
    }
    else {
      let tItem = this.allFields.find(x => x.aFieldID == fieldId);
      if (tItem) {
        let indx = this.selectedColumns.findIndex(x => x.nFieldID == fieldId);
        if (indx > -1)
          this.selectedColumns[indx].tFieldName = tItem.tFieldName;
        else
          this.selectedColumns.push({
            aDisplayColumnsID: -1,
            nFieldID: fieldId,
            nOrder: 0,
            tFieldName: tItem.tFieldName,
            nRelatedType: tItem.nFieldTypeID,
            nRelatedID: -1
          });
      }
    }
  }

  fieldChanged(item: any, fromSort: boolean) {
    if (item.option.value) {
      this.pushUpdateArray(item.option.value.aFieldID, fromSort);
      this.showAddFor = "";
    }
  }

  delColumn(indx: number, sort: boolean) {
    if (sort) {
      this.sortColumns.splice(indx, 1);
    }
    else {
      this.selectedColumns.splice(indx, 1);
    }
  }

  drop(event: CdkDragDrop<any[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  filter(sort: boolean): void {
    const filterValue = this.input.nativeElement.value.toLowerCase();
    if (sort)
      this.filteredOptionsSort = this.allFields.filter(o => o.tFieldName.toLowerCase().includes(filterValue));
    else
      this.filteredOptionsColumn = this.allFields.filter(o => o.tFieldName.toLowerCase().includes(filterValue));
  }

  saveClicked() {
    this.aferSave({ srtClmn: this.sortColumns, spClmn: this.selectedColumns });
  }
}
