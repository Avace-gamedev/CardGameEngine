import { Component, Input } from '@angular/core';
import {
  TriggeredEffectInstanceView,
  TriggerMoment,
  TurnTriggerStateView,
  TurnTriggerView,
} from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-triggered-effect-instance',
  templateUrl: './triggered-effect-instance.component.html',
})
export class TriggeredEffectInstanceComponent {
  @Input()
  get effect(): TriggeredEffectInstanceView | undefined {
    return this._effect;
  }

  set effect(value: TriggeredEffectInstanceView | undefined) {
    this._effect = value;
    this.update();
  }

  private _effect: TriggeredEffectInstanceView | undefined;

  protected triggerString: string | undefined;

  private update() {
    this.triggerString = undefined;

    if (!this._effect) {
      return;
    }

    if (
      this._effect.triggerState instanceof TurnTriggerStateView &&
      this._effect.effect.trigger instanceof TurnTriggerView
    ) {
      let trigger = '';
      switch (this._effect.effect.trigger.moment) {
        case TriggerMoment.StartOfSourceTurn:
          trigger = 'start of ' + this._effect.source + ' turn';
          break;
        case TriggerMoment.EndOfSourceTurn:
          trigger = 'end of ' + this._effect.source + ' turn';
          break;
        case TriggerMoment.StartOfTargetTurn:
          trigger = 'start of turn';
          break;
        case TriggerMoment.EndOfTargetTurn:
          trigger = 'end of turn';
          break;
      }

      if (this._effect.triggerState.triggersIn > 0) {
        if (this._effect.triggerState.remainingDuration > 0) {
          this.triggerString = `in ${this._effect.triggerState.triggersIn} turns, every ${trigger} for ${this._effect.triggerState.remainingDuration} turns`;
        } else {
          this.triggerString = `in ${this._effect.triggerState.triggersIn} turns, at ` + trigger;
        }
      } else {
        if (this._effect.triggerState.remainingDuration > 0) {
          this.triggerString = `every ${trigger} for ${this._effect.triggerState.remainingDuration} turns`;
        }
      }
    }
  }
}
