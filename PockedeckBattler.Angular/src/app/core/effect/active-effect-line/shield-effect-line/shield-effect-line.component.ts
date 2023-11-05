import { Component, Input } from '@angular/core';
import { ShieldEffectView } from '../../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../../../icons/asset-icon/asset-icons';
import { ActiveEffectIconsUtils } from '../../../icons/utils/active-effect-icons-utils';

@Component({
  selector: 'app-shield-effect-line',
  templateUrl: './shield-effect-line.component.html',
})
export class ShieldEffectLineComponent {
  @Input()
  public effect: ShieldEffectView | undefined;

  protected icon: AssetIcon = ActiveEffectIconsUtils.shield;
}
