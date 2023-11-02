import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreCommonModule } from '../core/core-common/core-common.module';
import { CoreModule } from '../core/core.module';
import { CombatCharacterImgComponent } from './combat-character-img/combat-character-img.component';
import { CombatPreparationModule } from './combat-preparation/combat-preparation.module';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatSideCommonElementsComponent } from './combat-side/combat-side-common-elements.component';
import { CombatComponent } from './combat.component';
import { GenericUiModule } from './generic-ui/generic-ui.module';
import { OpponentSideElementsComponent } from './opponent-side-elements/opponent-side-elements.component';
import { PlayerSideElementsComponent } from './player-side-elements/player-side-elements.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatComponent,
    CombatSideCommonElementsComponent,
    CombatCharacterImgComponent,
    PlayerSideElementsComponent,
    OpponentSideElementsComponent,
  ],
  imports: [
    CommonModule,
    CoreModule,
    CombatRoutingModule,
    CombatPreparationModule,
    FormsModule,
    NgbTooltip,
    CoreModule,
    NgbPopover,
    NgbAlert,
    GenericUiModule,
    CoreCommonModule,
  ],
})
export class CombatModule {}
