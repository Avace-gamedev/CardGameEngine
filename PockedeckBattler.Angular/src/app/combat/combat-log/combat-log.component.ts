import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  CardPlayedLogEntryView,
  CombatEndedLogEntryView,
  CombatLogEntryView,
  CombatLogView,
  CombatSide,
  CombatSideTurnPhase,
  ICharacterInCombatView,
  TriggeredEffectLogEntryView,
} from 'src/app/api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../current-combat.service';

@Component({
  selector: 'app-combat-log',
  templateUrl: './combat-log.component.html',
})
export class CombatLogComponent {
  @Input()
  get log(): CombatLogView | undefined {
    return this._log;
  }
  set log(value: CombatLogView | undefined) {
    this._log = value;
    this.update();
  }
  private _log: CombatLogView | undefined;

  @Output()
  public highlight: EventEmitter<ICharacterInCombatView | undefined> = new EventEmitter<
    ICharacterInCombatView | undefined
  >();

  protected playerSide: CombatSide | undefined;
  protected playerName: string | undefined;
  protected opponentSide: CombatSide | undefined;
  protected opponentName: string | undefined;

  constructor(private currentCombatService: CurrentCombatService) {}

  protected getType(entry: CombatLogEntryView) {
    if (entry instanceof CardPlayedLogEntryView) {
      return EntryType.PlayCard;
    } else if (entry instanceof TriggeredEffectLogEntryView) {
      return EntryType.TriggeredEffect;
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

  private update() {
    const combat = this.currentCombatService.get();

    this.playerSide = combat?.player.side;
    this.playerName = combat?.player.playerName;
    this.opponentSide = combat?.opponent.side;
    this.opponentName = combat?.opponent.playerName;
  }

  protected readonly EntryType = EntryType;
  protected readonly CombatSide = CombatSide;
  protected readonly CombatSideTurnPhase = CombatSideTurnPhase;
}

enum EntryType {
  PlayCard = 'play-card',
  TriggeredEffect = 'triggered-effect',
  CombatEnded = 'combat-ended',
}
