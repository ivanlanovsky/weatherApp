import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Weather } from './weather.model';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private apiUrl = 'https://localhost:7150/api/weather';

  constructor(private http: HttpClient) {}

  getWeather(city: string): Observable<Weather> {
    return this.http.get<Weather>(`${this.apiUrl}/${city}`);
  }
}
