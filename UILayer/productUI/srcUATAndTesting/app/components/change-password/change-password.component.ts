import { Component, Inject } from '@angular/core';
import { Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { AuthService } from 'src/app/services/auth.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  dtField: Fields[] = [];
  onClose: any;
  onChange: any;
  title: string;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private service: AuthService) {
    if (data) {
      this.dtField = [];
      this.title = data.title;
      this.dtField.push({
        field_name: "Current password",
        fieldUniqeName: "tCurrentPassword",
        field_type: FieldType.password,
        readOnly: false,
        field_placeholder: "Enter current password",
        invalid: false,
        validator: [Validators.required],
        mandatory: false,
        defaultVal: "",
        hidden: false
      }, {
        field_name: "New password",
        fieldUniqeName: "tNewPassword",
        field_type: FieldType.password,
        readOnly: false,
        field_placeholder: "Enter a new password",
        invalid: false,
        validator: [Validators.required],
        mandatory: false,
        defaultVal: "",
        hidden: false
      }, {
        field_name: "Confirm password",
        fieldUniqeName: "tConfirmPassword",
        field_type: FieldType.password,
        readOnly: false,
        field_placeholder: "Enter confirm password",
        invalid: false,
        validator: [Validators.required],
        mandatory: false,
        defaultVal: "",
        hidden: false
      });
      this.onChange = data.onChange;
      this.onClose = data.onClose;
    }
  }



  changePassword(item: any) {
    if (item.value["tNewPassword"] == item.value["tConfirmPassword"]) {
      let request = {
        tNewPassword: CommonService.getHashed(item.value["tNewPassword"]),
        tCurrentPassword: CommonService.getHashed(item.value["tCurrentPassword"]),
      };
      this.service.ChangePassword(request).subscribe(x => {
        if (x == "")
          this.onChange(x);
        else
          alert(x);
      });
    }
    else
      alert("Your new password does not match with confirm password");
  }

  Close(item: any) {
    this.onClose();
  }
}
