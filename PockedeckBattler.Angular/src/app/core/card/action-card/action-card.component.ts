import { Component, Input } from '@angular/core';
import { ActionCardView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-action-card',
  templateUrl: './action-card.component.html',
})
export class ActionCardComponent {
  @Input()
  public card: ActionCardView | undefined;

  @Input()
  public contentOnly: boolean = false;

  @Input()
  public size: ActionCardSize = 'md';
}

export type ActionCardSize = 'sm' | 'md';
