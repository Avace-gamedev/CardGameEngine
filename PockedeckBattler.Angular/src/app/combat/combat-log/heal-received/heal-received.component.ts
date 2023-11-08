import { Component, Input } from '@angular/core';
import { HealReceived } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-heal-received',
  templateUrl: './heal-received.component.html',
})
export class HealReceivedComponent {
  @Input()
  public healReceived: HealReceived | undefined;
}
