import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { catchError, from, map, switchMap, throwError } from 'rxjs';
import {
  CharactersService,
  CharacterView,
  CombatInPreparationView,
  CombatSide,
  CombatsService,
  UpdateCombatInPreparationRequest,
} from '../../api/pockedeck-battler-api-client';
import { SignalRService } from '../../api/signal-r/signal-r.service';
import { IdentityService } from '../../core/authentication/services/identity.service';
import { ModalsService } from '../../core/modals/modals.service';
import { CombatSideConfiguration } from './combat-configure-side/combat-configure-side.component';
import { AlertsService } from '../../core/alert/alerts.service';

@UntilDestroy()
@Component({
  templateUrl: './combat-configure.component.html',
})
export class CombatConfigureComponent implements OnInit {
  protected characters: CharacterView[] = [];
  protected combat: CombatInPreparationView | undefined;
  protected side: CombatSide | undefined;

  protected startedCombatId: string | undefined;
  private hasRequestedStartOfCombat: boolean | undefined;

  constructor(
    private signalRService: SignalRService,
    private identityService: IdentityService,
    private combatsService: CombatsService,
    private charactersService: CharactersService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private modalsService: ModalsService,
    private alertsService: AlertsService
  ) {}

  ngOnInit() {
    const identity = this.identityService.getIdentity();

    this.signalRService
      .listen<CombatInPreparationView>('combats', 'CombatInPreparationChanged', CombatInPreparationView.fromJS)
      .pipe(untilDestroyed(this))
      .subscribe((combat) => (this.combat = combat));

    this.signalRService
      .listen<CombatInPreparationView>('combats', 'CombatInPreparationDeleted', CombatInPreparationView.fromJS)
      .pipe(untilDestroyed(this))
      .subscribe((combat) => this.onDeleted(combat));

    this.signalRService
      .listenRaw('combats', 'CombatInPreparationStarted')
      .pipe(untilDestroyed(this))
      .subscribe((...args: any[]) => this.onCreated(args[0]));

    this.activatedRoute.paramMap
      .pipe(
        switchMap((params) => {
          const id = params.get('id');
          if (id == null) {
            this.router.to404().then();
            return throwError(() => 'Id not found');
          }

          return this.combatsService.getCombatInPreparation(id, identity);
        }),
        catchError((err) => {
          console.error(err);
          return from(this.router.to404().then((r) => undefined));
        }),
        map((combat) => {
          this.combat = combat;

          if (identity === this.combat?.leftPlayerName) {
            this.side = CombatSide.Left;
          } else if (identity == this.combat?.rightPlayerName) {
            this.side = CombatSide.Right;
          } else {
            this.side = CombatSide.None;
          }
        }),
        switchMap(() => this.charactersService.getAll().pipe(map((characters) => (this.characters = characters))))
      )
      .subscribe();
  }

  protected start() {
    if (this.startedCombatId) {
      this.router.toCombat(this.startedCombatId).then();
    }

    if (!this.combat) {
      return;
    }

    this.combatsService
      .startCombat(this.combat.id, this.identityService.getIdentity())
      .subscribe((combatId) => this.router.toCombat(combatId).then());
  }

  protected sendUpdate(side: CombatSide, configuration: CombatSideConfiguration) {
    if (!this.combat) {
      return;
    }

    const identity = this.identityService.getIdentity();

    if (
      !(this.combat.rightPlayerIsAi && side === CombatSide.Right) &&
      ((identity == this.combat.leftPlayerName && side !== CombatSide.Left) ||
        (identity == this.combat.rightPlayerName && side !== CombatSide.Right) ||
        configuration.playerName !== identity)
    ) {
      return;
    }

    const isAi = side === CombatSide.Right && this.combat.rightPlayerIsAi;

    this.combatsService
      .updateCombatInPreparation(this.combat.id, new UpdateCombatInPreparationRequest({ ...configuration, isAi }))
      .subscribe();
  }

  protected setRightPlayerAi() {
    if (!this.combat) {
      return;
    }

    this.combatsService
      .updateCombatInPreparation(
        this.combat.id,
        new UpdateCombatInPreparationRequest({
          isAi: true,
          playerName: 'Sancho',
          ready: false,
        })
      )
      .subscribe();
  }

  protected leave(side: CombatSide) {
    if (!this.combat) {
      return;
    }

    let name: string | undefined;
    switch (side) {
      case CombatSide.Left:
        name = this.combat?.leftPlayerName;
        break;
      case CombatSide.Right:
        name = this.combat?.rightPlayerName;
        break;
    }

    if (!name) {
      return;
    }

    this.combatsService.leaveCombatInPreparation(this.combat.id, name).subscribe(() => this.router.toCombatSelection());
  }

  private onDeleted(combat: CombatInPreparationView) {
    if (this.startedCombatId) {
      return;
    }

    if (combat.leftPlayerName === this.identityService.getIdentity()) {
      return;
    }

    this.modalsService
      .alert({
        content: 'This combat has been deleted by ' + combat.leftPlayerName,
        closeLabel: 'Go back',
      })
      .subscribe(() => this.router.toCombatSelection().then());
  }

  private onCreated(combatId: string) {
    if (this.hasRequestedStartOfCombat) {
      return;
    }

    this.startedCombatId = combatId;

    const currentPlayer = this.identityService.getIdentity();
    const otherPlayer =
      this.combat?.leftPlayerName === currentPlayer ? this.combat?.rightPlayerName : this.combat?.leftPlayerName;
    this.alertsService.info(`Combat has been started by ${otherPlayer}. Press START to join them.`);
  }

  protected readonly CombatSide = CombatSide;
}

declare module '@angular/router' {
  interface Router {
    toCombatConfiguration(id: string): Promise<boolean>;
  }
}

Router.prototype.toCombatConfiguration = function (id: string): Promise<boolean> {
  return this.navigate(['/', 'combat', 'configure', id]);
};
