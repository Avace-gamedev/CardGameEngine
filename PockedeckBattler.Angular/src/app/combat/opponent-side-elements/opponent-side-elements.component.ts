import { Component, Input } from '@angular/core';
import { PlayerCombatView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-opponent-side-elements',
  templateUrl: './opponent-side-elements.component.html',
  styleUrls: ['./opponent-side-elements.component.css'],
})
export class OpponentSideElementsComponent {
  @Input()
  public combat: PlayerCombatView | undefined;
}
