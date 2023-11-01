import { Component, Input } from '@angular/core';
import { RandomEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-random-effect-line',
  templateUrl: './random-effect-line.component.html',
  styleUrls: ['./random-effect-line.component.css'],
})
export class RandomEffectLineComponent {
  @Input()
  public effect: RandomEffectView | undefined;
}
