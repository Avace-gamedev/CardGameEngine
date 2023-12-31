import { Injectable } from '@angular/core';
import { CombatSide, CombatSideView, PlayerCombatView } from '../api/pockedeck-battler-api-client';

@Injectable()
export class CurrentCombatService {
  private combat: PlayerCombatView | undefined;

  public set(combat: PlayerCombatView) {
    this.combat = combat;
  }

  get() {
    return this.combat;
  }

  public getCharacter(name: string, side: CombatSide) {
    if (!this.combat || side === CombatSide.None) {
      return undefined;
    }

    return this.combat.player.side === side
      ? this.getCharacterOfSide(this.combat.player, name)
      : this.getCharacterOfSide(this.combat.opponent, name);
  }

  getPlayerName(side: CombatSide) {
    if (!this.combat || side === CombatSide.None) {
      return undefined;
    }

    return this.combat.player.side === side ? this.combat.player.playerName : this.combat.opponent.playerName;
  }

  private getCharacterOfSide(side: CombatSideView, name: string) {
    return side.frontCharacter?.character.identity.name === name
      ? side.frontCharacter
      : side.backCharacter?.character.identity.name === name
      ? side.backCharacter
      : side.deadCharacters.find((c) => c.character.identity.name === name);
  }
}
