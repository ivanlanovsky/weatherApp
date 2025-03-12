import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  
  get isLoggedIn(): boolean{
    return this.authService.isLoggedIn
  }

  constructor(private authService: AuthService) {}

  logout(){
    this.authService.logout();
  }

}
