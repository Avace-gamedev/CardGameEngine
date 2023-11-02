import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatConfigureComponent } from './combat-creation/combat-configure.component';
import { CombatJoinComponent } from './combat-join/combat-join.component';
import { CombatPreparationComponent } from './combat-preparation.component';

const routes: Routes = [
  {
    path: '',
    component: CombatPreparationComponent,
    children: [
      {
        path: 'configure/:id',
        component: CombatConfigureComponent,
      },
      {
        path: 'join/:id',
        component: CombatJoinComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CombatPreparationRoutingModule {}
