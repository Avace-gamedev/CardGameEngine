import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  ICharacterCombatView,
  ICharacterInCombatView,
  TriggeredEffectLogEntryView,
} from '../../../api/pockedeck-battler-api-client';
import { CombatLogEffectGroup, CombatLogEntryUtils } from '../combat-log-entry-utils';
import { CurrentCombatService } from '../../current-combat.service';

@Component({
  selector: 'app-triggered-effect-log-entry',
  templateUrl: './triggered-effect-log-entry.component.html',
})
export class TriggeredEffectLogEntryComponent {
  @Input()
  get entry(): TriggeredEffectLogEntryView | undefined {
    return this._entry;
  }
  set entry(value: TriggeredEffectLogEntryView | undefined) {
    this._entry = value;
    this.update();
  }
  private _entry: TriggeredEffectLogEntryView | undefined;

  @Output()
  public highlight: EventEmitter<ICharacterInCombatView | undefined> = new EventEmitter<
    ICharacterInCombatView | undefined
  >();

  protected source: (ICharacterCombatView & ICharacterInCombatView) | undefined;
  protected target: (ICharacterCombatView & ICharacterInCombatView) | undefined;
  protected effects: CombatLogEffectGroup[] = [];

  constructor(private currentCombatService: CurrentCombatService) {}

  private update() {
    this.effects = [];

    if (!this._entry) {
      return;
    }

    this.source = CombatLogEntryUtils.getCharacterState(this.currentCombatService, this._entry.source);
    this.target = CombatLogEntryUtils.getCharacterState(this.currentCombatService, this._entry.target);
    this.effects = CombatLogEntryUtils.computeEffectGroups(this.currentCombatService, this._entry.effectsOnCharacters);
  }
}
