import { Component, Input } from '@angular/core';
import { DamageReceived } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-damage-received',
  templateUrl: './damage-received.component.html',
})
export class DamageReceivedComponent {
  @Input()
  public damageReceived: DamageReceived | undefined;
}
