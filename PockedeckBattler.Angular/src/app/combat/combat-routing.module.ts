import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CombatSelectionComponent } from './combat-selection/combat-selection.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'selection',
  },
  {
    path: 'preparation',
    loadChildren: () =>
      import('./combat-preparation/combat-preparation.module').then(
        (m) => m.CombatPreparationModule,
      ),
  },
  {
    path: 'selection',
    component: CombatSelectionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CombatRoutingModule {}
