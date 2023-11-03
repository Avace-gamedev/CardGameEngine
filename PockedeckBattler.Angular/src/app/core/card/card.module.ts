import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { EffectModule } from '../effect/effect.module';
import { ActionCardChipComponent } from './action-card-chip/action-card-chip.component';
import { ActionCardTargetLineComponent } from './action-card-target-line/action-card-target-line.component';
import { ActionCardTypeIconComponent } from './action-card-type-icon/action-card-type-icon.component';
import { ActionCardComponent } from './action-card/action-card.component';
import { CardDamageComponent } from './card-damage/card-damage.component';
import { IconsModule } from '../icons/icons.module';

@NgModule({
  declarations: [
    ActionCardTypeIconComponent,
    ActionCardChipComponent,
    CardDamageComponent,
    ActionCardComponent,
    ActionCardTargetLineComponent,
  ],
  imports: [CommonModule, IconsModule, EffectModule, NgbPopover, NgbTooltip],
  exports: [ActionCardChipComponent, ActionCardComponent],
})
export class CardModule {}
