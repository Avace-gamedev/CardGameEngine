import { Component, Input } from '@angular/core';
import { AssetIcon } from '../asset-icon/asset-icons';
import { AssetIconSize } from '../asset-icon/asset-icon.component';
import { StatEffectUtils } from './stat-effect';
import { CharacterStatEffectType } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-stat-effect-icon',
  templateUrl: './character-stats-effect-icon.component.html',
})
export class CharacterStatsEffectIconComponent {
  @Input()
  get effect(): CharacterStatEffectType | undefined {
    return this._effect;
  }
  set effect(value: CharacterStatEffectType | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: CharacterStatEffectType | undefined;

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
