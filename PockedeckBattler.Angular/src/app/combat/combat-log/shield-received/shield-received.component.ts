import { Component, Input } from '@angular/core';
import { ShieldReceived } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-shield-received',
  templateUrl: './shield-received.component.html',
})
export class ShieldReceivedComponent {
  @Input()
  public shieldReceived: ShieldReceived | undefined;
}
