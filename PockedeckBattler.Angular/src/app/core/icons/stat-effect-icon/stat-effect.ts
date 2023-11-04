import { AssetIcon } from '../asset-icon/asset-icons';
import { CardStatEffectType, CharacterStatEffectType } from '../../../api/pockedeck-battler-api-client';

export class StatEffectUtils {
  static getIcon(effect: CharacterStatEffectType): AssetIcon;
  static getIcon(effect: CardStatEffectType): AssetIcon;
  static getIcon(effect: CharacterStatEffectType | CardStatEffectType): AssetIcon {
    switch (effect) {
      case CardStatEffectType.IncreaseApCost:
        return 'dice-increase';
      case CardStatEffectType.ReduceApCost:
        return 'dice-decrease';
      case CardStatEffectType.IncreaseDamage:
        return 'biceps';
      case CardStatEffectType.ReduceDamage:
        return 'broken-axe';
      case CharacterStatEffectType.IncreaseResistance:
        return 'shield';
      case CharacterStatEffectType.ReduceResistance:
        return 'cracked-shield';
    }
  }

  static getName(effect: CharacterStatEffectType): string;
  static getName(effect: CardStatEffectType): string;
  static getName(effect: CharacterStatEffectType | CardStatEffectType): string {
    switch (effect) {
      case CardStatEffectType.IncreaseApCost:
        return 'AP Cost increase';
      case CardStatEffectType.ReduceApCost:
        return 'AP Cost decrease';
      case CardStatEffectType.IncreaseDamage:
        return 'Damage increase';
      case CardStatEffectType.ReduceDamage:
        return 'Damage decrease';
      case CharacterStatEffectType.IncreaseResistance:
        return 'Resistance increase';
      case CharacterStatEffectType.ReduceResistance:
        return 'Resistance decrease';
    }
  }
}
