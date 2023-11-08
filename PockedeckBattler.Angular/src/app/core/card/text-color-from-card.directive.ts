import { Directive, HostBinding, Input } from '@angular/core';
import { ActionCardView, DamageEffectView, Element } from '../../api/pockedeck-battler-api-client';
import { ActionCardTypeUtils } from '../../shared/utils/action-card-type-utils';
import { CardType } from '../../shared/utils/types';

@Directive({
  selector: '[textColorFromCard]',
})
export class TextColorFromCardDirective {
  @Input('textColorFromCard')
  get card(): ActionCardView | undefined {
    return this._card;
  }
  set card(value: ActionCardView | undefined) {
    this._card = value;
    this.update();
  }
  private _card: ActionCardView | undefined;

  @HostBinding('class')
  private elementClass: string | undefined;

  private update() {
    if (!this._card) {
      this.elementClass = undefined;
      return;
    }

    const type = ActionCardTypeUtils.getType(this._card);

    switch (type) {
      case CardType.None:
        this.elementClass = undefined;
        break;
      case CardType.Damage:
        switch ((this._card.mainEffect as DamageEffectView).element) {
          case Element.Neutral:
            this.elementClass = 'neutral-color';
            break;
          case Element.Fire:
            this.elementClass = 'fire-color';
            break;
          case Element.Earth:
            this.elementClass = 'earth-color';
            break;
          case Element.Water:
            this.elementClass = 'water-color';
            break;
          case Element.Wind:
            this.elementClass = 'wind-color';
            break;
        }
        break;
      case CardType.Heal:
        this.elementClass = 'heal-card-color';
        break;
      case CardType.Shield:
        this.elementClass = 'shield-card-color';
        break;
      case CardType.Enchantment:
        this.elementClass = 'enchantment-card-color';
        break;
    }
  }
}
