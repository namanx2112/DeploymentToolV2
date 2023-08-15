import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { CommonService } from 'src/app/services/common.service';
import { SonicService } from 'src/app/services/sonic.service';

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
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  constructor(private sonicService: SonicService) {

  }

  getStoreNumber(item: any){
    return item.tStoreNumber;
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

  openItem(item: any) {
    this.sonicService.SearchStore(item).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  getDateClass(val: string) {
    return "";// "redColor";
  }

  getStatusClass(val: string) {
    return "";//"greenColor";
  }
}
