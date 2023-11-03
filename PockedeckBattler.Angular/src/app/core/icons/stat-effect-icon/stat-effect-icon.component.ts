import { Component, Input } from '@angular/core';
import { StatEffect } from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { AssetIconSize } from '../asset-icon/asset-icon.component';

@Component({
  selector: 'app-stat-effect-icon',
  templateUrl: './stat-effect-icon.component.html',
})
export class StatEffectIconComponent {
  @Input()
  get effect(): StatEffect | undefined {
    return this._effect;
  }
  set effect(value: StatEffect | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: StatEffect | undefined;

  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public disableTooltip: boolean = false;

  protected icon: AssetIcon | undefined;
  protected tooltip: string | undefined;

  private update() {
    this.icon = undefined;
    this.tooltip = undefined;

    if (!this._effect) {
      return;
    }

    switch (this._effect) {
      case StatEffect.IncreaseApCost:
        this.icon = 'dice-increase';
        break;
      case StatEffect.ReduceApCost:
        this.icon = 'dice-decrease';
        break;
      case StatEffect.IncreaseDamage:
        this.icon = 'biceps';
        break;
      case StatEffect.ReduceDamage:
        this.icon = 'broken-axe';
        break;
      case StatEffect.IncreaseResistance:
        this.icon = 'shield-reflect';
        break;
      case StatEffect.ReduceResistance:
        this.icon = 'cracked-shield';
        break;
    }

    switch (this.effect) {
      case StatEffect.IncreaseApCost:
      case StatEffect.ReduceApCost:
        this.tooltip = 'AP cost';
        break;
      case StatEffect.IncreaseDamage:
      case StatEffect.ReduceDamage:
        this.tooltip = 'damage';
        break;
      case StatEffect.IncreaseResistance:
      case StatEffect.ReduceResistance:
        this.tooltip = 'resistance';
        break;
    }
  }
}
