import { Component, Input } from '@angular/core';
import { CharacterCombatView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-side-common-elements',
  templateUrl: './combat-side-common-elements.component.html',
  styleUrls: ['./combat-side-common-elements.component.css'],
})
export class CombatSideCommonElementsComponent {
  @Input()
  public frontCharacter: CharacterCombatView | undefined;

  @Input()
  public backCharacter: CharacterCombatView | undefined;

  @Input()
  public invert: boolean = false;
}
