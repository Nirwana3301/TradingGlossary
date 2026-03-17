import { Component, effect, inject, input, OnInit, signal, untracked } from '@angular/core';
import { GlossaryEntryDto, GlossaryLetterDto } from '@api/tradingglossary-api';
import { GlossaryLetterService } from './shared/glossary-letter.service';
import { UpperCasePipe } from '@angular/common';
import { GlossaryEntryComponent } from './glossary-entry/glossary-entry.component';

@Component({
  selector: 'glossary-letter',
  templateUrl: 'glossary-letter.component.html',
  imports: [UpperCasePipe, GlossaryEntryComponent],
})
export class GlossaryLetterComponent {
  private readonly glossaryLetterService = inject(GlossaryLetterService);

  glossaryLetter = input.required<GlossaryLetterDto>();
  glossaryEntries = signal<GlossaryEntryDto[]>([]);

  isOpen = signal<boolean>(false);

  toggleIsOpen() {
    this.isOpen.update((isOpen) => !isOpen);
  }

  constructor() {
    effect(() => {
      const isOpen = this.isOpen();

      untracked(() => {
        if (isOpen) {
          this.glossaryLetterService
            .getGlossaryEntriesByLetterId(this.glossaryLetter().id)
            .then((glossaryEntries) => {
              this.glossaryEntries.set(glossaryEntries);
            });
        }
      });
    });
  }
}
