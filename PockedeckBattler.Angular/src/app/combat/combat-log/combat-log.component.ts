import { Component, Input } from '@angular/core';
import { CardPlayedLogEntryView, CombatLogEntryView, CombatLogView } from 'src/app/api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-log',
  templateUrl: './combat-log.component.html',
})
export class CombatLogComponent {
  @Input()
  public log: CombatLogView | undefined;

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
