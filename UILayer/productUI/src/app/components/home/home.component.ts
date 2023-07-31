import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BrandModel } from 'src/app/interfaces/models';
import { AuthService } from 'src/app/services/auth.service';
import { BrandServiceService } from 'src/app/services/brand-service.service';
import { CommonService } from 'src/app/services/common.service';
import { HomeService } from 'src/app/services/home.service';
import { SupportPageComponent } from '../support-page/support-page.component';
import html2canvas from 'html2canvas';
import { AccessService } from 'src/app/services/access.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { NotImplementedComponent } from '../not-implemented/not-implemented.component';
import { TicketViewComponent } from '../ticket-view/ticket-view.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  viewName: string;
  tTabName: string;
  userName: string;
  constructor(private homeService: HomeService, private brandService: BrandServiceService,
    private commonService: CommonService, private authService: AuthService, private dialog: MatDialog, public access: AccessService) {
    this.viewName = "Dashboard";
    this.tTabName = "Dashboard";
    this.userName = this.authService.getUserName();
    this.loadDropdown();
  }

  getValue() {
    this.homeService.loginGet().subscribe((res: string) => {
      alert(res);
    });
  }

  async loadDropdown() {
    this.commonService.getAllDropdowns();
  }

  switchView(vName: string) {
    this.viewName = vName;
    this.tTabName = vName;
  }

  brandClicked(brand: BrandModel) {
    this.viewName = brand.tBrandName;
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
  }

  ChangePassword() {
    const dialogConfig = new MatDialogConfig();
    let dialogRef: any;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.data = {
      onChange: function () {
        dialogRef.close();
      },
      onClose: function () {
        dialogRef.close();
      }
    };
    dialogRef = this.dialog.open(NotImplementedComponent, dialogConfig);
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
