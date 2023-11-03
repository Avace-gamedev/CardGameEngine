import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { ActionPointsComponent } from './action-points/action-points.component';
import { TurnsIconComponent } from './turns-icon/turns-icon.component';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { AssetIconComponent } from './asset-icon/asset-icon.component';

@NgModule({
  declarations: [
    ElementIconComponent,
    ActionPointsComponent,
    TurnsIconComponent,
    AssetIconComponent,
  ],
  imports: [CommonModule, NgbTooltip, NgOptimizedImage],
  exports: [
    ElementIconComponent,
    ActionPointsComponent,
    TurnsIconComponent,
    AssetIconComponent,
  ],
})
export class IconsModule {}
