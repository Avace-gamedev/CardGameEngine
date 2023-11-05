import { Component, Input } from '@angular/core';
import { AddEnchantmentEffectView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-add-enchantment-effect-line',
  templateUrl: './add-enchantment-effect-line.component.html',
})
export class AddEnchantmentEffectLineComponent {
  @Input()
  public effect: AddEnchantmentEffectView | undefined;
}
