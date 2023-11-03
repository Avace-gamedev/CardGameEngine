import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CharacterView, CombatInPreparationView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-configure-side',
  templateUrl: './combat-configure-side.component.html',
})
export class CombatConfigureSideComponent {
  @Input()
  get name(): string | undefined {
    return this._name;
  }
  set name(value: string | undefined) {
    this._name = value;
    this.doUpdateAfterChange();
  }
  private _name: string | undefined;

  @Input()
  get combat(): CombatInPreparationView | undefined {
    return this._combat;
  }
  set combat(value: CombatInPreparationView | undefined) {
    this._combat = value;
    this.doUpdateAfterChange();
  }
  private _combat: CombatInPreparationView | undefined;

  @Input()
  get characters(): CharacterView[] {
    return this._characters;
  }
  set characters(value: CharacterView[]) {
    this._characters = value;
    this.doUpdateAfterChange();
  }
  private _characters: CharacterView[] = [];

  @Input()
  public enableNameEdition: boolean = false;

  @Input()
  public readonly: boolean = false;

  @Input()
  public invertSlotPositions: boolean = false;

  @Output()
  public update: EventEmitter<CombatSideConfiguration> = new EventEmitter<CombatSideConfiguration>();

  @Output()
  public leave: EventEmitter<void> = new EventEmitter<void>();

  protected frontCharacter: CharacterView | undefined;
  protected backCharacter: CharacterView | undefined;
  protected ready: boolean = false;

  protected get disabled(): boolean {
    return this.readonly || this.ready;
  }

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
      this.sendUpdate();
    }
  }

  select(character: CharacterView, slot?: 'front' | 'back') {
    if (this.isSelected(character)) {
      if (slot && this.getAt(slot)?.identity?.name !== character.identity.name) {
        this.swap();
      }

      return;
    }

    switch (slot) {
      case 'front':
        this.frontCharacter = character;
        this.sendUpdate();
        break;
      case 'back':
        this.backCharacter = character;
        this.sendUpdate();
        break;
      default:
        if (!this.frontCharacter || (this.frontCharacter && this.backCharacter)) {
          this.frontCharacter = character;
        } else if (!this.backCharacter) {
          this.backCharacter = character;
        }
        this.sendUpdate();
    }
  }

  isSelected(character: CharacterView) {
    return (
      (this.frontCharacter && character.identity.name === this.frontCharacter.identity.name) ||
      (this.backCharacter && character.identity.name === this.backCharacter.identity.name)
    );
  }

  unselect(slot: 'front' | 'back') {
    switch (slot) {
      case 'front':
        this.frontCharacter = undefined;
        this.sendUpdate();
        break;
      case 'back':
        this.backCharacter = undefined;
        this.sendUpdate();
        break;
    }
  }

  protected dragCharacter($event: DragEvent, character: CharacterView) {
    if (!$event.dataTransfer) {
      return;
    }

    $event.dataTransfer.setData('text/plain', `character:${character.identity.name}`);
    $event.dataTransfer.effectAllowed = 'copy';
  }

  protected dropCharacter($event: DragEvent, slot: 'front' | 'back') {
    const data = $event.dataTransfer?.getData('text/plain');
    if (!data || !data.startsWith('character:')) {
      return;
    }

    const characterName = data.substring(10);
    const character = this._characters.find((c) => c.identity.name === characterName);

    if (!character) {
      return;
    }

    this.select(character, slot);

    $event.preventDefault();
  }

  protected setReady(ready: boolean) {
    this.ready = ready;
    this.sendUpdate();
  }

  private sendUpdate() {
    this.update.emit({
      playerName: this._name ?? '',
      frontCharacter: this.frontCharacter?.identity.name,
      backCharacter: this.backCharacter?.identity.name,
      ready: this.ready,
    });
  }

  private doUpdateAfterChange() {
    if (!this._combat || !this._name) {
      this.frontCharacter = undefined;
      this.backCharacter = undefined;
      this.ready = false;
      return;
    }

    if (this._name == this._combat.leftPlayerName) {
      this.frontCharacter = this._characters.find((c) => c.identity.name === this._combat?.leftFrontCharacter);
      this.backCharacter = this._characters.find((c) => c.identity.name === this._combat?.leftBackCharacter);
      this.ready = this._combat.leftReady;
    } else if (this._name == this._combat.rightPlayerName) {
      this.frontCharacter = this._characters.find((c) => c.identity.name === this._combat?.rightFrontCharacter);
      this.backCharacter = this._characters.find((c) => c.identity.name === this._combat?.rightBackCharacter);
      this.ready = this._combat.rightReady;
    }
  }
}

export interface CombatSideConfiguration {
  readonly playerName: string;
  readonly frontCharacter?: string;
  readonly backCharacter?: string;
  readonly ready: boolean;
}
