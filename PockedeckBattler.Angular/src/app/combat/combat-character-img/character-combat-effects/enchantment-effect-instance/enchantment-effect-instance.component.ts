import { Component, Input } from '@angular/core';
import { EnchantmentInstanceView, TurnTriggerStateView } from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../../core/icons/asset-icon/asset-icons';
import { EnchantmentIconUtils } from '../../../../core/icons/enchantment-icon/enchantment-icon-utils';

@Component({
  selector: 'app-enchantment-effect-instance',
  templateUrl: './enchantment-effect-instance.component.html',
})
export class EnchantmentEffectInstanceComponent {
  @Input()
  get effect(): EnchantmentInstanceView | undefined {
    return this._effect;
  }

  set effect(value: EnchantmentInstanceView | undefined) {
    this._effect = value;
    this.update();
  }

  private _effect: EnchantmentInstanceView | undefined;

  protected icon: AssetIcon | undefined;
  protected duration: number | undefined;

  private update() {
    if (!this._effect) {
      this.icon = undefined;
      this.duration = undefined;
      return;
    }

    this.icon = EnchantmentIconUtils.getIcon(this._effect.enchantment);

    let duration = 0;

    for (const passive of this._effect.passive) {
      duration = Math.max(duration, passive.remainingDuration);
    }

    for (const triggered of this._effect.triggered) {
      if (triggered.triggerState instanceof TurnTriggerStateView) {
        duration = Math.max(duration, triggered.triggerState.triggersIn + triggered.triggerState.remainingDuration);
      }
    }

    this.duration = duration > 0 ? duration : undefined;
  }
}
