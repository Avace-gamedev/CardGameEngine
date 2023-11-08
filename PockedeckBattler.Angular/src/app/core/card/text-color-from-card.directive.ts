import { Directive, HostBinding, Input } from '@angular/core';
import { ActionCardView } from '../../api/pockedeck-battler-api-client';
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
        this.elementClass = 'damage-card-color';
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
