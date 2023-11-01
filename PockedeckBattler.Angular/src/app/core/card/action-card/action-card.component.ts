import { Component, Input } from '@angular/core';
import { ActionCardView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-action-card',
  templateUrl: './action-card.component.html',
  styleUrls: ['./action-card.component.css'],
})
export class ActionCardComponent {
  @Input()
  public card: ActionCardView | undefined;

  @Input()
  public contentOnly: boolean = false;
}
