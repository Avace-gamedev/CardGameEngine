import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { TurnsIconComponent } from './turns-icon/turns-icon.component';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { AssetIconComponent } from './asset-icon/asset-icon.component';
import { ActionPointsIconComponent } from './action-points-icon/action-points-icon.component';
import { CharacterStatsEffectIconComponent } from './stat-effect-icon/character-stats-effect-icon.component';
import { CardStatsEffectIconComponent } from './stat-effect-icon/card-stats-effect-icon.component';
import { EnchantmentIconComponent } from './enchantment-icon/enchantment-icon.component';

@NgModule({
  declarations: [
    ElementIconComponent,
    ActionPointsIconComponent,
    TurnsIconComponent,
    AssetIconComponent,
    CharacterStatsEffectIconComponent,
    CardStatsEffectIconComponent,
    EnchantmentIconComponent,
  ],
  imports: [CommonModule, NgbTooltip, NgOptimizedImage],
  exports: [
    ElementIconComponent,
    ActionPointsIconComponent,
    TurnsIconComponent,
    AssetIconComponent,
    CharacterStatsEffectIconComponent,
    CardStatsEffectIconComponent,
  ],
})
export class IconsModule {}
