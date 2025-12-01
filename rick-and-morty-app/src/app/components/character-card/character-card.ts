import { Component, Input } from '@angular/core';
import { Character } from '../../shared/character.model';

@Component({
  selector: 'app-character-card',
  imports: [],
  templateUrl: './character-card.html',
})
export class CharacterCard {
  @Input() character!: Character;
}
