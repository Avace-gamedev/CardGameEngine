import {
  PassiveEffectView,
  PassiveStatsModifierView,
} from '../../../api/pockedeck-battler-api-client';
import { PassiveEffectType } from './types';

export class PassiveEffectTypeUtils {
  static getType(effect: PassiveEffectView): PassiveEffectType {
    if (effect instanceof PassiveStatsModifierView) {
      return PassiveEffectType.Stats;
    }

    return PassiveEffectType.None;
  }
}
