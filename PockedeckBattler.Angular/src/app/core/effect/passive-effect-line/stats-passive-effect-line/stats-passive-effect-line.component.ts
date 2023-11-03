import { Component, Input } from '@angular/core';
import { PassiveStatsModifierView } from '../../../../api/pockedeck-battler-api-client';

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

  protected modifiers: ModifierToDisplay[] = [];

  private update() {
    this.modifiers = [];

    if (this.effect === undefined) {
      return;
    }

    this.modifiers.push({
      value: this.effect.statsModifier.apCostAdditiveModifier,
      name: 'AP Cost',
      duration: this.effect.duration,
    });

    this.modifiers.push({
      value: this.effect.statsModifier.damageAdditiveModifier,
      name: 'damage',
      duration: this.effect.duration,
    });

    this.modifiers.push({
      value: this.effect.statsModifier.damageReductionAdditiveModifier,
      name: 'resistance',
      duration: this.effect.duration,
    });

    this.modifiers.push({
      value: this.effect.statsModifier.healthAdditiveModifier,
      name: 'HP',
      duration: this.effect.duration,
    });
  }
}

interface ModifierToDisplay {
  readonly value: number;
  readonly name: string;
  readonly duration: number;
}
