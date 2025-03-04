import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherHistoryService {
  private historyUrl = 'https://localhost:7150/api/WeatherHistory/10';

  constructor(private http: HttpClient) {}

  getHistory(): Observable<History[]> {
    return this.http.get<History[]>(this.historyUrl);
  }
}
