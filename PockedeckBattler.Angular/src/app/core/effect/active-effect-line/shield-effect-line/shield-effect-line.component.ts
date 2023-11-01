import { Component, Input } from '@angular/core';
import { ShieldEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-shield-effect-line',
  templateUrl: './shield-effect-line.component.html',
  styleUrls: ['./shield-effect-line.component.css'],
})
export class ShieldEffectLineComponent {
  @Input()
  public effect: ShieldEffectView | undefined;
}
