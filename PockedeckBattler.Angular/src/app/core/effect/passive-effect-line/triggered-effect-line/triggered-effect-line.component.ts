import { Component, Input } from '@angular/core';
import { TriggeredEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-triggered-effect-line',
  templateUrl: './triggered-effect-line.component.html',
  styleUrls: ['./triggered-effect-line.component.css'],
})
export class TriggeredEffectLineComponent {
  @Input()
  public effect: TriggeredEffectView | undefined;
}
