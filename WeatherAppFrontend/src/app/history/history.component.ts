import { Component, OnInit } from '@angular/core';
import { WeatherHistoryService } from './history.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  history: History[] = [];
  displayedColumns: string[] = ['city', 'date', 'weather'];
  errorMessage: string = "";

  constructor(private weatherHistoryService: WeatherHistoryService) {}

  ngOnInit(): void {
    this.errorMessage = "";
    this.weatherHistoryService.getHistory().subscribe(
      {
        next: (data: History[]) => {
          this.history = data;
        },
        error: (err: any) => {
          this.errorMessage = err.error?.message || 'An error occurred while fetching weather data.';
        }
      }
    );
  }
}
