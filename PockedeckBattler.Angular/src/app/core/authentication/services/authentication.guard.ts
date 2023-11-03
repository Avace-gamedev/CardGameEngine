import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { IdentityService } from './identity.service';

export const authenticationGuard: (redirect: string) => CanActivateFn = (redirect: string) => (route, state) => {
  const isAuthenticated = inject(IdentityService).isAuthenticated();

  if (isAuthenticated) {
    return true;
  }

  inject(Router)
    .navigate([redirect], {
      queryParams: { redirect: state.url },
    })
    .then();

  return false;
};
