import { Component, Input } from '@angular/core';
import {
  ActionCardView,
  AddEnchantmentEffectView,
  DamageEffectView,
  Element,
} from '../../../api/pockedeck-battler-api-client';
import { ActionCardTypeUtils } from '../../../shared/utils/action-card-type-utils';
import { PlacementArray } from '@ng-bootstrap/ng-bootstrap/util/positioning';

@Component({
  selector: 'app-action-card-chip',
  templateUrl: './action-card-chip.component.html',
})
export class ActionCardChipComponent {
  @Input()
  get card(): ActionCardView | undefined {
    return this._card;
  }
  set card(value: ActionCardView | undefined) {
    this._card = value;
    this.update();
  }
  private _card: ActionCardView | undefined;

  @Input()
  public enablePopoverDetails: boolean = false;

  @Input()
  public popoverDetailsPlacement: PlacementArray = 'auto';

  @Input()
  public size: ActionCardChipSize = 'md';

  protected bgColorCssVariable: string | undefined;
  protected textColorCssVariable: string | undefined;
  protected damage: { value: number; element: Element } | undefined;
  protected isTriggered: boolean = false;

  private update() {
    this.bgColorCssVariable = this.card ? ActionCardTypeUtils.computeBgColor(this.card) : undefined;
    this.textColorCssVariable = this.card ? ActionCardTypeUtils.computeTextColor(this.card) : undefined;
    this.damage = this.computeDamage();
    this.isTriggered =
      this.card?.mainEffect instanceof AddEnchantmentEffectView &&
      this.card.mainEffect.enchantmentEffect.triggered.length > 0;
  }

  private computeDamage(): { value: number; element: Element } | undefined {
    if (!(this.card?.mainEffect instanceof DamageEffectView)) {
      return undefined;
    }

    return {
      value: this.card.mainEffect.amount,
      element: this.card.mainEffect.element,
    };
  }
}

export type ActionCardChipSize = 'sm' | 'md';
