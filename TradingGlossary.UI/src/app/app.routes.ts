import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    title: 'Glossary',
    loadComponent: () => import('./glossary/glossary.component').then(m => m.GlossaryComponent)
  }
];
