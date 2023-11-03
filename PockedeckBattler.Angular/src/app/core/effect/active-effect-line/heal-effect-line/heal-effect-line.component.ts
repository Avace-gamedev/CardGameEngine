import { Component, Input } from '@angular/core';
import { HealEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-heal-effect-line',
  templateUrl: './heal-effect-line.component.html',
})
export class HealEffectLineComponent {
  @Input()
  public effect: HealEffectView | undefined;
}
