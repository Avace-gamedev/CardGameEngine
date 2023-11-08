import { Component, Input } from '@angular/core';
import { EnchantmentView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-enchantment-received',
  templateUrl: './enchantment-received.component.html',
})
export class EnchantmentReceivedComponent {
  @Input()
  public enchantment: EnchantmentView | undefined;
}
