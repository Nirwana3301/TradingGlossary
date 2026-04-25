import { Component, inject, OnInit, signal } from '@angular/core';
import { GlossaryLetterDto } from '@api/tradingglossary-api';
import { GlossaryService } from './shared/glossary.service';

@Component({
  selector: 'glossary',
  templateUrl: 'glossary.component.html',
})
export class GlossaryComponent implements OnInit {
  private readonly glossaryService = inject(GlossaryService);

  glossaryLetters = signal<GlossaryLetterDto[]>([]);

  async ngOnInit() {
    const letters = await this.glossaryService.getAllGlossaryLetters();
    this.glossaryLetters.set(letters);
  }
}
