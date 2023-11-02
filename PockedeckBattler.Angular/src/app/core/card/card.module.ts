import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbPopover } from '@ng-bootstrap/ng-bootstrap';
import { CoreCommonModule } from '../core-common/core-common.module';
import { EffectModule } from '../effect/effect.module';
import { ActionCardChipComponent } from './action-card-chip/action-card-chip.component';
import { ActionCardTargetLineComponent } from './action-card-target-line/action-card-target-line.component';
import { ActionCardTypeIconComponent } from './action-card-type-icon/action-card-type-icon.component';
import { ActionCardComponent } from './action-card/action-card.component';
import { CardDamageComponent } from './card-damage/card-damage.component';

@NgModule({
  declarations: [
    ActionCardTypeIconComponent,
    ActionCardChipComponent,
    CardDamageComponent,
    ActionCardComponent,
    ActionCardTargetLineComponent,
  ],
  imports: [CommonModule, NgbPopover, CoreCommonModule, EffectModule],
  exports: [ActionCardChipComponent, ActionCardComponent],
})
export class CardModule {}
