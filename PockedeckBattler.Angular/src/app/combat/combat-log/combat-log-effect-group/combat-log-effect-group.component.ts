import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CombatLogEffectGroup } from '../combat-log-entry-utils';
import { ICharacterInCombatView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-log-effect-group',
  templateUrl: './combat-log-effect-group.component.html',
})
export class CombatLogEffectGroupComponent {
  @Input()
  public effects: CombatLogEffectGroup[] = [];

  @Output()
  public highlight: EventEmitter<ICharacterInCombatView | undefined> = new EventEmitter<
    ICharacterInCombatView | undefined
  >();
}
