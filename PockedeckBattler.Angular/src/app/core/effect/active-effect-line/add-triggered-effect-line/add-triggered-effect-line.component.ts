import { Component, Input } from '@angular/core';
import { AddTriggeredEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-add-triggered-effect-line',
  templateUrl: './add-triggered-effect-line.component.html',
  styleUrls: ['./add-triggered-effect-line.component.css'],
})
export class AddTriggeredEffectLineComponent {
  @Input()
  public effect: AddTriggeredEffectView | undefined;
}
