import { Component, Input } from '@angular/core';
import { PassiveEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-stats-modifier-effect',
  templateUrl: './stats-modifier-effect.component.html',
})
export class StatsModifierEffectComponent {
  @Input()
  public effect: PassiveEffectView | undefined;
}
