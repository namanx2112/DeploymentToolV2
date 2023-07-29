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

  async loadDropdown(){    
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
    this.getScreenshot(function (bytes: any) {
      const dialogConfig = new MatDialogConfig();
      let dialogRef: any;
      dialogConfig.autoFocus = true;
      dialogConfig.width = '60%';
      dialogConfig.data = {
        fileBytes: bytes,
        onSubmit: function (data: any) {
          dialogRef.close();
        }
      };
      dialogRef = cThis.dialog.open(SupportPageComponent, dialogConfig);
    })
  }

  getScreenshot(callBack: any) {
    const div = document.body;
    if (div) {
      const options = {
        background: 'white',
        scale: 3
      };

      html2canvas(div, options).then((canvas) => {
        //var base64URL = canvas.toDataURL('image/jpeg').replace('image/jpeg', 'image/octet-stream');
        callBack(canvas.toDataURL('image/jpeg'))
      });
    }
  }

  OpenTicketWatcher() {
    let inpVal = prompt("Please enter Ticket Id");
    if (inpVal != null) {
      let nTicketId = parseInt(inpVal);
      let cThis = this;
      this.getScreenshot(function (bytes: any) {
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
      })
    }
  }

  actionChanged(ev: any){
    this.tTabName = ev;
  }
}
