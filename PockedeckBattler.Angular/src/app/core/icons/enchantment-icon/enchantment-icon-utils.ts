import {
  CardStatsEffectView,
  CharacterStatsEffectView,
  EnchantmentView,
  PassiveEffectView,
  TriggeredEffectView,
} from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { StatEffectUtils } from '../stat-effect-icon/stat-effect';
import { ActiveEffectIconsUtils } from '../utils/active-effect-icons-utils';

export class EnchantmentIconUtils {
  static getIcon(enchantment: EnchantmentView): AssetIcon | undefined {
    const knownEnchantmentIcon = this.getIconFromKnownEnchantment(enchantment.name);
    if (knownEnchantmentIcon) {
      return knownEnchantmentIcon;
    }

    const passiveIcon = this.getIconFromPassives(enchantment.passive);
    if (passiveIcon) {
      return passiveIcon;
    }

    const triggeredIcon = this.getIconFromTriggered(enchantment.triggered);
    if (triggeredIcon) {
      return triggeredIcon;
    }

    return undefined;
  }

  private static getIconFromKnownEnchantment(name: string): AssetIcon | undefined {
    switch (name.toLowerCase()) {
      case 'burn':
        return 'fire';
    }

    return undefined;
  }

  private static getIconFromPassives(passives: PassiveEffectView[]): AssetIcon | undefined {
    for (const passive of passives) {
      if (passive instanceof CharacterStatsEffectView) {
        return StatEffectUtils.getIcon(passive.type);
      } else if (passive instanceof CardStatsEffectView) {
        return StatEffectUtils.getIcon(passive.type);
      }
    }

    return undefined;
  }

  private static getIconFromTriggered(triggers: TriggeredEffectView[]): AssetIcon | undefined {
    for (const trigger of triggers) {
      const activeEffectIcon = ActiveEffectIconsUtils.getIcon(trigger.effect);
      if (activeEffectIcon) {
        return activeEffectIcon;
      }
    }

    return undefined;
  }
}
