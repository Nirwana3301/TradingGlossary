import { inject, Injectable } from '@angular/core';
import { GlossaryLetterGeneratedService } from '@api/tradingglossary-api';
import { Observable } from 'rxjs';
import { SelectableGlossaryLetter } from '../../shared/glossary.interfaces';

@Injectable({ providedIn: 'root' })
export class GlossaryLetterService {
  private readonly glossaryLetterGeneratedService = inject(GlossaryLetterGeneratedService);

  getGlossaryLetters(): Observable<SelectableGlossaryLetter[]> {
    return this.glossaryLetterGeneratedService.getGlossaryLetters();
  }
}
