import { inject, Injectable } from '@angular/core';
import { GlossaryLetterDto, GlossaryLetterGeneratedService } from '@api/tradingglossary-api';
import { firstValueFrom } from 'rxjs';

@Injectable({providedIn: 'root'})
export class GlossaryService {
  private readonly glossaryLetterGeneratedService = inject(GlossaryLetterGeneratedService);

  async getAllGlossaryLetters(): Promise<GlossaryLetterDto[]> {
    return await firstValueFrom(this.glossaryLetterGeneratedService.getGlossaryLetters());
  }
}
