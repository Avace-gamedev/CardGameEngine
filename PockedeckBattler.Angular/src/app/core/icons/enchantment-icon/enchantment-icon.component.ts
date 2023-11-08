import { Component, Input } from '@angular/core';
import { EnchantmentView } from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { AssetIconSize } from '../asset-icon/asset-icon.component';
import { EnchantmentIconUtils } from './enchantment-icon-utils';
import { Color } from '../../../shared/utils/colors';

@Component({
  selector: 'app-enchantment-icon',
  templateUrl: './enchantment-icon.component.html',
})
export class EnchantmentIconComponent {
  @Input()
  get enchantment(): EnchantmentView | undefined {
    return this._enchantment;
  }
  set enchantment(value: EnchantmentView | undefined) {
    this._enchantment = value;
    this.update();
  }
  private _enchantment: EnchantmentView | undefined;

  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;

  @Input()
  public color: Color | undefined;

  protected assetIcon: AssetIcon | undefined;

  private update() {
    this.assetIcon = undefined;

    if (!this._enchantment) {
      return;
    }

    this.assetIcon = EnchantmentIconUtils.getIcon(this._enchantment);
  }
}
