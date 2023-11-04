import { Component, Input } from '@angular/core';
import { CombatSide, PlayerCombatView, TurnTriggerView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-common-elements',
  templateUrl: './combat-common-elements.component.html',
})
export class CombatCommonElementsComponent {
  @Input()
  get combat(): PlayerCombatView | undefined {
    return this._combat;
  }
  set combat(value: PlayerCombatView | undefined) {
    this._combat = value;
    this.update();
  }
  private _combat: PlayerCombatView | undefined;

  protected currentPlayer: string | undefined;

  private update() {
    if (!this._combat) {
      this.currentPlayer = undefined;
      return;
    }

    this.currentPlayer =
      this._combat.currentSide == CombatSide.Left
        ? this._combat.leftPlayerName
        : this._combat.currentSide == CombatSide.Right
        ? this._combat.rightPlayerName
        : undefined;
  }

  protected readonly TurnTriggerView = TurnTriggerView;
}
