import { Component, Input } from '@angular/core';
import { PassiveEffectInstanceView, PassiveStatsModifierView } from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../../core/icons/asset-icon/asset-icons';
import { StatEffectUtils } from '../../../../core/icons/stat-effect-icon/stat-effect';

@Component({
  selector: 'app-passive-effect-instance',
  templateUrl: './passive-effect-instance.component.html',
})
export class PassiveEffectInstanceComponent {
  @Input()
  get effect(): PassiveEffectInstanceView | undefined {
    return this._effect;
  }
  set effect(value: PassiveEffectInstanceView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: PassiveEffectInstanceView | undefined;

  protected icon: AssetIcon | undefined;
  protected name: string | undefined;

  private update() {
    this.icon = undefined;
    this.name = undefined;

    if (!this._effect || !(this._effect?.effect instanceof PassiveStatsModifierView)) {
      return;
    }

    this.icon = StatEffectUtils.getIcon(this._effect.effect.effect);
    this.name = StatEffectUtils.getName(this._effect.effect.effect);
  }
}
