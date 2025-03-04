import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map, catchError } from 'rxjs';
import { User } from "../models/user.model"
import { Credentials } from '../models/credentials.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _currentUser: User | null = null;
  private _isLoggedIn: boolean = false;
  private authUrl = 'https://localhost:7150/api/login';

  constructor(private http: HttpClient){}

  get currentUser(): User | null {
    return this._currentUser;
  }

  get isLoggedIn(): boolean {
    return this._isLoggedIn;
  }
  
  login(credentials: Credentials): Observable<User>{
    return this.http.post(this.authUrl, credentials).pipe(
      map((user: any) => {
        this._currentUser = user;
        this._isLoggedIn = true;
        return user;
      })
    );
  }

  logout(){
    this._isLoggedIn = false;
  }
}
