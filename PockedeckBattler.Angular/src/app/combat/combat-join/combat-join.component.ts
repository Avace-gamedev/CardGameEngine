import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, from, of, switchMap } from 'rxjs';
import {
  CombatsService,
  UpdateCombatInPreparationRequest,
} from '../../api/pockedeck-battler-api-client';
import { IdentityService } from '../../core/authentication/services/identity.service';

@Component({
  templateUrl: './combat-join.component.html',
  styleUrls: ['./combat-join.component.css'],
})
export class CombatJoinComponent implements OnInit {
  protected error: string | undefined;

  constructor(
    private identityService: IdentityService,
    private combatsService: CombatsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    let id: string;
    this.activatedRoute.paramMap
      .pipe(
        switchMap((paramMap) => {
          const idOrNull = paramMap.get('id');
          if (!idOrNull) {
            return from(this.router.to404().then());
          }

          id = idOrNull;

          const identity = this.identityService.getIdentity();
          return this.combatsService.updateCombatInPreparation(
            id,
            new UpdateCombatInPreparationRequest({
              playerName: identity,
              ready: false,
            }),
          );
        }),
        catchError((err) => {
          console.error(err);
          this.error = 'Could not join combat, does it exist?';
          return of(void 0);
        }),
        switchMap(() => this.router.toCombatConfiguration(id)),
      )
      .subscribe();
  }
}
