import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WelcomeSplashComponent } from './welcome-splash/welcome-splash.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, WelcomeSplashComponent],
  templateUrl: './app.html',
  standalone: true,
})
export class App {
  protected readonly title = signal('TradingGlossary.UI');
}
