import { Component } from '@angular/core';
import { WeatherService } from '../services/weather.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent {
  city: string = '';
  weatherData: any = null;
  loading: boolean = false;
  errorMessage: string = '';

  constructor(private weatherService: WeatherService) {}

  getWeather() {
    this.errorMessage = '';
    if (this.city) {
      this.loading = true;
      this.weatherService.getWeather(this.city).subscribe(
        (data: any) => {
          this.weatherData = data;
          this.loading = false;
        },
        (err: any) => {
          this.errorMessage = err.error?.message || 'An error occurred while fetching weather data.';
          this.loading = false;
        }
      );
    }
  }
}

