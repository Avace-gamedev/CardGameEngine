import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { TurnsIconComponent } from './turns-icon/turns-icon.component';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { AssetIconComponent } from './asset-icon/asset-icon.component';
import { ActionPointsIconComponent } from './action-points-icon/action-points-icon.component';
import { StatEffectIconComponent } from './stat-effect-icon/stat-effect-icon.component';

@NgModule({
  declarations: [
    ElementIconComponent,
    ActionPointsIconComponent,
    TurnsIconComponent,
    AssetIconComponent,
    StatEffectIconComponent,
  ],
  imports: [CommonModule, NgbTooltip, NgOptimizedImage],
  exports: [
    ElementIconComponent,
    ActionPointsIconComponent,
    TurnsIconComponent,
    AssetIconComponent,
    StatEffectIconComponent,
  ],
})
export class IconsModule {}
