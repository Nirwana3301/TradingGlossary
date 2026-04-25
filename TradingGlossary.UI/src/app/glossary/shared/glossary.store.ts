import { GlossaryLetter } from '@api/tradingglossary-api';
import { signalStore, withMethods, withProps, withState } from '@ngrx/signals';
import { inject } from '@angular/core';
import { GlossaryLetterService } from '../glossary-letter/shared/glossary-letter.service';
import { rxResource } from '@angular/core/rxjs-interop';

type GlossaryStoreState = {
  selectedGlossaryLetter: GlossaryLetter | null;
};

const initialState: GlossaryStoreState = {
  selectedGlossaryLetter: null,
};

export const GlossaryStore = signalStore(
  withState<GlossaryStoreState>(initialState),
  withProps((store) => ({
    _glossaryLetterService: inject(GlossaryLetterService),
  })),
  withProps((store) => ({
    glossaryLettersResource: rxResource({
      defaultValue: [],
      stream: () => store._glossaryLetterService.getGlossaryLetters(),
    }),
  })),
  withMethods((store) => {
    return {};
  }),
);
