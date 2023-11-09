import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  CardPlayedLogEntryView,
  CombatSide,
  ICharacterCombatView,
  ICharacterInCombatView,
} from '../../../api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../../current-combat.service';
import { left } from '@popperjs/core';
import { CombatLogEffectGroup, CombatLogEntryUtils } from '../combat-log-entry-utils';

@Component({
  selector: 'app-card-played-log-entry',
  templateUrl: './card-played-log-entry.component.html',
})
export class CardPlayedLogEntryComponent implements OnInit {
  @Input()
  get entry(): CardPlayedLogEntryView | undefined {
    return this._entry;
  }

  set entry(value: CardPlayedLogEntryView | undefined) {
    this._entry = value;
    this.update();
  }

  private _entry: CardPlayedLogEntryView | undefined;

  @Output()
  public highlight: EventEmitter<ICharacterInCombatView | undefined> = new EventEmitter<
    ICharacterInCombatView | undefined
  >();

  protected source: (ICharacterCombatView & ICharacterInCombatView) | undefined;
  protected effects: CombatLogEffectGroup[] = [];
  protected playerSide: CombatSide = CombatSide.None;

  constructor(private currentCombatService: CurrentCombatService) {}

  ngOnInit() {
    this.playerSide = this.currentCombatService.get()?.player.side ?? CombatSide.None;
  }

  private update() {
    this.effects = [];

    if (!this._entry) {
      return;
    }

    this.source = CombatLogEntryUtils.getCharacterState(this.currentCombatService, this._entry.source);
    this.effects = CombatLogEntryUtils.computeEffectGroups(this.currentCombatService, this._entry.effects);
  }

  protected readonly left = left;
}
