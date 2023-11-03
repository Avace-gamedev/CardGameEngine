import { Component, Input } from '@angular/core';
import { PassiveStatsModifierView, StatEffect } from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../icons/asset-icon/asset-icons';

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

    let icon: AssetIcon;
    switch (this.effect.effect) {
      case StatEffect.IncreaseApCost:
        icon = 'dice-increase';
        break;
      case StatEffect.ReduceApCost:
        icon = 'dice-decrease';
        break;
      case StatEffect.IncreaseDamage:
        icon = 'biceps';
        break;
      case StatEffect.ReduceDamage:
        icon = 'broken-axe';
        break;
      case StatEffect.IncreaseResistance:
        icon = 'shield-reflect';
        break;
      case StatEffect.ReduceResistance:
        icon = 'cracked-shield';
        break;
    }

    this.display = { icon, name, value: this.effect.amount, duration: this.effect.duration };
  }
}

interface ModifierToDisplay {
  readonly icon: AssetIcon;
  readonly value: number;
  readonly name: string;
  readonly duration: number;
}
