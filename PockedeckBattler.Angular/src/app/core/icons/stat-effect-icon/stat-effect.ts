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
      case CharacterStatEffectType.IncreaseAllDamages:
        return 'biceps';
      case CharacterStatEffectType.ReduceAllDamages:
        return 'broken-axe';
      case CharacterStatEffectType.IncreaseAllResistances:
        return 'shield';
      case CharacterStatEffectType.ReduceAllResistances:
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
        return 'Increase card damage';
      case CardStatEffectType.ReduceDamage:
        return 'Decrease card damage';
      case CharacterStatEffectType.IncreaseAllDamages:
        return 'Increase all damages';
      case CharacterStatEffectType.ReduceAllDamages:
        return 'Decrease all damages';
      case CharacterStatEffectType.IncreaseAllResistances:
        return 'Increase all resistances';
      case CharacterStatEffectType.ReduceAllResistances:
        return 'Decrease all resistances';
    }
  }
}
