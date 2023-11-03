import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { from, switchMap } from 'rxjs';
import { IdentityService } from '../../core/authentication/services/identity.service';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent {
  protected name: string | undefined;

  constructor(
    private identityService: IdentityService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  login() {
    if (!this.name) {
      return;
    }

    this.identityService.setIdentity(this.name);

    this.activatedRoute.queryParamMap
      .pipe(
        switchMap((paramMap) => {
          const redirect = paramMap.get('redirect') ?? '/';
          return from(this.router.navigateByUrl(redirect));
        }),
      )
      .subscribe();
  }
}
