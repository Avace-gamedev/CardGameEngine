import { Component, Input } from '@angular/core';
import {
  CharacterCombatView,
  PassiveEffectInstanceView,
  PassiveStatsModifierView,
} from '../../../api/pockedeck-battler-api-client';
import { CombatCharacterImageSize } from '../../../core/character/character-img/character-img.component';

@Component({
  selector: 'app-character-combat-effects',
  templateUrl: './character-combat-effects.component.html',
  styles: [':host { display: contents }'],
})
export class CharacterCombatEffectsComponent {
  @Input()
  get character(): CharacterCombatView | undefined {
    return this._character;
  }
  set character(value: CharacterCombatView | undefined) {
    this._character = value;
    this.update();
  }
  private _character: CharacterCombatView | undefined;

  @Input()
  public size: CombatCharacterImageSize = 'md';

  @Input()
  public mode: 'col' | 'row' = 'row';

  protected statsModifiers: PassiveEffectInstanceView[] = [];

  private update() {
    this.statsModifiers = [];

    if (!this._character) {
      return;
    }

    this.statsModifiers = this._character.passiveEffects.filter((e) => e.effect instanceof PassiveStatsModifierView);
  }
}
