import { Component, Input } from '@angular/core';
import { AssetIconSize } from '../asset-icon/asset-icon.component';

@Component({
  selector: 'app-health-icon',
  templateUrl: './health-icon.component.html',
})
export class HealthIconComponent {
  @Input()
  public size: AssetIconSize = 'md';

  @Input()
  public noColor: boolean = false;

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;
}
