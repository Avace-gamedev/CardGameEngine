import { Component, Input } from '@angular/core';
import { AssetIconSize } from '../asset-icon/asset-icon.component';
import { Color } from '../../../shared/utils/colors';

@Component({
  selector: 'app-death-icon',
  templateUrl: './death-icon.component.html',
})
export class DeathIconComponent {
  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public color: Color | undefined;

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;
}
