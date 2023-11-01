import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { IdentityService } from './identity.service';

export const authenticationGuard: CanActivateFn = (route, state) => {
  var isAuthenticated = inject(IdentityService).isAuthenticated();

  if (isAuthenticated) {
    return true;
  }

  inject(Router)
    .navigate(['', 'login'], {
      queryParams: { redirect: state.url },
    })
    .then();

  return false;
};
