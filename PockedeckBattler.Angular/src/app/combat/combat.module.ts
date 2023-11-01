import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { CombatCreationSideComponent } from './combat-creation/combat-creation-side/combat-creation-side.component';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatCreationComponent,
    CombatCreationSideComponent,
  ],
  imports: [
    CommonModule,
    CombatRoutingModule,
    FormsModule,
    NgbTooltip,
    CoreModule,
  ],
})
export class CombatModule {}
