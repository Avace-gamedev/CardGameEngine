import {
  PassiveEffectView,
  PassiveStatsModifierView,
  TriggeredEffectView,
} from '../../../api/pockedeck-battler-api-client';
import { PassiveEffectType } from './types';

export class PassiveEffectTypeUtils {
  static getType(effect: PassiveEffectView): PassiveEffectType {
    if (effect instanceof PassiveStatsModifierView) {
      return PassiveEffectType.Stats;
    } else if (effect instanceof TriggeredEffectView) {
      return PassiveEffectType.Triggered;
    }

    return PassiveEffectType.None;
  }
}
