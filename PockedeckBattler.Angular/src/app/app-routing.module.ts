import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './common-pages/login/login.component';
import { NotFoundComponent } from './common-pages/not-found/not-found.component';
import { authenticationGuard } from './core/authentication/services/authentication.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [authenticationGuard],

    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home',
      },
      {
        path: 'home',
        loadChildren: () =>
          import('./home/home.module').then((m) => m.HomeModule),
      },
      {
        path: 'combat',
        loadChildren: () =>
          import('./combat/combat.module').then((m) => m.CombatModule),
      },
      {
        path: '404',
        component: NotFoundComponent,
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '**',
    redirectTo: '404',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
