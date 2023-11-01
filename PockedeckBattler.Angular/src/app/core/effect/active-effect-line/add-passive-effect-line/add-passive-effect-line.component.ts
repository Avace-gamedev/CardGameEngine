import { Component, Input } from '@angular/core';
import { AddPassiveEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-add-passive-effect-line',
  templateUrl: './add-passive-effect-line.component.html',
  styleUrls: ['./add-passive-effect-line.component.css'],
})
export class AddPassiveEffectLineComponent {
  @Input()
  public effect: AddPassiveEffectView | undefined;
}
