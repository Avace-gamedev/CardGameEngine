import { Component, Input } from '@angular/core';
import {
  TriggeredEffectInstanceView,
  TriggerMoment,
  TurnTriggerStateView,
  TurnTriggerView,
} from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-triggered-effect-instance-line',
  templateUrl: './triggered-effect-instance-line.component.html',
})
export class TriggeredEffectInstanceLineComponent {
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
  protected duration: number | undefined;

  private update() {
    this.triggerString = undefined;

    if (!this._effect) {
      return;
    }

    if (
      this._effect.triggerState instanceof TurnTriggerStateView &&
      this._effect.effect.trigger instanceof TurnTriggerView
    ) {
      this.triggerString = this.computeTurnTriggerString(
        this._effect.source.name,
        this._effect.triggerState,
        this._effect.effect.trigger
      );

      this.duration =
        this._effect.triggerState.triggersIn > 0
          ? this._effect.triggerState.triggersIn
          : this._effect.triggerState.remainingDuration;
    }
  }

  private computeTurnTriggerString(source: string, triggerState: TurnTriggerStateView, trigger: TurnTriggerView) {
    let triggerStr = '';
    switch (trigger.moment) {
      case TriggerMoment.StartOfSourceTurn:
        triggerStr = 'start of ' + source + ' turn';
        break;
      case TriggerMoment.EndOfSourceTurn:
        triggerStr = 'end of ' + source + ' turn';
        break;
      case TriggerMoment.StartOfTargetTurn:
        triggerStr = 'start of turn';
        break;
      case TriggerMoment.EndOfTargetTurn:
        triggerStr = 'end of turn';
        break;
    }

    if (triggerState.triggersIn > 0) {
      if (triggerState.remainingDuration > 1) {
        return `in ${triggerState.triggersIn} turns, every ${triggerStr} for ${triggerState.remainingDuration} turns`;
      } else {
        return `in ${triggerState.triggersIn} turns, at ` + triggerStr;
      }
    } else {
      if (triggerState.remainingDuration > 0) {
        return `every ${triggerStr} for ${triggerState.remainingDuration} turns`;
      }
    }

    return undefined;
  }
}
