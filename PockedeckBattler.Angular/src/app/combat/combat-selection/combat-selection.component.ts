import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize, forkJoin, ignoreElements, map, Observable } from 'rxjs';
import {
  CombatInPreparationView,
  CombatsService,
  PlayerCombatView,
} from '../../api/pockedeck-battler-api-client';
import { IdentityService } from '../../core/authentication/services/identity.service';

@Component({
  templateUrl: './combat-selection.component.html',
})
export class CombatSelectionComponent implements OnInit {
  protected combats: PlayerCombatView[] = [];
  protected combatsInPreparation: CombatInPreparationView[] = [];
  protected refreshing: boolean = false;

  constructor(
    private combatController: CombatsService,
    private identityService: IdentityService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.loadCombats().subscribe();
  }

  public refresh() {
    this.loadCombats().subscribe();
  }

  protected create() {
    const identity = this.identityService.getIdentity();

    this.combatController
      .createCombat(identity)
      .pipe(
        map((newCombat) => {
          this.router.toCombatConfiguration(newCombat.id).then();
        }),
      )
      .subscribe();
  }

  protected select(combat: CombatInPreparationView | PlayerCombatView) {
    if (combat instanceof CombatInPreparationView) {
      this.router.toCombatConfiguration(combat.id).then();
    } else {
      this.router.toCombat(combat.id).then();
    }
  }

  private loadCombats(): Observable<void> {
    const player = this.identityService.getIdentity();

    this.refreshing = true;
    return forkJoin([
      this.combatController.getCombatsOfPlayer(player).pipe(
        map((combats) => {
          this.combats = combats;
        }),
      ),
      this.combatController.getCombatsInPreparationOfPlayer(player).pipe(
        map((combats) => {
          this.combatsInPreparation = combats;
        }),
      ),
    ]).pipe(
      ignoreElements(),
      finalize(() => (this.refreshing = false)),
    );
  }
}

declare module '@angular/router' {
  interface Router {
    toCombatSelection(): Promise<boolean>;
  }
}

Router.prototype.toCombatSelection = function (): Promise<boolean> {
  return this.navigate(['/', 'combat', 'selection']);
};
