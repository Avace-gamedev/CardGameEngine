import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, filter, from, map, of, switchMap } from 'rxjs';
import {
  CardInstanceWithModificationsView,
  CombatSide,
  CombatsService,
  ICharacterInCombatView,
  PlayerCombatView,
} from '../api/pockedeck-battler-api-client';
import { SignalRService } from '../api/signal-r/signal-r.service';
import { IdentityService } from '../core/authentication/services/identity.service';
import { ActionCardTargetUtils } from './utils/utils';
import { CurrentCombatService } from './current-combat.service';

@Component({
  selector: 'app-combat',
  templateUrl: './combat.component.html',
  providers: [CurrentCombatService],
})
export class CombatComponent implements OnInit {
  protected combat: PlayerCombatView | undefined;
  protected source: ICharacterInCombatView | undefined;
  protected allyTargets: string[] = [];
  protected enemyTargets: string[] = [];

  constructor(
    private identityService: IdentityService,
    private signalrService: SignalRService,
    private combatsService: CombatsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private currentCombatService: CurrentCombatService
  ) {}

  ngOnInit() {
    const identity = this.identityService.getIdentity();

    this.signalrService
      .listen<PlayerCombatView>('combats', 'CombatUpdated', PlayerCombatView.fromJS)
      .subscribe((combat) => this.setCombat(combat));

    this.signalrService
      .listen<PlayerCombatView>('combats', 'CombatEnded', PlayerCombatView.fromJS)
      .subscribe((combat) => this.setCombat(combat));

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
        map((combat) => this.setCombat(combat))
      )
      .subscribe();
  }

  play(index: number) {
    if (!this.combat || this.combat.currentSide !== this.combat.player.side) {
      return;
    }

    const card = this.combat.player.hand[index];
    if (!card || card.card.apCost > this.combat.player.ap) {
      return;
    }

    this.combatsService.playCard(this.combat.id, index, this.identityService.getIdentity()).subscribe();
  }

  endTurn() {
    if (!this.combat || this.combat.currentSide !== this.combat.player.side) {
      return;
    }

    this.combatsService.endTurn(this.combat.id, this.identityService.getIdentity()).subscribe();
  }

  hoverCard(card: CardInstanceWithModificationsView | undefined) {
    this.source = undefined;
    this.allyTargets = [];
    this.enemyTargets = [];

    if (!card || !this.combat) {
      return;
    }

    this.source = { name: card.character, side: this.combat.player.side };
    const allyTargets = ActionCardTargetUtils.getAllyTargets(
      card.card.target,
      card.character === this.combat.player.frontCharacter?.character.identity.name ? 'front' : 'back'
    );

    switch (allyTargets) {
      case 'none':
        break;
      case 'front':
        if (this.combat.player.frontCharacter) {
          this.allyTargets.push(this.combat.player.frontCharacter.character.identity.name);
        }

        break;
      case 'back':
        if (this.combat.player.backCharacter) {
          this.allyTargets.push(this.combat.player.backCharacter.character.identity.name);
        }
        break;
      case 'both':
        if (this.combat.player.frontCharacter) {
          this.allyTargets.push(this.combat.player.frontCharacter.character.identity.name);
        }

        if (this.combat.player.backCharacter) {
          this.allyTargets.push(this.combat.player.backCharacter.character.identity.name);
        }
        break;
    }

    const enemyTargets = ActionCardTargetUtils.getEnemyTargets(card.card.target);
    switch (enemyTargets) {
      case 'none':
        break;
      case 'front':
        if (this.combat.opponent.frontCharacter) {
          this.enemyTargets.push(this.combat.opponent.frontCharacter.character.identity.name);
        }
        break;
      case 'back':
        if (this.combat.opponent.backCharacter) {
          this.enemyTargets.push(this.combat.opponent.backCharacter.character.identity.name);
        }
        break;
      case 'both':
        if (this.combat.opponent.frontCharacter) {
          this.enemyTargets.push(this.combat.opponent.frontCharacter.character.identity.name);
        }

        if (this.combat.opponent.backCharacter) {
          this.enemyTargets.push(this.combat.opponent.backCharacter.character.identity.name);
        }
        break;
    }
  }

  hoverCharacter(character: ICharacterInCombatView | undefined) {
    this.source = character;
  }

  private setCombat(combat: PlayerCombatView) {
    this.combat = combat;
    this.currentCombatService.set(combat);
  }

  protected readonly CombatSide = CombatSide;
}

declare module '@angular/router' {
  interface Router {
    toCombat(id: string): Promise<boolean>;
  }
}

Router.prototype.toCombat = function (id: string): Promise<boolean> {
  return this.navigate(['/', 'combat', id]);
};
