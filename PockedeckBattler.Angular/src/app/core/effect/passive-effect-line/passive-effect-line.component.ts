import { Component, Input } from '@angular/core';
import { PassiveEffectView } from '../../../api/pockedeck-battler-api-client';
import { PassiveEffectTypeUtils } from '../../../shared/utils/passive-effect-type-utils';
import { PassiveEffectType } from '../../../shared/utils/types';

@Component({
  selector: 'app-passive-effect-line',
  templateUrl: './passive-effect-line.component.html',
})
export class PassiveEffectLineComponent {
  @Input()
  get effect(): PassiveEffectView | undefined {
    return this._effect;
  }
  set effect(value: PassiveEffectView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: PassiveEffectView | undefined;

  @Input()
  public overrideDuration: number | undefined;

  protected type: PassiveEffectType | undefined;

  private update() {
    this.type = this._effect ? PassiveEffectTypeUtils.getType(this._effect) : undefined;
  }

  protected readonly PassiveEffectType = PassiveEffectType;
}
