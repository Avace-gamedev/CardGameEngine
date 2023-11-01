import {
  ActiveEffectView,
  AddPassiveEffectView,
  DamageEffectView,
  HealEffectView,
  RandomEffectView,
  ShieldEffectView,
} from '../../../api/pockedeck-battler-api-client';
import { ActiveEffectType } from './types';

export class ActiveEffectTypeUtils {
  static getType(card: ActiveEffectView): ActiveEffectType {
    if (!card) {
      return ActiveEffectType.None;
    }

    if (card instanceof DamageEffectView) {
      return ActiveEffectType.Damage;
    } else if (card instanceof HealEffectView) {
      return ActiveEffectType.Heal;
    } else if (card instanceof ShieldEffectView) {
      return ActiveEffectType.Shield;
    } else if (card instanceof RandomEffectView) {
      return ActiveEffectType.Random;
    } else if (card instanceof AddPassiveEffectView) {
      return ActiveEffectType.AddPassive;
    }

    return ActiveEffectType.None;
  }
}
