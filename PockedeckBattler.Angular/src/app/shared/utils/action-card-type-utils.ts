import {
  ActionCardView,
  DamageEffectView,
  EffectView,
  Element,
  RandomEffectView,
} from '../../api/pockedeck-battler-api-client';
import { ActiveEffectTypeUtils } from './active-effect-type-utils';
import { ActiveEffectType, CardType } from './types';
import { Color } from './colors';

export class ActionCardTypeUtils {
  static getType(card: ActionCardView): CardType {
    if (!card.mainEffect) {
      return CardType.None;
    }

    return this.activeEffectToCardType(card.mainEffect);
  }

  private static activeEffectToCardType(mainEffect: EffectView): CardType {
    const mainEffectType = ActiveEffectTypeUtils.getType(mainEffect);

    switch (mainEffectType) {
      case ActiveEffectType.None:
        return CardType.None;
      case ActiveEffectType.Damage:
        return CardType.Damage;
      case ActiveEffectType.Heal:
        return CardType.Heal;
      case ActiveEffectType.Shield:
        return CardType.Shield;
      case ActiveEffectType.Random:
        const entries = (mainEffect as RandomEffectView)?.entries ?? [];
        const types = entries.map((e) => this.activeEffectToCardType(e.effect));

        if (types.length > 0 && types.every((t) => t === types[0])) {
          return types[0];
        } else {
          return CardType.Enchantment;
        }
      case ActiveEffectType.AddEnchantment:
        return CardType.Enchantment;
    }
  }

  static computeBgColor(card: ActionCardView): Color {
    const type = this.getType(card);

    switch (type) {
      case CardType.None:
        return '--other-card-color';
      case CardType.Damage:
        switch ((card.mainEffect as DamageEffectView).element) {
          case Element.Neutral:
            return '--neutral-color';
          case Element.Fire:
            return '--fire-color';
          case Element.Earth:
            return '--earth-color';
          case Element.Water:
            return '--water-color';
          case Element.Wind:
            return '--wind-color';
        }
        break;
      case CardType.Heal:
        return '--heal-card-color';
      case CardType.Shield:
        return '--shield-card-color';
      case CardType.Enchantment:
        return '--enchantment-card-color';
    }
  }

  static computeTextColor(card: ActionCardView): Color {
    const type = this.getType(card);

    switch (type) {
      case CardType.None:
        return '--text-on-other-card-color';
      case CardType.Damage:
        switch ((card.mainEffect as DamageEffectView).element) {
          case Element.Neutral:
            return '--text-on-neutral-color';
          case Element.Fire:
            return '--text-on-fire-color';
          case Element.Earth:
            return '--text-on-earth-color';
          case Element.Water:
            return '--text-on-water-color';
          case Element.Wind:
            return '--text-on-wind-color';
        }
        break;
      case CardType.Heal:
        return '--text-on-heal-card-color';
      case CardType.Shield:
        return '--text-on-shield-card-color';
      case CardType.Enchantment:
        return '--text-on-enchantment-card-color';
    }
  }
}
