import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DateChangePOOption, StoreSearchModel } from 'src/app/interfaces/sonic';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-date-change-revised-po',
  templateUrl: './date-change-revised-po.component.html',
  styleUrls: ['./date-change-revised-po.component.css']
})
export class DateChangeRevisedPOComponent {

  curStore: StoreSearchModel;
  allPOs: DateChangePOOption[];
  constructor(public dialogRef: MatDialogRef<DateChangeRevisedPOComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private service: StoreService) {
    this.curStore = data.curStore;
    this.allPOs = data.tItems;
  }

  cannotSend() {
    let cant = false;
    if (this.allPOs.findIndex(x => x.isSelected) == -1)
      cant = true;
    return cant;
  }

  SendDateChangeRevisedPO() {
    this.service.SendDateChangeRevisedPO(this.allPOs).subscribe(x => {
      if (x == "")
        this.dialogRef.close();
      else
        alert(x);
    });
  }

  onCancelUserDialog(): void {
    this.dialogRef.close();
  }
}
