import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { CombatCreationSideComponent } from './combat-creation/combat-creation-side/combat-creation-side.component';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';
import { CombatEmptySideComponent } from './combat-creation/combat-empty-side/combat-empty-side.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatCreationComponent,
    CombatCreationSideComponent,
    CombatEmptySideComponent,
    CombatJoinComponent,
  ],
  imports: [
    CommonModule,
    CombatRoutingModule,
    FormsModule,
    NgbTooltip,
    CoreModule,
    NgbPopover,
    NgbAlert,
  ],
})
export class CombatModule {}
