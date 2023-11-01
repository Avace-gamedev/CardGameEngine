import { Component, Input } from '@angular/core';
import { PassiveStatsModifierView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-stats-passive-effect-line',
  templateUrl: './stats-passive-effect-line.component.html',
  styleUrls: ['./stats-passive-effect-line.component.css'],
})
export class StatsPassiveEffectLineComponent {
  @Input()
  public effect: PassiveStatsModifierView | undefined;
}
