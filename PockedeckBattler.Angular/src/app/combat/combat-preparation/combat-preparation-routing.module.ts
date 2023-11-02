import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatConfigureComponent } from './combat-configure/combat-configure.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';

const routes: Routes = [
  {
    path: 'configure/:id',
    component: CombatConfigureComponent,
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
export class CombatPreparationRoutingModule {}
