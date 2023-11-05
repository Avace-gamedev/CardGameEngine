import { Component, Input } from '@angular/core';
import { HealEffectView } from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../icons/asset-icon/asset-icons';
import { ActiveEffectIconsUtils } from '../../../icons/utils/active-effect-icons-utils';

@Component({
  selector: 'app-heal-effect-line',
  templateUrl: './heal-effect-line.component.html',
})
export class HealEffectLineComponent {
  @Input()
  public effect: HealEffectView | undefined;

  protected icon: AssetIcon = ActiveEffectIconsUtils.heal;
}
