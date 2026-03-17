import { Component, inject, OnInit, signal } from '@angular/core';
import { GlossaryLetterService } from './letter/shared/glossary-letter.service';
import { GlossaryLetterDto } from '@api/tradingglossary-api';
import { GlossaryService } from './shared/glossary.service';
import { GlossaryLetterComponent } from './letter/glossary-letter.component';

@Component({
  selector: 'glossary',
  templateUrl: 'glossary.component.html',
  imports: [GlossaryLetterComponent],
})
export class GlossaryComponent implements OnInit {
  private readonly glossaryService = inject(GlossaryService);

  glossaryLetters = signal<GlossaryLetterDto[]>([]);

  async ngOnInit() {
    const letters = await this.glossaryService.getAllGlossaryLetters();
    this.glossaryLetters.set(letters);
  }
}
