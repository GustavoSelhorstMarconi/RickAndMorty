import { Component, DestroyRef, ElementRef, inject, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { CharacterCard } from './components/character-card/character-card';
import { SearchEpisodes } from './components/search-episodes/search-episodes';
import { RickAndMorty } from './services/rick-and-morty';
import { Character } from './shared/character.model';

@Component({
  selector: 'app-root',
  imports: [SearchEpisodes, CharacterCard, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  private readonly _rickAndMortyService = inject(RickAndMorty);
  private destroyRef = inject(DestroyRef);
  @ViewChild('controlsContainer')
  controlsContainer!: ElementRef<HTMLDivElement>;

  characters: Character[] = [];
  loading: boolean = false;
  sortAsc: boolean = true;
  selectedGroup: string = 'None';
  lastEpisodeSelected: number | null = null;
  charactersGrouped: Record<string, Character[]> = {};

  groupFields = [
    'None',
    'Status',
    'Species',
    'Type',
    'Gender',
    'Origin',
    'Location',
  ];

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
          this.groupBy();
          setTimeout(() => {
            this.controlsContainer?.nativeElement.scrollIntoView({
              behavior: 'smooth',
              block: 'start',
            });
          }, 50);
        },
        error: () => {
          this.characters = [];
          this.loading = false;
          this.selectedGroup = 'None';
          this.groupBy();
        },
      });
  }

  toggleSort() {
    this.sortAsc = !this.sortAsc;
    this.characters = [...this.characters].reverse();
    this.groupBy();
  }

  groupBy() {
    this.charactersGrouped = {};

    if (!this.selectedGroup) return;

    if (this.selectedGroup === 'None') {
      this.charactersGrouped = {};

      this.characters.sort((a, b) => {
        let aValue: string = '';
        let bValue: string = '';

        aValue = a.name.toLowerCase();
        bValue = b.name.toLowerCase();

        if (aValue < bValue) return this.sortAsc ? -1 : 1;
        if (aValue > bValue) return this.sortAsc ? 1 : -1;

        return 0;
      });

      return;
    }

    this.characters.map((char) => {
      const characterField = this.getCharacterField(char, this.selectedGroup);
      if (this.charactersGrouped.hasOwnProperty(characterField))
        this.charactersGrouped[characterField].push(char);
      else this.charactersGrouped[characterField] = [char];
    });

    const orderedKeys = Object.keys(this.charactersGrouped).sort((a, b) => {
      const comparison = a.toLowerCase().localeCompare(b.toLowerCase());
      return this.sortAsc ? comparison : -comparison;
    });

    const orderedGrouped: Record<string, Character[]> = {};
    orderedKeys.forEach((key) => {
      orderedGrouped[key] = this.charactersGrouped[key].sort((a, b) => {
        const comparison = a.name
          .toLowerCase()
          .localeCompare(b.name.toLowerCase());
        return this.sortAsc ? comparison : -comparison;
      });
    });

    this.charactersGrouped = orderedGrouped;
  }

  getCharacterField(char: Character, field: string): string {
    const fieldLowerCase = field.toLocaleLowerCase();

    switch (fieldLowerCase) {
      case 'status':
      case 'species':
      case 'type':
      case 'gender':
        return (char as any)[fieldLowerCase] || 'Unknown';
      case 'origin':
        return char.origin?.name || 'Unknown';
      case 'location':
        return char.location?.name || 'Unknown';
      default:
        return 'Unknown';
    }
  }

  getCharacterGroupedKeys(): string[] {
    return Object.keys(this.charactersGrouped);
  }
}
