import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { WeatherComponent } from './weather/weather.component';
import { HistoryComponent } from './history/history.component';
import { WeatherService } from './weather/weather.service'; 
import { WeatherHistoryService } from './history/history.service';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button'; 
import { MessageModule } from 'primeng/message';
import { AuthService } from './services/auth.service';
import { HeaderComponent } from './header/header.component';


@NgModule({
  declarations: [
    AppComponent,
    WeatherComponent,
    HistoryComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MessageModule,
    InputTextModule,
    TableModule,
    ToolbarModule,   // Add Toolbar module
    ButtonModule
  ],
  providers: [WeatherService, WeatherHistoryService, AuthService], 
  bootstrap: [AppComponent]
})
export class AppModule { }
