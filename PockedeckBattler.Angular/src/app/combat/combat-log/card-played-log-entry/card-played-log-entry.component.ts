import { Component, Input } from '@angular/core';
import {
  AddEnchantmentEffectOnCharacterLogEntryView,
  CardPlayedLogEntryView,
  CharacterIdentity,
  CharacterInCombatView,
  DamageEffectOnCharacterLogEntryView,
  EffectOnCharacterLogEntryView,
  HealEffectOnCharacterLogEntryView,
  IDamageReceived,
  IEnchantmentView,
  IHealReceived,
  IShieldReceived,
  ShieldEffectOnCharacterLogEntryView,
} from '../../../api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../../current-combat.service';

@Component({
  selector: 'app-card-played-log-entry',
  templateUrl: './card-played-log-entry.component.html',
})
export class CardPlayedLogEntryComponent {
  @Input()
  get entry(): CardPlayedLogEntryView | undefined {
    return this._entry;
  }

  set entry(value: CardPlayedLogEntryView | undefined) {
    this._entry = value;
    this.update();
  }

  private _entry: CardPlayedLogEntryView | undefined;

  protected source: CharacterIdentity | undefined;
  protected effects: CombatLogEffectGroup[] = [];

  constructor(private currentCombatService: CurrentCombatService) {}

  private update() {
    this.effects = [];

    if (!this._entry) {
      return;
    }

    this.source = this.getIdentity(this._entry.source);

    let currentEffect: Effect | undefined;
    let currentTargets: CharacterInCombatView[] = [];
    for (const effectOnCharacter of this._entry.effects) {
      const effect = this.getEffect(effectOnCharacter);
      if (!effect) {
        continue;
      }

      if (!currentEffect) {
        currentEffect = effect;
        currentTargets = [effectOnCharacter.character];
      } else if (this.effectEquals(effect, currentEffect)) {
        currentTargets.push(effectOnCharacter.character);
      } else {
        this.effects.push({
          effect: currentEffect,
          targets: currentTargets
            .map((c) => this.getIdentity(c))
            .filter((c) => !!c)
            .map((c) => c!),
        });
        currentEffect = undefined;
        currentTargets = [];
      }
    }

    if (currentEffect != undefined) {
      this.effects.push({
        effect: currentEffect,
        targets: currentTargets
          .map((c) => this.getIdentity(c))
          .filter((c) => !!c)
          .map((c) => c!),
      });
    }
  }

  private getEffect(effect: EffectOnCharacterLogEntryView): Effect | undefined {
    if (effect instanceof DamageEffectOnCharacterLogEntryView) {
      return { ...effect.damage, type: 'damage' };
    } else if (effect instanceof HealEffectOnCharacterLogEntryView) {
      return { ...effect.heal, type: 'heal' };
    } else if (effect instanceof ShieldEffectOnCharacterLogEntryView) {
      return { ...effect.shield, type: 'shield' };
    } else if (effect instanceof AddEnchantmentEffectOnCharacterLogEntryView) {
      return { ...effect.enchantment, type: 'enchantment' };
    }

    return undefined;
  }

  private effectEquals(effect: Effect, currentEffect: Effect) {
    if (effect.type === 'damage' && currentEffect.type === 'damage') {
      return effect.shield === currentEffect.shield && effect.health === currentEffect.health;
    } else if (effect.type === 'heal' && currentEffect.type === 'heal') {
      return effect.health === currentEffect.health;
    } else if (effect.type === 'shield' && currentEffect.type === 'shield') {
      return effect.shield === currentEffect.shield;
    } else if (effect.type === 'enchantment' && currentEffect.type === 'enchantment') {
      return effect.name === currentEffect.name;
    }

    return undefined;
  }

  private getIdentity(character: CharacterInCombatView) {
    return this.currentCombatService.getCharacter(character.name, character.side)?.character.identity;
  }
}

interface CombatLogEffectGroup {
  readonly targets: readonly CharacterIdentity[];
  readonly effect: Effect;
}

type DamageEffect = IDamageReceived & { type: 'damage' };
type HealEffect = IHealReceived & { type: 'heal' };
type ShieldEffect = IShieldReceived & { type: 'shield' };
type EnchantmentEffect = IEnchantmentView & { type: 'enchantment' };

type Effect = DamageEffect | HealEffect | ShieldEffect | EnchantmentEffect;
