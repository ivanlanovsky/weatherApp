import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Credentials } from '../models/credentials.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = "";

  constructor(private router: Router, private authService: AuthService) {
    this.loginForm = new FormGroup(
      {
        email: new FormControl("", [Validators.email, Validators.required]),
        password: new FormControl("", [Validators.required])
      });

    this.loginForm.valueChanges.subscribe(
      {
        next: () => {
          this.errorMessage = "";
        }
        
      });
  }

  onLogin() {
    var email = this.loginForm.controls['email'].value;
    var password = this.loginForm.controls['password'].value;
    this.authService.login({ email, password }).subscribe(
      {
        next: (user) => {
          this.router.navigate(["../"]);
        },
        error: (err) => {
          this.errorMessage = err.error.error;
          console.log(err);
        }
      });
  }
  
}

