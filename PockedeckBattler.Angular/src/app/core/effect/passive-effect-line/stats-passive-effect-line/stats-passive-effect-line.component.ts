import { Component, Input } from '@angular/core';
import { PassiveStatsModifierView, StatEffect } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-stats-passive-effect-line',
  templateUrl: './stats-passive-effect-line.component.html',
})
export class StatsPassiveEffectLineComponent {
  @Input()
  get effect(): PassiveStatsModifierView | undefined {
    return this._effect;
  }
  set effect(value: PassiveStatsModifierView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: PassiveStatsModifierView | undefined;

  protected display: ModifierToDisplay | undefined;

  private update() {
    if (this.effect === undefined) {
      this.display = undefined;
      return;
    }

    let name: string;
    switch (this.effect.effect) {
      case StatEffect.IncreaseApCost:
      case StatEffect.ReduceApCost:
        name = 'AP cost';
        break;
      case StatEffect.IncreaseDamage:
      case StatEffect.ReduceDamage:
        name = 'damage';
        break;
      case StatEffect.IncreaseResistance:
      case StatEffect.ReduceResistance:
        name = 'resistance';
        break;
    }

    this.display = { name, value: this.effect.amount, duration: this.effect.duration };
  }
}

interface ModifierToDisplay {
  readonly value: number;
  readonly name: string;
  readonly duration: number;
}
