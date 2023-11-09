import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  CardPlayedLogEntryView,
  CombatEndedLogEntryView,
  CombatLogEntryView,
  CombatLogView,
  CombatSide,
  ICharacterInCombatView,
  TurnStartedLogEntryView,
} from 'src/app/api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../current-combat.service';

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

  constructor(private currentCombatService: CurrentCombatService) {}

  protected getType(entry: CombatLogEntryView) {
    if (entry instanceof TurnStartedLogEntryView) {
      return EntryType.TurnStarted;
    } else if (entry instanceof CardPlayedLogEntryView) {
      return EntryType.PlayCard;
    } else if (entry instanceof CombatEndedLogEntryView) {
      return EntryType.CombatEnded;
    }

    return undefined;
  }

  protected getWinner(entry: CombatEndedLogEntryView) {
    if (entry.winner === CombatSide.None) {
      return undefined;
    }

    return this.currentCombatService.getPlayerName(entry.winner);
  }

  protected readonly EntryType = EntryType;
  protected readonly CombatSide = CombatSide;
}

enum EntryType {
  TurnStarted = 'turn-started',
  PlayCard = 'play-card',
  CombatEnded = 'combat-ended',
}
