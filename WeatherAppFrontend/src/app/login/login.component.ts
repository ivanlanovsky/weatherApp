import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private router: Router) {
    this.loginForm = new FormGroup(
      {
        email: new FormControl("", [Validators.email, Validators.required]),
        password: new FormControl("", [Validators.required])
      }
    );
  }

  onLogin() {
    var email = this.loginForm.controls['email'].value;
    var password = this.loginForm.controls['password'].value;
  }
}

