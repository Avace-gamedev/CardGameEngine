import { Component, Input } from '@angular/core';
import { CharacterStatEffectType, CharacterStatsEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-stat-effect-line',
  templateUrl: './character-stat-effect-line.component.html',
})
export class CharacterStatEffectLineComponent {
  @Input()
  get effect(): CharacterStatsEffectView | undefined {
    return this._effect;
  }
  set effect(value: CharacterStatsEffectView | undefined) {
    this._effect = value;
    this.update();
  }
  private _effect: CharacterStatsEffectView | undefined;

  @Input()
  public overrideDuration: number | undefined;

  protected display: ModifierToDisplay | undefined;

  private update() {
    if (this.effect === undefined) {
      this.display = undefined;
      return;
    }

    let name: string;
    switch (this.effect.type) {
      case CharacterStatEffectType.IncreaseResistance:
      case CharacterStatEffectType.ReduceResistance:
        name = 'resistance';
        break;
    }

    this.display = { name, value: this.effect.amount, duration: this.overrideDuration ?? this.effect.duration };
  }
}

interface ModifierToDisplay {
  readonly value: number;
  readonly name: string;
  readonly duration: number;
}
