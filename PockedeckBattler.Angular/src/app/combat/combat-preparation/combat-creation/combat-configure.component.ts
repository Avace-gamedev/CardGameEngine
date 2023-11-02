import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, from, map, switchMap, throwError } from 'rxjs';
import {
  CharactersService,
  CharacterView,
  CombatInPreparationView,
  CombatSide,
  CombatsService,
} from '../../../api/pockedeck-battler-api-client';
import '../../../common-pages/not-found/redirect';
import { IdentityService } from '../../../core/authentication/services/identity.service';

@Component({
  templateUrl: './combat-configure.component.html',
  styleUrls: ['./combat-configure.component.css'],
})
export class CombatConfigureComponent implements OnInit {
  protected characters: CharacterView[] = [];
  protected combat: CombatInPreparationView | undefined;
  protected side: CombatSide | undefined;

  constructor(
    private identityService: IdentityService,
    private combatsService: CombatsService,
    private charactersService: CharactersService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    const identity = this.identityService.getIdentity();
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
        switchMap(() =>
          this.charactersService
            .getAll()
            .pipe(map((characters) => (this.characters = characters))),
        ),
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

    this.combatsService
      .leaveCombatInPreparation(this.combat.id, name)
      .subscribe(() => this.router.toCombatSelection());
  }

  protected readonly CombatSide = CombatSide;
}

declare module '@angular/router' {
  interface Router {
    toCombatConfiguration(id: string): Promise<boolean>;
  }
}

Router.prototype.toCombatConfiguration = function (
  id: string,
): Promise<boolean> {
  return this.navigate(['/', 'combat', 'preparation', 'configure', id]);
};
