import { Component, Input } from '@angular/core';
import { Element } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-card-damage',
  templateUrl: './card-damage.component.html',
})
export class CardDamageComponent {
  @Input()
  public value: number | undefined;

  @Input()
  public element: Element | undefined;
}
