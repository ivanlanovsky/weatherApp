import { Component, OnInit } from '@angular/core';
import { WeatherHistoryService } from './history.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  history: History[] = [];
  displayedColumns: string[] = ['City', 'Date', 'Id', 'Weather'];
  errorMessage: string = "";

  constructor(private weatherHistoryService: WeatherHistoryService) {}

  ngOnInit(): void {
    this.errorMessage = "";
    this.weatherHistoryService.getHistory().subscribe(
      (data: History[]) => {
        this.history = data;
      },
      (err: any) => {
        this.errorMessage = err.error?.message || 'An error occurred while fetching weather data.';
      }
    );
  }
}
