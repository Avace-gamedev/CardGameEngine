import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'selection',
  },
  {
    path: 'selection',
    component: CombatSelectionComponent,
  },
  {
    path: 'creation/:id',
    component: CombatCreationComponent,
  },
  {
    path: 'join/:id',
    component: CombatJoinComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CombatRoutingModule {}
