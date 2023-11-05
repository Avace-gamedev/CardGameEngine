import { Component, Input } from '@angular/core';
import {
  CardStatsEffectView,
  CharacterStatsEffectView,
  PassiveEffectInstanceView,
} from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../../core/icons/asset-icon/asset-icons';
import { StatEffectUtils } from '../../../../core/icons/stat-effect-icon/stat-effect';

@Component({
  selector: 'app-passive-effect-instance-line',
  templateUrl: './passive-effect-instance-line.component.html',
})
export class PassiveEffectInstanceLineComponent {
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

    if (!this._effect) {
      return;
    }

    if (this._effect.effect instanceof CharacterStatsEffectView) {
      this.icon = StatEffectUtils.getIcon(this._effect.effect.type);
      this.name = StatEffectUtils.getName(this._effect.effect.type);
    } else if (this._effect.effect instanceof CardStatsEffectView) {
      this.icon = StatEffectUtils.getIcon(this._effect.effect.type);
      this.name = StatEffectUtils.getName(this._effect.effect.type);
    }
  }
}
