import { Component, Input } from '@angular/core';
import { AssetIconSize } from '../asset-icon/asset-icon.component';

@Component({
  selector: 'app-effect-icon',
  templateUrl: './effect-icon.component.html',
})
export class EffectIconComponent {
  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;
}
