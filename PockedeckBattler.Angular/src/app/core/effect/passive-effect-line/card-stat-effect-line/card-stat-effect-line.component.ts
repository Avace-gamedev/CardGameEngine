import { Component, Input } from '@angular/core';
import { CardStatEffectType, CardStatsEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-card-stat-effect-line',
  templateUrl: './card-stat-effect-line.component.html',
})
export class CardStatEffectLineComponent {
  @Input()
  get effect(): CardStatsEffectView | undefined {
    return this._effect;
  }
  set effect(value: CardStatsEffectView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: CardStatsEffectView | undefined;

  @Input()
  public overrideDuration: number | undefined;

  protected display: ModifierToDisplay | undefined;

  private update() {
    if (this.effect === undefined) {
      this.display = undefined;
      return;
    }

    let sign;
    switch (this.effect.type) {
      case CardStatEffectType.IncreaseApCost:
      case CardStatEffectType.IncreaseDamage:
        sign = 1;
        break;
      case CardStatEffectType.ReduceApCost:
      case CardStatEffectType.ReduceDamage:
        sign = -1;
        break;
    }

    let name: string;
    switch (this.effect.type) {
      case CardStatEffectType.IncreaseApCost:
      case CardStatEffectType.ReduceApCost:
        name = ' AP cost';
        break;
      case CardStatEffectType.IncreaseDamage:
      case CardStatEffectType.ReduceDamage:
        name = ' damage';
        break;
    }

    this.display = { name, value: sign * this.effect.amount, duration: this.overrideDuration ?? this.effect.duration };
  }
}

interface ModifierToDisplay {
  readonly value: number;
  readonly name: string;
  readonly duration: number;
}
