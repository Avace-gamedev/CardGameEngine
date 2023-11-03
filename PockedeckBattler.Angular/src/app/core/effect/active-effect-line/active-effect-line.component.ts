import { Component, Input } from '@angular/core';
import { ActiveEffectView } from '../../../api/pockedeck-battler-api-client';
import { ActiveEffectTypeUtils } from '../../../shared/utils/active-effect-type-utils';
import { ActiveEffectType } from '../../../shared/utils/types';

@Component({
  selector: 'app-active-effect-line',
  templateUrl: './active-effect-line.component.html',
})
export class ActiveEffectLineComponent {
  @Input()
  get effect(): ActiveEffectView | undefined {
    return this._effect;
  }
  set effect(value: ActiveEffectView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: ActiveEffectView | undefined;

  protected effectType: ActiveEffectType | undefined;

  private update() {
    this.effectType = this.effect ? ActiveEffectTypeUtils.getType(this.effect) : undefined;
  }

  protected readonly ActiveEffectType = ActiveEffectType;
}
