import { Component, Input } from '@angular/core';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
  styleUrls: ['./character-card.component.css'],
})
export class CharacterCardComponent {
  @Input()
  public character: CharacterView | undefined;
}
