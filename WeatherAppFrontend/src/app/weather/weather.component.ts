import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent {
  city: string = '';
  weatherData: any;
  errorMessage: string = '';

  constructor(private http: HttpClient) {}

  getWeather() {
    if (!this.city) {
      this.errorMessage = 'Please enter a city.';
      return;
    }

    this.http.get(`https://your-api-url/api/weather/${this.city}`).subscribe(
      (data) => {
        this.weatherData = data;
        this.errorMessage = '';
      },
      (error) => {
        this.errorMessage = 'Error fetching weather data. Please try again.';
        this.weatherData = null;
      }
    );
  }
}
