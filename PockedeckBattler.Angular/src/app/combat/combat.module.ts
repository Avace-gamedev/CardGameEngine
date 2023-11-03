import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { ActionCardInstanceComponent } from './action-card-instance/action-card-instance.component';
import { CombatCharacterImgComponent } from './combat-character-img/combat-character-img.component';
import { CombatConfigureSideComponent } from './combat-configure/combat-configure-side/combat-configure-side.component';
import { CombatConfigureComponent } from './combat-configure/combat-configure.component';
import { CombatEmptySideComponent } from './combat-configure/combat-empty-side/combat-empty-side.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatSideCommonElementsComponent } from './combat-side/combat-side-common-elements.component';
import { CombatComponent } from './combat.component';
import { GenericUiModule } from './generic-ui/generic-ui.module';
import { OpponentSideElementsComponent } from './opponent-side-elements/opponent-side-elements.component';
import { PlayerSideElementsComponent } from './player-side-elements/player-side-elements.component';
import { CharacterCombatEffectsComponent } from './combat-character-img/character-combat-effects/character-combat-effects.component';
import { CombatEffectComponent } from './combat-character-img/character-combat-effects/combat-effect/combat-effect.component';
import { StatsModifierEffectComponent } from './combat-character-img/character-combat-effects/stats-modifier-effect/stats-modifier-effect.component';
import { PassiveEffectInstanceComponent } from './combat-character-img/character-combat-effects/passive-effect-instance/passive-effect-instance.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatConfigureComponent,
    CombatConfigureSideComponent,
    CombatEmptySideComponent,
    CombatJoinComponent,
    CombatComponent,
    CombatSideCommonElementsComponent,
    CombatCharacterImgComponent,
    PlayerSideElementsComponent,
    OpponentSideElementsComponent,
    ActionCardInstanceComponent,
    CharacterCombatEffectsComponent,
    CombatEffectComponent,
    StatsModifierEffectComponent,
    PassiveEffectInstanceComponent,
  ],
  imports: [
    CommonModule,
    CoreModule,
    CombatRoutingModule,
    FormsModule,
    NgbTooltip,
    CoreModule,
    NgbPopover,
    NgbAlert,
    GenericUiModule,
  ],
})
export class CombatModule {}
