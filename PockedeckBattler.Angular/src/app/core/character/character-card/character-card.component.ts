import { Component, Input } from '@angular/core';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';
import { PlacementArray } from '@ng-bootstrap/ng-bootstrap/util/positioning';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
})
export class CharacterCardComponent {
  @Input()
  public character: CharacterView | undefined;

  @Input()
  public contentOnly: boolean = false;

  @Input()
  public enablePopoverDetails: boolean = false;

  @Input()
  public popoverDetailsPlacement: PlacementArray = 'auto';
}
