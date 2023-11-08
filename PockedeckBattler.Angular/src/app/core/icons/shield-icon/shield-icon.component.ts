import { Component, Input } from '@angular/core';
import { AssetIconSize } from '../asset-icon/asset-icon.component';

@Component({
  selector: 'app-shield-icon',
  templateUrl: './shield-icon.component.html',
})
export class ShieldIconComponent {
  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;
}
