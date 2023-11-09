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
import { PlayerSideElementsComponent } from './player-side-elements/player-side-elements.component';
import { CharacterCombatEffectsComponent } from './combat-character-img/character-combat-effects/character-combat-effects.component';
import { CombatEffectComponent } from './combat-character-img/character-combat-effects/combat-effect/combat-effect.component';
import { PassiveEffectInstanceLineComponent } from './combat-character-img/character-combat-effects/passive-effect-instance-line/passive-effect-instance-line.component';
import { CombatCommonElementsComponent } from './combat-common-elements/combat-common-elements.component';
import { EnchantmentEffectInstanceComponent } from './combat-character-img/character-combat-effects/enchantment-effect-instance/enchantment-effect-instance.component';
import { TriggeredEffectInstanceLineComponent } from './combat-character-img/character-combat-effects/triggered-effect-instance-line/triggered-effect-instance-line.component';
import { CombatLogComponent } from './combat-log/combat-log.component';
import { CardPlayedLogEntryComponent } from './combat-log/card-played-log-entry/card-played-log-entry.component';
import { DamageReceivedComponent } from './combat-log/damage-received/damage-received.component';
import { ShieldReceivedComponent } from './combat-log/shield-received/shield-received.component';
import { HealReceivedComponent } from './combat-log/heal-received/heal-received.component';
import { EnchantmentReceivedComponent } from './combat-log/enchantment-received/enchantment-received.component';
import { StickyPopoverDirective } from '../shared/directives/sticky-popover.directive';
import { CharacterDiedComponent } from './combat-log/character-died/character-died.component';

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
    ActionCardInstanceComponent,
    CharacterCombatEffectsComponent,
    CombatEffectComponent,
    PassiveEffectInstanceLineComponent,
    TriggeredEffectInstanceLineComponent,
    CombatCommonElementsComponent,
    EnchantmentEffectInstanceComponent,
    CombatLogComponent,
    CardPlayedLogEntryComponent,
    DamageReceivedComponent,
    ShieldReceivedComponent,
    HealReceivedComponent,
    EnchantmentReceivedComponent,
    CharacterDiedComponent,
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
    StickyPopoverDirective,
  ],
})
export class CombatModule {}
