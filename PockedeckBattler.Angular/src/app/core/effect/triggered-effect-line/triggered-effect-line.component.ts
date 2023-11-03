import { Component, Input } from '@angular/core';
import {
  TriggeredEffectView,
  TriggerMoment,
  TurnTriggerView,
} from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-triggered-effect-line',
  templateUrl: './triggered-effect-line.component.html',
})
export class TriggeredEffectLineComponent {
  @Input()
  get effect(): TriggeredEffectView | undefined {
    return this._effect;
  }

  set effect(value: TriggeredEffectView | undefined) {
    this._effect = value;
    this.update();
  }

  private _effect: TriggeredEffectView | undefined;

  protected effectTriggerString: string | undefined;

  private update() {
    if (this.effect) {
      if (this.effect.trigger instanceof TurnTriggerView) {
        this.effectTriggerString = this.getTurnTriggerLine(this.effect.trigger);
        return;
      }
    }

    this.effectTriggerString = undefined;
  }

  private getTurnTriggerLine(turnTrigger: TurnTriggerView) {
    let delay = '';
    if (turnTrigger.initialDelay === 1) {
      delay = `in ${turnTrigger.initialDelay} turn, `;
    } else if (turnTrigger.initialDelay > 1) {
      delay = `in ${turnTrigger.initialDelay} turns, `;
    }

    let duration = '';
    if (turnTrigger.duration > 1) {
      duration = `for ${turnTrigger.duration} turns, `;
    }

    let moment;
    switch (turnTrigger.moment) {
      case TriggerMoment.StartOfSourceTurn:
        moment = 'start';
        break;
      case TriggerMoment.StartOfTargetTurn:
        moment = 'start';
        break;
      case TriggerMoment.EndOfSourceTurn:
        moment = 'end';
        break;
      case TriggerMoment.EndOfTargetTurn:
        moment = 'end';
        break;
    }

    let turnOrEachTurn = turnTrigger.duration > 1 ? 'each turn' : 'turn';

    return `${delay}${duration}at ${moment} of ${turnOrEachTurn}`;
  }
}
