import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ToolbarModule } from 'primeng/toolbar';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  get isLoggedIn(): boolean{
    return this.authService.isLoggedIn;
  }
  constructor(private authService: AuthService){

  }

  login(){
    this.authService.login();
  }

  logout(){
    this.authService.logout();
  }

}
