import { Component, Input } from '@angular/core';
import { AssetIcon } from '../asset-icon/asset-icons';
import { AssetIconSize } from '../asset-icon/asset-icon.component';
import { StatEffectUtils } from './stat-effect';
import { CardStatEffectType } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-card-stat-effect-icon',
  templateUrl: './card-stats-effect-icon.component.html',
})
export class CardStatsEffectIconComponent {
  @Input()
  get effect(): CardStatEffectType | undefined {
    return this._effect;
  }
  set effect(value: CardStatEffectType | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: CardStatEffectType | undefined;

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

    this.icon = StatEffectUtils.getIcon(this._effect);
    this.tooltip = StatEffectUtils.getName(this._effect);
  }
}
