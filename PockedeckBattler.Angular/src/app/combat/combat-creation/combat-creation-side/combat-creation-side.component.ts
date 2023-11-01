import { Component, Input } from '@angular/core';
import { finalize, switchMap } from 'rxjs';
import {
  CharacterView,
  CombatInPreparationView,
  CombatsService,
  UpdateCombatInPreparationRequest,
} from '../../../api/pockedeck-battler-api-client';
import { IdentityService } from '../../../core/authentication/services/identity.service';

@Component({
  selector: 'app-combat-creation-side',
  templateUrl: './combat-creation-side.component.html',
  styleUrls: ['./combat-creation-side.component.css'],
})
export class CombatCreationSideComponent {
  @Input()
  public name: string | undefined;

  @Input()
  get combat(): CombatInPreparationView | undefined {
    return this._combat;
  }
  set combat(value: CombatInPreparationView | undefined) {
    this._combat = value;
    this.update(value);
  }
  private _combat: CombatInPreparationView | undefined;

  @Input()
  public readonly: boolean = false;

  @Input()
  public characters: CharacterView[] = [];

  @Input()
  public invertSlotPositions: boolean = false;

  protected refreshing: boolean = false;
  protected frontCharacter: CharacterView | undefined;
  protected backCharacter: CharacterView | undefined;
  protected ready: boolean = false;

  protected get disabled(): boolean {
    return this.readonly || this.ready;
  }

  constructor(
    private identityService: IdentityService,
    private combatsService: CombatsService,
  ) {}

  getAt(slot: 'front' | 'back'): CharacterView | undefined {
    switch (slot) {
      case 'front':
        return this.frontCharacter;
      case 'back':
        return this.backCharacter;
    }
  }

  swap() {
    if (this.frontCharacter && this.backCharacter) {
      const tmp = this.frontCharacter;
      this.frontCharacter = this.backCharacter;
      this.backCharacter = tmp;
      this.sendUpdateRequestAndRefresh();
    }
  }

  select(character: CharacterView, slot?: 'front' | 'back') {
    if (this.isSelected(character)) {
      if (
        slot &&
        this.getAt(slot)?.identity?.name !== character.identity.name
      ) {
        this.swap();
      }

      return;
    }

    switch (slot) {
      case 'front':
        this.frontCharacter = character;
        this.sendUpdateRequestAndRefresh();
        break;
      case 'back':
        this.backCharacter = character;
        this.sendUpdateRequestAndRefresh();
        break;
      default:
        if (
          !this.frontCharacter ||
          (this.frontCharacter && this.backCharacter)
        ) {
          this.frontCharacter = character;
        } else if (!this.backCharacter) {
          this.backCharacter = character;
        }
        this.sendUpdateRequestAndRefresh();
    }
  }

  isSelected(character: CharacterView) {
    return (
      (this.frontCharacter &&
        character.identity.name === this.frontCharacter.identity.name) ||
      (this.backCharacter &&
        character.identity.name === this.backCharacter.identity.name)
    );
  }

  unselect(slot: 'front' | 'back') {
    switch (slot) {
      case 'front':
        this.frontCharacter = undefined;
        this.sendUpdateRequestAndRefresh();
        break;
      case 'back':
        this.backCharacter = undefined;
        this.sendUpdateRequestAndRefresh();
        break;
    }
  }

  protected dragCharacter($event: DragEvent, character: CharacterView) {
    if (!$event.dataTransfer) {
      return;
    }

    $event.dataTransfer.setData(
      'text/plain',
      `character:${character.identity.name}`,
    );
    $event.dataTransfer.effectAllowed = 'copy';
  }

  protected dropCharacter($event: DragEvent, slot: 'front' | 'back') {
    const data = $event.dataTransfer?.getData('text/plain');
    if (!data || !data.startsWith('character:')) {
      return;
    }

    const characterName = data.substring(10);
    const character = this.characters.find(
      (c) => c.identity.name === characterName,
    );

    if (!character) {
      return;
    }

    this.select(character, slot);

    $event.preventDefault();
  }

  protected setReady(ready: boolean) {
    this.ready = ready;
    this.sendUpdateRequestAndRefresh();
  }

  private sendUpdateRequestAndRefresh() {
    if (this.combat) {
      this.refreshing = true;
      this.combatsService
        .updateCombatInPreparation(
          this.combat.id,
          new UpdateCombatInPreparationRequest({
            playerName: this.name ?? '',
            frontCharacter: this.frontCharacter?.identity.name,
            backCharacter: this.frontCharacter?.identity.name,
            ready: this.ready,
          }),
        )
        .pipe(
          switchMap(() =>
            this.combatsService.getCombatInPreparation(
              this.combat?.id ?? '',
              this.identityService.getIdentity(),
            ),
          ),
          finalize(() => (this.refreshing = false)),
        )
        .subscribe();
    }
  }

  private update(combat: CombatInPreparationView | undefined) {
    if (!combat) {
      return;
    }

    if (this.name == combat.leftPlayerName) {
      this.frontCharacter = this.characters.find(
        (c) => c.identity.name === combat.leftFrontCharacter,
      );
      this.backCharacter = this.characters.find(
        (c) => c.identity.name === combat.leftBackCharacter,
      );
      this.ready = combat.leftReady;
    } else if (this.name == combat.rightPlayerName) {
      this.frontCharacter = this.characters.find(
        (c) => c.identity.name === combat.rightFrontCharacter,
      );
      this.backCharacter = this.characters.find(
        (c) => c.identity.name === combat.rightBackCharacter,
      );
      this.ready = combat.rightReady;
    }
  }
}
