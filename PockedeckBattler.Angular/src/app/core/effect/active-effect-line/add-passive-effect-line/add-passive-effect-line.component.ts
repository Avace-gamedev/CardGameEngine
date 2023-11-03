import { Component, Input } from '@angular/core';
import { AddPassiveEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-add-passive-effect-line',
  templateUrl: './add-passive-effect-line.component.html',
})
export class AddPassiveEffectLineComponent {
  @Input()
  public effect: AddPassiveEffectView | undefined;
}
