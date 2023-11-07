import {
  AddEnchantmentEffectView,
  DamageEffectView,
  EffectView,
  HealEffectView,
  RandomEffectView,
  ShieldEffectView,
} from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { ElementIconsUtils } from '../element-icon/element-icons-utils';

export class ActiveEffectIconsUtils {
  static heal: AssetIcon = 'heal';
  static shield: AssetIcon = 'healing-shield';
  static enchantment: AssetIcon = 'galaxy';

  static getIcon(effect: EffectView): AssetIcon | undefined {
    if (effect instanceof DamageEffectView) {
      return ElementIconsUtils.getIcon(effect.element);
    } else if (effect instanceof HealEffectView) {
      return this.heal;
    } else if (effect instanceof ShieldEffectView) {
      return this.shield;
    } else if (effect instanceof RandomEffectView) {
      for (const innerEffect of effect.entries) {
        const icon = this.getIcon(innerEffect.effect);
        if (icon) {
          return icon;
        }
      }
    } else if (effect instanceof AddEnchantmentEffectView) {
      return this.enchantment;
    }

    return undefined;
  }
}
