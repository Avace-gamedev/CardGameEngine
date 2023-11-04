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
import { TriggeredEffectLineComponent } from './triggered-effect-line/triggered-effect-line.component';
import { IconsModule } from '../icons/icons.module';
import { SharedModule } from '../../shared/shared.module';
import { CardStatEffectLineComponent } from './passive-effect-line/card-stat-effect-line/card-stat-effect-line.component';
import { CharacterStatEffectLineComponent } from './passive-effect-line/character-stat-effect-line/character-stat-effect-line.component';

@NgModule({
  declarations: [
    ActiveEffectLineComponent,
    DamageEffectLineComponent,
    HealEffectLineComponent,
    ShieldEffectLineComponent,
    RandomEffectLineComponent,
    AddPassiveEffectLineComponent,
    PassiveEffectLineComponent,
    CharacterStatEffectLineComponent,
    CardStatEffectLineComponent,
    TriggeredEffectLineComponent,
    AddTriggeredEffectLineComponent,
  ],
  imports: [SharedModule, IconsModule, NgbTooltip],
  exports: [ActiveEffectLineComponent, PassiveEffectLineComponent, TriggeredEffectLineComponent],
})
export class EffectModule {}
