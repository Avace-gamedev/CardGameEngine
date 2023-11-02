import { Component, Input } from '@angular/core';
import { Placement } from '@ng-bootstrap/ng-bootstrap';
import { CharacterCombatView } from '../../api/pockedeck-battler-api-client';
import { CombatCharacterImageSize } from '../../core/character/character-img/character-img.component';

@Component({
  selector: 'app-combat-character-img',
  templateUrl: './combat-character-img.component.html',
  styleUrls: ['./combat-character-img.component.css'],
})
export class CombatCharacterImgComponent {
  @Input()
  public character: CharacterCombatView | undefined;

  @Input()
  public size: CombatCharacterImageSize = 'md';

  @Input()
  public popoverPlacement: Placement = 'auto';
}
