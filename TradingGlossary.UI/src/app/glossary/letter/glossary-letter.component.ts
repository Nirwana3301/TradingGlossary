import { Component, inject, input, OnInit, signal } from '@angular/core';
import { GlossaryLetterDto } from '@api/tradingglossary-api';
import { GlossaryLetterService } from './shared/glossary-letter.service';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'glossary-letter',
  templateUrl: 'glossary-letter.component.html',
  imports: [UpperCasePipe],
})
export class GlossaryLetterComponent {
  glossaryLetter = input.required<GlossaryLetterDto>();

  isOpen = signal<boolean>(false);

  toggleIsOpen() {
    this.isOpen.update(isOpen => !isOpen);
  }
}
