import { Component, Input } from '@angular/core';
import { CharacterCombatView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-side',
  templateUrl: './combat-side.component.html',
  styleUrls: ['./combat-side.component.css'],
})
export class CombatSideComponent {
  @Input()
  public frontCharacter: CharacterCombatView | undefined;

  @Input()
  public backCharacter: CharacterCombatView | undefined;

  @Input()
  public invert: boolean = false;
}
