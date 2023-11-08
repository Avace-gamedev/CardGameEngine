import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  CardPlayedLogEntryView,
  CombatLogEntryView,
  CombatLogView,
  ICharacterInCombatView,
} from 'src/app/api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-log',
  templateUrl: './combat-log.component.html',
})
export class CombatLogComponent {
  @Input()
  public log: CombatLogView | undefined;

  @Output()
  public highlight: EventEmitter<ICharacterInCombatView | undefined> = new EventEmitter<
    ICharacterInCombatView | undefined
  >();

  protected getType(entry: CombatLogEntryView) {
    if (entry instanceof CardPlayedLogEntryView) {
      return EntryType.PlayCard;
    }

    return undefined;
  }

  protected readonly EntryType = EntryType;
}

enum EntryType {
  PlayCard = 'play-card',
}
