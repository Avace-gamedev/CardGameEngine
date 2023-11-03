import { Component, Input } from '@angular/core';
import { AddTriggeredEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-add-triggered-effect-line',
  templateUrl: './add-triggered-effect-line.component.html',
})
export class AddTriggeredEffectLineComponent {
  @Input()
  public effect: AddTriggeredEffectView | undefined;
}
