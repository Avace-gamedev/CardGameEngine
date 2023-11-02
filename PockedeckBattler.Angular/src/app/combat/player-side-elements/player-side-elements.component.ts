import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  CombatSide,
  PlayerCombatView,
} from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-player-side-elements',
  templateUrl: './player-side-elements.component.html',
  styleUrls: ['./player-side-elements.component.css'],
})
export class PlayerSideElementsComponent {
  @Input()
  public combat: PlayerCombatView | undefined;

  @Output()
  public play: EventEmitter<number> = new EventEmitter<number>();

  @Output()
  public endTurn: EventEmitter<void> = new EventEmitter<void>();

  protected readonly CombatSide = CombatSide;
}
