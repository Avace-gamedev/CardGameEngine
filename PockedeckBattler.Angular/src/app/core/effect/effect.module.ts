import { NgModule } from '@angular/core';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { ActiveEffectLineComponent } from './active-effect-line/active-effect-line.component';
import { AddPassiveEffectLineComponent } from './active-effect-line/add-passive-effect-line/add-passive-effect-line.component';
import { AddTriggeredEffectLineComponent } from './active-effect-line/add-triggered-effect-line/add-triggered-effect-line.component';
import { DamageEffectLineComponent } from './active-effect-line/damage-effect-line/damage-effect-line.component';
import { HealEffectLineComponent } from './active-effect-line/heal-effect-line/heal-effect-line.component';
import { RandomEffectLineComponent } from './active-effect-line/random-effect-line/random-effect-line.component';
import { ShieldEffectLineComponent } from './active-effect-line/shield-effect-line/shield-effect-line.component';
import { PassiveEffectLineComponent } from './passive-effect-line/passive-effect-line.component';
import { StatsPassiveEffectLineComponent } from './passive-effect-line/stats-passive-effect-line/stats-passive-effect-line.component';
import { TriggeredEffectLineComponent } from './triggered-effect-line/triggered-effect-line.component';
import { IconsModule } from '../icons/icons.module';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [
    ActiveEffectLineComponent,
    DamageEffectLineComponent,
    HealEffectLineComponent,
    ShieldEffectLineComponent,
    RandomEffectLineComponent,
    AddPassiveEffectLineComponent,
    PassiveEffectLineComponent,
    StatsPassiveEffectLineComponent,
    TriggeredEffectLineComponent,
    AddTriggeredEffectLineComponent,
  ],
  imports: [SharedModule, IconsModule, NgbTooltip],
  exports: [ActiveEffectLineComponent],
})
export class EffectModule {}
