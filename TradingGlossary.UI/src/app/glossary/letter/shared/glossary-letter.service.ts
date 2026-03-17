import { inject, Injectable } from '@angular/core';
import {
  GlossaryEntryDto,
  GlossaryEntryGeneratedService,
  GlossaryLetterDto,
  GlossaryLetterGeneratedService,
} from '@api/tradingglossary-api';
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class GlossaryLetterService {
  private readonly glossaryEntryGeneratedService = inject(GlossaryEntryGeneratedService);

  async getGlossaryEntriesByLetterId(letterId: number): Promise<GlossaryEntryDto[]> {
    return await firstValueFrom(
      this.glossaryEntryGeneratedService.getGlossaryEntriesByLetterId(letterId),
    );
  }
}
