import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from "../models/user.model";
import { Credentials } from '../models/credentials.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _currentUserSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
  private _isLoggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private authUrl = 'https://localhost:7150/api/auth/';

  constructor(private http: HttpClient) {}

  get currentUser(): Observable<User | null> {
    return this._currentUserSubject.asObservable();
  }

  get isLoggedIn(): boolean {
    return this._isLoggedInSubject.value
  }

  login(credentials: Credentials): Observable<User> {
    return this.http.post<User>(`${this.authUrl}login`, credentials, { withCredentials: true }).pipe(
      map((user: User) => {
        this._currentUserSubject.next(user);
        this._isLoggedInSubject.next(true);
        return user;
      })
    );
  }

  logout() {
    this.http.get(`${this.authUrl}logout`, { withCredentials: true });
    this._currentUserSubject.next(null);
    this._isLoggedInSubject.next(false);
  }
}
