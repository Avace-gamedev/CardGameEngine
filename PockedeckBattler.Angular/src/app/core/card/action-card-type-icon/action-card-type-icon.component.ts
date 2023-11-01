import { Component, Input } from '@angular/core';
import { ActionCardView } from '../../../api/pockedeck-battler-api-client';
import { ActionCardTypeUtils } from '../../core-common/utils/action-card-type-utils';
import { CardType } from '../../core-common/utils/types';

@Component({
  selector: 'app-action-card-type-icon',
  templateUrl: './action-card-type-icon.component.html',
  styleUrls: ['./action-card-type-icon.component.css'],
})
export class ActionCardTypeIconComponent {
  @Input()
  public card: ActionCardView | undefined;

  protected get type(): CardType {
    return this.card ? ActionCardTypeUtils.getType(this.card) : CardType.None;
  }

  protected readonly CardType = CardType;
}
