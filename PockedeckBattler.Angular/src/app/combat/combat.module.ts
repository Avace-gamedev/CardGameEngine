import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { CombatCreationSideComponent } from './combat-creation/combat-creation-side/combat-creation-side.component';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatEmptySideComponent } from './combat-creation/combat-empty-side/combat-empty-side.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatCreationComponent,
    CombatCreationSideComponent,
    CombatEmptySideComponent,
  ],
  imports: [
    CommonModule,
    CombatRoutingModule,
    FormsModule,
    NgbTooltip,
    CoreModule,
    NgbPopover,
  ],
})
export class CombatModule {}
