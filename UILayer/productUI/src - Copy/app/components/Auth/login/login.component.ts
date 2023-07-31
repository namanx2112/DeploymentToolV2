import { Component, OnInit } from '@angular/core';
import { AuthResponse } from 'src/app/interfaces/auth-response';
import { AuthService } from 'src/app/services/auth.service';
import { AuthRequest } from 'src/app/interfaces/auth-request';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FieldType, Fields } from 'src/app/interfaces/home-tab';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  authResp?: AuthResponse;
  auth: AuthRequest;
  dtField: Fields[] = [];
  constructor(private authService: AuthService, public router: Router, private route: ActivatedRoute) {
    this.dtField = [];
    this.dtField.push({
      field_name: "User Name",
      fieldUniqeName: "UserName",
      field_type: FieldType.text,
      readOnly: false,
      field_placeholder: "Enter your user name",
      invalid: false,
      validator: [Validators.required],
      mandatory: false,
      defaultVal: "",
      hidden: false
    }, {
      field_name: "Password",
      fieldUniqeName: "Password",
      field_type: FieldType.password,
      readOnly: false,
      field_placeholder: "Enter your password",
      invalid: false,
      validator: [Validators.required],
      mandatory: false,
      defaultVal: "",
      hidden: false
    });
  }
  ngOnInit(): void {
    this.checkLoggedIn();
  }

  checkLoggedIn() {
    let move = this.authService.isLoggedIn();
  }

  logMeIn(resp: any) {
    if (resp.value["UserName"] == "" || resp.value["Password"] == "")
      alert("Please enter user name and password");
    else {
      let request = {
        UserName: resp.value["UserName"],
        Password: btoa(resp.value["Password"])
      }
      this.authService.signIn(request);
    }
  }
}
