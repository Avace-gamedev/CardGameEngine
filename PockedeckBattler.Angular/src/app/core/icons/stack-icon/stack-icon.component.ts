import { Component, Input } from '@angular/core';
import { AssetIcon } from '../asset-icon/asset-icons';

@Component({
  selector: 'app-stack-icon',
  templateUrl: './stack-icon.component.html',
})
export class StackIconComponent {
  @Input()
  get value(): number | undefined {
    return this._value;
  }
  set value(value: number | undefined) {
    this._value = value;
    this.update();
  }
  private _value: number | undefined;

  @Input()
  public enableTooltip: boolean = false;

  protected assetIcon: AssetIcon | undefined;
  protected valueString: number | undefined;
  protected tooltipString: string | undefined;

  private update() {
    this.assetIcon = this.getAssetIcon();
    this.valueString = !!this.assetIcon ? undefined : this._value;
    this.tooltipString = this._value + ' turns';
  }

  private getAssetIcon(): AssetIcon | undefined {
    if (!this._value) {
      return undefined;
    }

    if (this._value >= 1 && this._value <= 6) {
      return ('dice-' + this._value + '-6') as AssetIcon;
    }

    return undefined;
  }
}
