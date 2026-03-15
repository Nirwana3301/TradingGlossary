import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WelcomeSplashComponent } from './welcome-splash/welcome-splash.component';
import { NgxFlickeringGridComponent } from '@omnedia/ngx-flickering-grid';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, WelcomeSplashComponent, NgxFlickeringGridComponent],
  templateUrl: './app.html',
  standalone: true,
})
export class App {
  protected readonly title = signal('TradingGlossary.UI');
}
