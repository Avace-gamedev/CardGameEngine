import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreCommonModule } from '../core-common/core-common.module';
import { ActiveEffectLine } from './active-effect-line/active-effect-line.component';
import { AddPassiveEffectLineComponent } from './active-effect-line/add-passive-effect-line/add-passive-effect-line.component';
import { AddTriggeredEffectLineComponent } from './active-effect-line/add-triggered-effect-line/add-triggered-effect-line.component';
import { DamageEffectLineComponent } from './active-effect-line/damage-effect-line/damage-effect-line.component';
import { HealEffectLineComponent } from './active-effect-line/heal-effect-line/heal-effect-line.component';
import { RandomEffectLineComponent } from './active-effect-line/random-effect-line/random-effect-line.component';
import { ShieldEffectLineComponent } from './active-effect-line/shield-effect-line/shield-effect-line.component';
import { PassiveEffectLineComponent } from './passive-effect-line/passive-effect-line.component';
import { StatsPassiveEffectLineComponent } from './passive-effect-line/stats-passive-effect-line/stats-passive-effect-line.component';
import { TriggeredEffectLineComponent } from './triggered-effect-line/triggered-effect-line.component';

@NgModule({
  declarations: [
    ActiveEffectLine,
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
  imports: [CommonModule, CoreCommonModule, NgbTooltip],
  exports: [ActiveEffectLine],
})
export class EffectModule {}