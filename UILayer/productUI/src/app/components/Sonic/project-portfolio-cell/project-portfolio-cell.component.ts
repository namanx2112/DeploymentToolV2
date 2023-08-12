import { Component, Input } from '@angular/core';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-project-portfolio-cell',
  templateUrl: './project-portfolio-cell.component.html',
  styleUrls: ['./project-portfolio-cell.component.css']
})
export class ProjectPortfolioCellComponent {
  @Input()
  jObject: any;
  @Input()
  tColumn: string;
  constructor() {

  }

  getCellVal(colName: string, colVal: string) {
    let rVal = colVal;
    if (colName.indexOf("c") == 0) {
      rVal = "$" + colVal;
    }
    else if (colName.indexOf("d") == 0) {
      rVal = CommonService.getFormatedDateString(colVal);
    }
    return rVal;
  }

  getDateClass(val: string) {
    return "redColor";
  }

  getStatusClass(val: string) {
    return "greenColor";
  }
}
