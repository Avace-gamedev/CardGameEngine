import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, filter, from, map, of, switchMap } from 'rxjs';
import {
  CombatsService,
  PlayerCombatView,
} from '../api/pockedeck-battler-api-client';
import { SignalRService } from '../api/signal-r/signal-r.service';
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
    private signalrService: SignalRService,
    private combatsService: CombatsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    const identity = this.identityService.getIdentity();

    this.signalrService
      .listen<PlayerCombatView>(
        'combats',
        'CombatChanged',
        PlayerCombatView.fromJS,
      )
      .subscribe((combat) => (this.combat = combat));

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

  play(index: number) {
    if (!this.combat) {
      return;
    }

    this.combatsService
      .playCard(this.combat.id, index, this.identityService.getIdentity())
      .subscribe();
  }

  endTurn() {
    if (!this.combat) {
      return;
    }

    this.combatsService
      .endTurn(this.combat.id, this.identityService.getIdentity())
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
