import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Fields } from 'src/app/interfaces/home-tab';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  dtField: Fields[] = [];
  onClose: any;
  onChange: any;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private service: AuthService) {
    if (data) {
      this.onChange = data.onChange;
      this.onClose = data.onClose;
    }
  }



  changePassword(item: any) {
    this.service.ChangePassword(item.value).subscribe(x => {
      if (x == "")
        this.onChange(x);
      else
        alert(x);
    });
  }

  Close(item: any) {
    this.onClose();
  }
}
