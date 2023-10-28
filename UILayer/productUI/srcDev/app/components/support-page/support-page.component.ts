import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SupportContent } from 'src/app/interfaces/models';
import { SupportService } from 'src/app/services/support.service';

@Component({
  selector: 'app-support-page',
  templateUrl: './support-page.component.html',
  styleUrls: ['./support-page.component.css']
})
export class SupportPageComponent {

  tRequest: SupportContent;
  nTicketId: number;
  constructor(public dialogRef: MatDialogRef<SupportPageComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private supportService: SupportService) {
    this.nTicketId = 0;
    if (typeof data.nTicketId != 'undefined') {
      this.nTicketId = data.nTicketId;
      this.getTicketDetails();
    }
    else
      this.tRequest = {
        nPriority: 2,
        aTicketId: 0,
        tFixComment: "",
        tTicketStatus: "New",
        dtCreatedOn : new Date(),
        nCreatedBy: 1,
        tContent: "",
        nFileSie: data.fileBytes.length,
        fBase64: data.fileBytes
      }

  }

  getTicketDetails() {
    this.supportService.Get(this.nTicketId).subscribe(x => {
      this.tRequest = x;
    });
  }

  LogTicket() {
    this.supportService.LogSupportTicket(this.tRequest).subscribe(x => {
      this.dialogRef.close();
    });
  }

  CantSend() {
    let cant = false;
    if (this.tRequest.tContent == "")
      cant = true;
    return cant;
  }

}
