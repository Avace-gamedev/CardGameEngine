import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../core/core.module';
import { CombatPreparationModule } from './combat-preparation/combat-preparation.module';

import { CombatRoutingModule } from './combat-routing.module';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatComponent } from './combat.component';

@NgModule({
  declarations: [CombatSelectionComponent, CombatComponent],
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
  ],
})
export class CombatModule {}
