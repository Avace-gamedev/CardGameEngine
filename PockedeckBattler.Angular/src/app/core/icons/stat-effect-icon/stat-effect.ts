import { StatEffect } from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';

export class StatEffectUtils {
  static getIcon(effect: StatEffect): AssetIcon {
    switch (effect) {
      case StatEffect.IncreaseApCost:
        return 'dice-increase';
      case StatEffect.ReduceApCost:
        return 'dice-decrease';
      case StatEffect.IncreaseDamage:
        return 'biceps';
      case StatEffect.ReduceDamage:
        return 'broken-axe';
      case StatEffect.IncreaseResistance:
        return 'shield';
      case StatEffect.ReduceResistance:
        return 'cracked-shield';
    }
  }

  static getName(effect: StatEffect): string {
    switch (effect) {
      case StatEffect.IncreaseApCost:
        return 'AP Cost increase';
      case StatEffect.ReduceApCost:
        return 'AP Cost decrease';
      case StatEffect.IncreaseDamage:
        return 'Damage increase';
      case StatEffect.ReduceDamage:
        return 'Damage decrease';
      case StatEffect.IncreaseResistance:
        return 'Resistance increase';
      case StatEffect.ReduceResistance:
        return 'Resistance decrease';
    }
  }
}
