import { Component, OnInit } from '@angular/core';
import { finalize, forkJoin, ignoreElements, map, Observable } from 'rxjs';
import {
  CombatInPreparationView,
  CombatsService,
  PlayerCombatView,
} from '../../api/pockedeck-battler-api-client';
import { IdentityService } from '../../core/authentication/services/identity.service';

@Component({
  templateUrl: './combat-selection.component.html',
  styleUrls: ['./combat-selection.component.css'],
})
export class CombatSelectionComponent implements OnInit {
  protected combats: PlayerCombatView[] = [];
  protected combatsInPreparation: CombatInPreparationView[] = [];
  protected refreshing: boolean = false;

  constructor(
    private combatController: CombatsService,
    private identityService: IdentityService,
  ) {}

  ngOnInit() {
    this.loadCombats().subscribe();
  }

  public refresh() {
    this.loadCombats().subscribe();
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
