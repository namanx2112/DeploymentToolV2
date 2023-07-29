import { Component, OnInit } from '@angular/core';
import { AuthResponse } from 'src/app/interfaces/auth-response';
import { AuthService } from 'src/app/services/auth.service';
import { AuthRequest } from 'src/app/interfaces/auth-request';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  authResp?: AuthResponse;
  auth: AuthRequest;
  formGroup = new FormGroup({});
  constructor(private authService: AuthService, public router: Router, private route: ActivatedRoute) {
    this.formGroup.addControl("UserName", new FormControl(
      "", Validators.required));
    this.formGroup.addControl("Password", new FormControl(
      "", Validators.required));
  }
  ngOnInit(): void {
    this.checkLoggedIn();
  }

  checkLoggedIn() {
    let move = this.authService.isLoggedIn();
  }

  hasEror(cControl: string): boolean {
    let has = false;
    let control = this.formGroup.get(cControl);
    if (typeof control != 'undefined' && control != null) {
      has = !control.valid;
    }
    return has;
  }

  logMeIn() {
    if (this.formGroup.valid) {
      this.authService.signIn(this.formGroup.value);
    }
  }
}
