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
import { SharedModule } from '../../shared/shared.module';
import { TextColorFromCardDirective } from './text-color-from-card.directive';
import { StickyPopoverDirective } from '../../shared/directives/sticky-popover.directive';
import { WithCardPopoverComponent } from './with-card-popover/with-card-popover.component';

@NgModule({
  declarations: [
    ActionCardTypeIconComponent,
    ActionCardChipComponent,
    CardDamageComponent,
    ActionCardComponent,
    ActionCardTargetLineComponent,
    TextColorFromCardDirective,
    WithCardPopoverComponent,
  ],
  imports: [CommonModule, IconsModule, EffectModule, NgbPopover, NgbTooltip, SharedModule, StickyPopoverDirective],
  exports: [ActionCardChipComponent, ActionCardComponent, TextColorFromCardDirective, WithCardPopoverComponent],
})
export class CardModule {}
