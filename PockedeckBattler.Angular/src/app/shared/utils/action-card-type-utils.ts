import { ActionCardView, EffectView, RandomEffectView } from '../../api/pockedeck-battler-api-client';
import { ActiveEffectTypeUtils } from './active-effect-type-utils';
import { ActiveEffectType, CardType } from './types';

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
          return CardType.Effect;
        }
      case ActiveEffectType.AddEnchantment:
        return CardType.Effect;
    }
  }

  static computeBgColor(card: ActionCardView): string {
    const type = this.getType(card);

    switch (type) {
      case CardType.None:
        return '--other-card-color';
      case CardType.Damage:
        return '--damage-card-color';
      case CardType.Heal:
        return '--heal-card-color';
      case CardType.Shield:
        return '--shield-card-color';
      case CardType.Effect:
        return '--effect-card-color';
    }
  }

  static computeTextColor(card: ActionCardView): string {
    const type = this.getType(card);

    switch (type) {
      case CardType.None:
        return '--text-on-other-card-color';
      case CardType.Damage:
        return '--text-on-damage-card-color';
      case CardType.Heal:
        return '--text-on-heal-card-color';
      case CardType.Shield:
        return '--text-on-shield-card-color';
      case CardType.Effect:
        return '--text-on-effect-card-color';
    }
  }
}
