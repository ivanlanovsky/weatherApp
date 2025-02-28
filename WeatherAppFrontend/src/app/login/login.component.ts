import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private router: Router) {}

  onLogin() {
    if (this.email === 'admin@example.com' && this.password === 'password123') {
      alert('Login Successful!');
      this.router.navigate(['/dashboard']); // Navigate to dashboard after login
    } else {
      alert('Invalid email or password. Please try again.');
    }
  }
}

