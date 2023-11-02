import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { CombatCharacterImgComponent } from './combat-character-img/combat-character-img.component';
import { CombatPreparationModule } from './combat-preparation/combat-preparation.module';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatSideComponent } from './combat-side/combat-side.component';
import { CombatComponent } from './combat.component';
import { GenericUiModule } from './generic-ui/generic-ui.module';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatComponent,
    CombatSideComponent,
    CombatCharacterImgComponent,
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
  ],
})
export class CombatModule {}
