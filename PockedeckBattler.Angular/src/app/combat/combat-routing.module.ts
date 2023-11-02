import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatConfigureComponent } from './combat-configure/combat-configure.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';
import { CombatComponent } from './combat.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'selection',
  },
  {
    path: 'configure/:id',
    component: CombatConfigureComponent,
  },
  {
    path: 'join/:id',
    component: CombatJoinComponent,
  },
  {
    path: 'selection',
    component: CombatSelectionComponent,
  },
  {
    path: ':id',
    component: CombatComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CombatRoutingModule {}
