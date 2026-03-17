import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { BASE_PATH } from '@api/tradingglossary-api';

const apiUrls = {
  localhost: 'http://localhost:5000',
};

function getApiUrl(): string {
  return apiUrls.localhost;
}


export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    providePrimeNG(),
    provideStore(),
    {
      provide: BASE_PATH,
      useValue: getApiUrl()
    },
  ],
};
