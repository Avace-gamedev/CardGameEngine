import { Component, Input } from '@angular/core';
import { BaseCombatView, CombatSide, TurnTriggerView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-common-elements',
  templateUrl: './combat-common-elements.component.html',
})
export class CombatCommonElementsComponent {
  @Input()
  get combat(): BaseCombatView | undefined {
    return this._combat;
  }
  set combat(value: BaseCombatView | undefined) {
    this._combat = value;
    this.update();
  }
  private _combat: BaseCombatView | undefined;

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
