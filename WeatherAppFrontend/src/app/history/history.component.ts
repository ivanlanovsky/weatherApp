import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  history: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory() {
    this.http.get('https://your-api-url/api/history').subscribe(
      (data: any[]) => {
        this.history = data;
      },
      (error) => {
        console.error('Error fetching history:', error);
      }
    );
  }
}
