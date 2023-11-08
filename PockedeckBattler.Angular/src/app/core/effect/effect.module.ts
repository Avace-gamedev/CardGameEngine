import { NgModule } from '@angular/core';
import { NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { ActiveEffectLineComponent } from './active-effect-line/active-effect-line.component';
import { DamageEffectLineComponent } from './active-effect-line/damage-effect-line/damage-effect-line.component';
import { HealEffectLineComponent } from './active-effect-line/heal-effect-line/heal-effect-line.component';
import { RandomEffectLineComponent } from './active-effect-line/random-effect-line/random-effect-line.component';
import { ShieldEffectLineComponent } from './active-effect-line/shield-effect-line/shield-effect-line.component';
import { PassiveEffectLineComponent } from './passive-effect-line/passive-effect-line.component';
import { TriggeredEffectLineComponent } from './triggered-effect-line/triggered-effect-line.component';
import { IconsModule } from '../icons/icons.module';
import { SharedModule } from '../../shared/shared.module';
import { CardStatEffectLineComponent } from './passive-effect-line/card-stat-effect-line/card-stat-effect-line.component';
import { CharacterStatEffectLineComponent } from './passive-effect-line/character-stat-effect-line/character-stat-effect-line.component';
import { AddEnchantmentEffectLineComponent } from './active-effect-line/add-enchantment-effect-line/add-enchantment-effect-line.component';
import { StickyPopoverDirective } from '../../shared/directives/sticky-popover.directive';
import { WithEnchantmentPopoverComponent } from './with-enchantment-popover/with-enchantment-popover.component';
import { TextColorFromElementDirective } from './text-color-from-element.directive';

@NgModule({
  declarations: [
    ActiveEffectLineComponent,
    DamageEffectLineComponent,
    HealEffectLineComponent,
    ShieldEffectLineComponent,
    RandomEffectLineComponent,
    AddEnchantmentEffectLineComponent,
    PassiveEffectLineComponent,
    CharacterStatEffectLineComponent,
    CardStatEffectLineComponent,
    TriggeredEffectLineComponent,
    WithEnchantmentPopoverComponent,
    TextColorFromElementDirective,
  ],
  imports: [SharedModule, IconsModule, NgbTooltip, NgbPopover, StickyPopoverDirective],
  exports: [
    ActiveEffectLineComponent,
    PassiveEffectLineComponent,
    TriggeredEffectLineComponent,
    AddEnchantmentEffectLineComponent,
    ShieldEffectLineComponent,
    WithEnchantmentPopoverComponent,
  ],
})
export class EffectModule {}
