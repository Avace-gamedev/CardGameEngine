import { Component, Input } from '@angular/core';
import { ShieldEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-shield-effect-line',
  templateUrl: './shield-effect-line.component.html',
})
export class ShieldEffectLineComponent {
  @Input()
  public effect: ShieldEffectView | undefined;
}
