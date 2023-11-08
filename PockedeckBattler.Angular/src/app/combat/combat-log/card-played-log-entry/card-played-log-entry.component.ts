import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AddEnchantmentEffectOnCharacterLogEntryView,
  CardPlayedLogEntryView,
  CharacterInCombatView,
  CombatSide,
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
} from '../../../api/pockedeck-battler-api-client';
import { CurrentCombatService } from '../../current-combat.service';
import { left } from '@popperjs/core';

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

    this.source = this.getCharacterState(this._entry.source);

    let currentEffect: Effect | undefined;
    let currentTargets: CharacterInCombatView[] = [];
    for (const effectOnCharacter of this._entry.effects) {
      const effect = this.getEffect(effectOnCharacter);
      if (!effect) {
        continue;
      }

      if (currentEffect) {
        if (this.effectEquals(effect, currentEffect)) {
          currentTargets.push(effectOnCharacter.character);
        } else {
          this.effects.push({
            effect: currentEffect,
            targets: currentTargets
              .map((c) => ({ character: c, identity: this.getCharacterState(c) }))
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
      this.effects.push({
        effect: currentEffect,
        targets: currentTargets
          .map((c) => ({ character: c, identity: this.getCharacterState(c) }))
          .filter((c) => !!c.identity)
          .map((c) => ({ character: c.character, identity: c.identity! }))
          .map((c) => ({ ...c.character, ...c!.identity })),
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

  private getCharacterState(
    character: CharacterInCombatView
  ): (ICharacterCombatView & ICharacterInCombatView) | undefined {
    const state = this.currentCombatService.getCharacter(character.name, character.side);
    if (!state) {
      return undefined;
    }

    return { ...character, ...state };
  }

  protected readonly left = left;
}

interface CombatLogEffectGroup {
  readonly targets: readonly (ICharacterCombatView & ICharacterInCombatView)[];
  readonly effect: Effect;
}

type DamageEffect = IDamageReceived & { type: 'damage' };
type HealEffect = IHealReceived & { type: 'heal' };
type ShieldEffect = IShieldReceived & { type: 'shield' };
type EnchantmentEffect = IEnchantmentView & { type: 'enchantment' };

type Effect = DamageEffect | HealEffect | ShieldEffect | EnchantmentEffect;
