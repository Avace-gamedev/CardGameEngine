import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';
import { CombatCreationSideComponent } from './combat-creation/combat-creation-side/combat-creation-side.component';

@NgModule({
  declarations: [
    CombatSelectionComponent,
    CombatCreationComponent,
    CombatCreationSideComponent,
  ],
  imports: [CommonModule, CombatRoutingModule, FormsModule],
})
export class CombatModule {}
