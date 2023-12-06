import { CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDrag,
  CdkDropList } from '@angular/cdk/drag-drop';
import { Component } from '@angular/core';
import { ReportField } from 'src/app/interfaces/report-generator';

@Component({
  selector: 'app-field-selection',
  templateUrl: './field-selection.component.html',
  styleUrls: ['./field-selection.component.css']
})
export class FieldSelectionComponent {

  allFields: ReportField[];
  sortColumns: number[] = [];
  selectedColumns: number[] = [];
  constructor() {

  }

  todo = ['Get to work', 'Pick up groceries', 'Go home', 'Fall asleep'];

  done = ['Get up', 'Brush teeth', 'Take a shower', 'Check e-mail', 'Walk dog'];

  drop(event: CdkDragDrop<string[]>) {
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
}
