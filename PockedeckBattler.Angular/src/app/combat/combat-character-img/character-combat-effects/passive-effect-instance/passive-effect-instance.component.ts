import { Component, Input } from '@angular/core';
import { PassiveEffectInstanceView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-passive-effect-instance',
  templateUrl: './passive-effect-instance.component.html',
})
export class PassiveEffectInstanceComponent {
  @Input()
  public effect: PassiveEffectInstanceView | undefined;
}
