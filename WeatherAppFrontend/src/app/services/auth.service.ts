import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isLoggedIn: boolean = false;

  get isLoggedIn(): boolean {
    return this._isLoggedIn;
  }
  
  login(): boolean{
    this._isLoggedIn = true;
    return true;
  }

  logout(){
    this._isLoggedIn = false;
  }
}
