import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent {
  dtField: Fields[] = [];
  constructor(private authService: AuthService, public dialogRef: MatDialogRef<ForgetPasswordComponent>) {
    this.dtField = [];
    this.dtField.push({
      field_name: "User Name",
      fieldUniqeName: "tUserNameOrEmail",
      field_type: FieldType.text,
      readOnly: false,
      field_placeholder: "Enter your user name",
      invalid: false,
      validator: [Validators.required],
      mandatory: false,
      defaultVal: "",
      hidden: false
    });
  }

  forgotPassword(item: any) {
    if (item.value["tUserNameOrEmail"] != "") {
      this.authService.ForgotPassword({ tContent: item.value["tUserNameOrEmail"] }).subscribe(x => {
        alert(x);
        this.dialogRef.close();
      });
    }
    else
      alert("Please enter your user name to reset your password");
  }

  Close(item: any) {
    this.dialogRef.close();
  }
}
