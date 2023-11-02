import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbAlert, NgbPopover, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../../core/core.module';
import { CombatConfigureComponent } from './combat-creation/combat-configure.component';
import { CombatConfigureSideComponent } from './combat-creation/combat-creation-side/combat-configure-side.component';
import { CombatEmptySideComponent } from './combat-creation/combat-empty-side/combat-empty-side.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';

import { CombatPreparationRoutingModule } from './combat-preparation-routing.module';
import { CombatPreparationComponent } from './combat-preparation.component';

@NgModule({
  declarations: [
    CombatPreparationComponent,
    CombatConfigureComponent,
    CombatConfigureSideComponent,
    CombatEmptySideComponent,
    CombatJoinComponent,
  ],
  imports: [
    CommonModule,
    CombatPreparationRoutingModule,
    CoreModule,
    NgbTooltip,
    NgbPopover,
    NgbAlert,
  ],
})
export class CombatPreparationModule {}
