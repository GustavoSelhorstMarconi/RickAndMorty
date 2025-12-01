import { Component, EventEmitter, inject, Output } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, Subject, switchMap } from 'rxjs';
import { RickAndMorty } from '../../services/rick-and-morty';
import { Episode } from '../../shared/episode.model';

@Component({
  selector: 'app-search-episodes',
  imports: [],
  templateUrl: './search-episodes.html',
})
export class SearchEpisodes {
  @Output() episodeSelected = new EventEmitter<number>();
  
  searchText: string = '';
  results: Episode[] = [];
  loading: boolean = false;

  private searchSubject = new Subject<string>();
  private readonly _episodeService = inject(RickAndMorty);

  constructor() {
    this.searchSubject
      .pipe(
        debounceTime(400),
        switchMap((text) => {
          this.loading = true;
          return this._episodeService.searchEpisodes(text);
        })
      )
      .pipe(takeUntilDestroyed())
      .subscribe({
        next: (episodes: Episode[]) => {
          this.results = episodes ?? [];
          this.loading = false;
        },
        error: () => {
          this.results = [];
          this.loading = false;
        },
      });
  }

  onSearch(event: Event) {
    const input = event.target as HTMLInputElement;
    this.searchText = input.value.trim();

    if (this.searchText.length === 0) {
      this.results = [];
      return;
    }

    this.searchSubject.next(this.searchText);
  }

  selectEpisode(episode: Episode) {
    this.episodeSelected.emit(episode.id);
  }
}
