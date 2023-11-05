import { Component, Input } from '@angular/core';
import { ActionCardView, ActiveEffectView, RandomEffectView } from '../../../api/pockedeck-battler-api-client';
import { ActiveEffectType } from '../../../shared/utils/types';
import { AssetIcon } from '../../icons/asset-icon/asset-icons';
import { ActiveEffectIconsUtils } from '../../icons/utils/active-effect-icons-utils';
import { ActiveEffectTypeUtils } from '../../../shared/utils/active-effect-type-utils';

@Component({
  selector: 'app-action-card-type-icon',
  templateUrl: './action-card-type-icon.component.html',
})
export class ActionCardTypeIconComponent {
  @Input()
  get card(): ActionCardView | undefined {
    return this._card;
  }
  set card(value: ActionCardView | undefined) {
    this._card = value;
    this.update();
  }
  private _card: ActionCardView | undefined;

  protected icon: AssetIcon | undefined;
  protected tooltip: string | undefined;

  private update() {
    if (!this._card) {
      this.icon = undefined;
      this.tooltip = undefined;
      return;
    }

    this.icon = ActiveEffectIconsUtils.getIcon(this._card.mainEffect);
    this.tooltip = this.getTooltip(this._card.mainEffect);
  }

  private getTooltip(effect: ActiveEffectView): string | undefined {
    const type = ActiveEffectTypeUtils.getType(effect);
    switch (type) {
      case ActiveEffectType.None:
        return undefined;
      case ActiveEffectType.Damage:
        return 'Damage';
      case ActiveEffectType.Heal:
        return 'Heal';
      case ActiveEffectType.Shield:
        return 'Shield';
      case ActiveEffectType.Random:
        for (const entry of (effect as RandomEffectView).entries) {
          const tooltip = this.getTooltip(entry.effect);
          if (tooltip) {
            return tooltip;
          }
        }
        break;
      case ActiveEffectType.AddEnchantment:
        return 'Enchantment';
    }

    return undefined;
  }
}
