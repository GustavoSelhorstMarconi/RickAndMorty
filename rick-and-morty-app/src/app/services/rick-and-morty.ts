import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Character } from '../shared/character.model';
import { Episode } from '../shared/episode.model';

@Injectable({
  providedIn: 'root',
})
export class RickAndMorty {
  private baseUrl = environment.apiBaseUrl + 'RickAndMorty';
  private readonly _httpClient = inject(HttpClient);

  searchEpisodes(search: string) {
    let params = new HttpParams();

    if (search) {
      params = params.set('name', search);
    }

    return this._httpClient.get<Episode[]>(`${this.baseUrl}/search`, {
      params,
    });
  }

  getCharactersByEpisode(episodeId: number) {
    return this._httpClient.get<Character[]>(
      `${this.baseUrl}/${episodeId}`
    );
  }
}
