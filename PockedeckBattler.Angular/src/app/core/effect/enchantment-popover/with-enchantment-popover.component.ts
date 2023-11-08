import { Component, Input } from '@angular/core';
import { EnchantmentView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-with-enchantment-popover',
  templateUrl: './with-enchantment-popover.component.html',
})
export class WithEnchantmentPopoverComponent {
  @Input()
  public enchantment: EnchantmentView | undefined;

  @Input()
  public disabled: boolean = false;
}
