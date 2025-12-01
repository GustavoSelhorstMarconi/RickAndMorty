import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CharacterCard } from './components/character-card/character-card';
import { SearchEpisodes } from './components/search-episodes/search-episodes';
import { RickAndMorty } from './services/rick-and-morty';
import { Character } from './shared/character.model';

@Component({
  selector: 'app-root',
  imports: [SearchEpisodes, CharacterCard],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  private readonly _rickAndMortyService = inject(RickAndMorty);

  private destroyRef = inject(DestroyRef);

  characters: Character[] = [];
  loading: boolean = false;
  sortAsc: boolean = true;
  lastEpisodeSelected: number | null = null;

  onEpisodeSelected(episodeId: number) {
    this.loadCharactersFromEpisode(episodeId);
  }

  loadCharactersFromEpisode(episodeId: number) {
    this.lastEpisodeSelected = episodeId;
    this.loading = true;

    this._rickAndMortyService
      .getCharactersByEpisode(episodeId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (chars) => {
          this.characters = chars;
          this.loading = false;
        },
        error: () => {
          this.characters = [];
          this.loading = false;
        },
      });
  }

  toggleSort() {
    this.sortAsc = !this.sortAsc;
    this.characters = [...this.characters].reverse(); // inverte a ordem atual
  }
}
