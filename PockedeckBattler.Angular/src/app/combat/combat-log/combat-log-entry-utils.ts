import {
  AddEnchantmentEffectOnCharacterLogEntryView,
  CharacterDiedLogEntryView,
  CharacterInCombatView,
  DamageEffectOnCharacterLogEntryView,
  EffectOnCharacterLogEntryView,
  HealEffectOnCharacterLogEntryView,
  ICharacterCombatView,
  ICharacterInCombatView,
  IDamageReceived,
  IEnchantmentView,
  IHealReceived,
  IShieldReceived,
  ShieldEffectOnCharacterLogEntryView,
} from '../../api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../current-combat.service';

export class CombatLogEntryUtils {
  static computeEffectGroups(
    currentCombatService: CurrentCombatService,
    effects: EffectOnCharacterLogEntryView[]
  ): CombatLogEffectGroup[] {
    const groups: CombatLogEffectGroup[] = [];

    let currentEffect: Effect | undefined;
    let currentTargets: CharacterInCombatView[] = [];
    for (const effectOnCharacter of effects) {
      const effect = this.getEffect(effectOnCharacter);
      if (!effect) {
        continue;
      }

      if (currentEffect) {
        if (this.effectEquals(effect, currentEffect)) {
          currentTargets.push(effectOnCharacter.character);
        } else {
          groups.push({
            effect: currentEffect,
            targets: currentTargets
              .map((c) => ({ character: c, identity: this.getCharacterState(currentCombatService, c) }))
              .filter((c) => !!c.identity)
              .map((c) => ({ character: c.character, identity: c.identity! }))
              .map((c) => ({ ...c.character, ...c!.identity })),
          });
          currentEffect = undefined;
          currentTargets = [];
        }
      }

      if (!currentEffect) {
        currentEffect = effect;
        currentTargets = [effectOnCharacter.character];
      }
    }

    if (currentEffect != undefined) {
      groups.push({
        effect: currentEffect,
        targets: currentTargets
          .map((c) => ({ character: c, identity: this.getCharacterState(currentCombatService, c) }))
          .filter((c) => !!c.identity)
          .map((c) => ({ character: c.character, identity: c.identity! }))
          .map((c) => ({ ...c.character, ...c!.identity })),
      });
    }

    return groups;
  }

  static getCharacterState(
    currentCombatService: CurrentCombatService,
    character: CharacterInCombatView
  ): (ICharacterCombatView & ICharacterInCombatView) | undefined {
    const state = currentCombatService.getCharacter(character.name, character.side);
    if (!state) {
      return undefined;
    }

    return { ...character, ...state };
  }

  private static getEffect(effect: EffectOnCharacterLogEntryView): Effect | undefined {
    if (effect instanceof DamageEffectOnCharacterLogEntryView) {
      return { ...effect.damage, type: 'damage' };
    } else if (effect instanceof HealEffectOnCharacterLogEntryView) {
      return { ...effect.heal, type: 'heal' };
    } else if (effect instanceof ShieldEffectOnCharacterLogEntryView) {
      return { ...effect.shield, type: 'shield' };
    } else if (effect instanceof AddEnchantmentEffectOnCharacterLogEntryView) {
      return { ...effect.enchantment, type: 'enchantment' };
    } else if (effect instanceof CharacterDiedLogEntryView) {
      return { type: 'death' };
    }

    return undefined;
  }

  private static effectEquals(effect: Effect, currentEffect: Effect) {
    if (effect.type === 'damage' && currentEffect.type === 'damage') {
      return effect.shield === currentEffect.shield && effect.health === currentEffect.health;
    } else if (effect.type === 'heal' && currentEffect.type === 'heal') {
      return effect.health === currentEffect.health;
    } else if (effect.type === 'shield' && currentEffect.type === 'shield') {
      return effect.shield === currentEffect.shield;
    } else if (effect.type === 'enchantment' && currentEffect.type === 'enchantment') {
      return effect.name === currentEffect.name;
    } else if (effect.type === 'death' && currentEffect.type === 'death') {
      return true;
    }

    return undefined;
  }
}

export interface CombatLogEffectGroup {
  readonly targets: readonly (ICharacterCombatView & ICharacterInCombatView)[];
  readonly effect: Effect;
}

export type DamageEffect = IDamageReceived & { type: 'damage' };
export type HealEffect = IHealReceived & { type: 'heal' };
export type ShieldEffect = IShieldReceived & { type: 'shield' };
export type EnchantmentEffect = IEnchantmentView & { type: 'enchantment' };
export type DeathEffect = { type: 'death' };

export type Effect = DamageEffect | HealEffect | ShieldEffect | EnchantmentEffect | DeathEffect;
