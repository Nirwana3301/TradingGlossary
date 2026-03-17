import { Component, input } from '@angular/core';
import { GlossaryEntry, GlossaryEntryDto } from '@api/tradingglossary-api';

@Component({
  selector: 'glossary-entry',
  templateUrl: 'glossary-entry.component.html'
})
export class GlossaryEntryComponent {
  glossaryEntry = input.required<GlossaryEntryDto>()
}
