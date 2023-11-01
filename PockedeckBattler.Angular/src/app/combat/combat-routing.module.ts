import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatCreationComponent } from './combat-creation/combat-creation.component';
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
    path: 'creation',
    component: CombatCreationComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CombatRoutingModule {}
