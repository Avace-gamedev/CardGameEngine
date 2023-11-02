import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, filter, from, map, of, switchMap } from 'rxjs';
import {
  CombatsService,
  PlayerCombatView,
} from '../api/pockedeck-battler-api-client';
import { IdentityService } from '../core/authentication/services/identity.service';

@Component({
  selector: 'app-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css'],
})
export class CombatComponent implements OnInit {
  protected combat: PlayerCombatView | undefined;

  constructor(
    private identityService: IdentityService,
    private combatsService: CombatsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    const identity = this.identityService.getIdentity();

    this.activatedRoute.paramMap
      .pipe(
        switchMap((paramMap) => {
          const id = paramMap.get('id');
          if (id == null) {
            this.router.to404().then();
            return of(void 0);
          }

          return this.combatsService.getCombat(id, identity);
        }),
        catchError((err) => {
          console.error(err);
          return from(this.router.to404().then((r) => undefined));
        }),
        filter(Boolean),
        map((combat) => (this.combat = combat)),
      )
      .subscribe();
  }
}

declare module '@angular/router' {
  interface Router {
    toCombat(id: string): Promise<boolean>;
  }
}

Router.prototype.toCombat = function (id: string): Promise<boolean> {
  return this.navigate(['/', 'combat', id]);
};
