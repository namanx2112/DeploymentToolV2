import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BrandModel } from 'src/app/interfaces/models';
import { AuthService } from 'src/app/services/auth.service';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';
import { SupportPageComponent } from '../support-page/support-page.component';
import html2canvas from 'html2canvas';
import { AccessService } from 'src/app/services/access.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { NotImplementedComponent } from '../not-implemented/not-implemented.component';
import { TicketViewComponent } from '../ticket-view/ticket-view.component';
import { StoreSearchModel } from 'src/app/interfaces/sonic';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  viewName: string;
  tTabName: string;
  userName: string;
  directStore: StoreSearchModel;
  constructor(private homeService: HomeService, private commonService: CommonService, private authService: AuthService, private dialog: MatDialog, public access: AccessService) {
    this.viewName = "Dashboard";
    this.tTabName = "Dashboard";
    this.userName = this.authService.getUserName();
    this.commonService.getAllDropdowns();
    if (this.authService.isFirstTime())
      this.ChangePassword("Please change your password");
  }


  getValue() {
    this.homeService.loginGet().subscribe((res: string) => {
      alert(res);
    });
  }

  openStoreFromNotification(item: any) {
    this.directStore = item;
  }

  switchView(vName: string) {
    this.viewName = vName;
    this.tTabName = vName;
  }

  changeViewFromDashboard(item: any) {
    if (typeof item == 'string')
      this, this.viewName = item;
    else {
      if (item.view == "sonic") {
        this.directStore = item.instance;
      }
      this.viewName = item.view;
    }
  }

  changeView(vName: string) {
    this.viewName = vName;
  }

  LogOut() {
    this.authService.loggedOut();
  }

  supportPage() {
    let cThis = this;
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    dialogConfig.data = {
      onSubmit: function (data: any) {
        dialogRef.close();
      }
    };
    dialogRef = cThis.dialog.open(TicketViewComponent, dialogConfig);
    //dialogRef = cThis.dialog.open(NotImplementedComponent, dialogConfig);
  }

  ChangePassword(title?: string) {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.data = {
      title: (typeof title == "undefined") ? "Change Password" : title,
      onChange: function () {
        dialogRef.close();
      },
      onClose: function () {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(ChangePasswordComponent, dialogConfig);
  }

  OpenTicketWatcher() {
    let inpVal = prompt("Please enter Ticket Id");
    if (inpVal != null) {
      let nTicketId = parseInt(inpVal);
      let cThis = this;
      const dialogConfig = new MatDialogConfig();
      let dialogRef: any;
      dialogConfig.autoFocus = true;
      dialogConfig.width = '60%';
      dialogConfig.data = {
        nTicketId: nTicketId,
        onSubmit: function (data: any) {
          dialogRef.close();
        }
      };
      dialogRef = cThis.dialog.open(SupportPageComponent, dialogConfig);
    }
  }

  actionChanged(ev: any) {
    this.tTabName = ev;
  }
}
