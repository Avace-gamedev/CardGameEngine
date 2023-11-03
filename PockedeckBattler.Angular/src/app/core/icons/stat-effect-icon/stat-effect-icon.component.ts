import { Component, Input } from '@angular/core';
import { StatEffect } from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { AssetIconSize } from '../asset-icon/asset-icon.component';
import { StatEffectUtils } from './stat-effect';

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

    this.icon = StatEffectUtils.getIcon(this._effect);
    this.tooltip = StatEffectUtils.getName(this._effect);
  }
}
