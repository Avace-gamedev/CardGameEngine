import { Component, Input } from '@angular/core';
import { DamageEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-damage-effect-line',
  templateUrl: './damage-effect-line.component.html',
  styleUrls: ['./damage-effect-line.component.css'],
})
export class DamageEffectLineComponent {
  @Input()
  public effect: DamageEffectView | undefined;
}
