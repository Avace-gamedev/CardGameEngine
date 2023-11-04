import {
  CardStatsEffectView,
  CharacterStatsEffectView,
  PassiveEffectView,
} from '../../api/pockedeck-battler-api-client';
import { PassiveEffectType } from './types';

export class PassiveEffectTypeUtils {
  static getType(effect: PassiveEffectView): PassiveEffectType {
    if (effect instanceof CharacterStatsEffectView) {
      return PassiveEffectType.CharacterStats;
    } else if (effect instanceof CardStatsEffectView) {
      return PassiveEffectType.CardStats;
    }

    return PassiveEffectType.None;
  }
}
