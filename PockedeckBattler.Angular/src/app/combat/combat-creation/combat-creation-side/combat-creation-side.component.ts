import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-creation-side',
  templateUrl: './combat-creation-side.component.html',
  styleUrls: ['./combat-creation-side.component.css'],
})
export class CombatCreationSideComponent {
  @Input()
  get name(): string {
    return this._name;
  }
  set name(value: string) {
    this._name = value;
    this.initialName = value;
  }
  private _name: string = '';

  protected initialName: string = '';

  @Input()
  public disableNameEdition: boolean = false;

  @Input()
  get characters(): CharacterView[] {
    return this._characters;
  }

  set characters(value: CharacterView[]) {
    this._characters = value;
    this.autoSelect();
  }
  private _characters: CharacterView[] = [];

  @Input()
  public frontCharacter: CharacterView | undefined;

  @Input()
  public backCharacter: CharacterView | undefined;

  @Input()
  public invertSlotPositions: boolean = false;

  @Output()
  public frontCharacterChange: EventEmitter<CharacterView | undefined> =
    new EventEmitter<CharacterView | undefined>();

  @Output()
  public backCharacterChange: EventEmitter<CharacterView | undefined> =
    new EventEmitter<CharacterView | undefined>();

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
      this.setFront(this.backCharacter);
      this.setBack(tmp);
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
        this.setFront(character);
        break;
      case 'back':
        this.setBack(character);
        break;
      default:
        if (
          !this.frontCharacter ||
          (this.frontCharacter && this.backCharacter)
        ) {
          this.setFront(character);
        } else if (!this.backCharacter) {
          this.setBack(character);
        }
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
        this.setFront(undefined);
        break;
      case 'back':
        this.setBack(undefined);
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
    const character = this._characters.find(
      (c) => c.identity.name === characterName,
    );

    if (!character) {
      return;
    }

    this.select(character, slot);

    $event.preventDefault();
  }

  private autoSelect() {
    if (
      (!this.frontCharacter || !this.backCharacter) &&
      this._characters.length > 0
    ) {
      this.select(this._characters[0]);
    }

    if (
      (!this.frontCharacter || !this.backCharacter) &&
      this._characters.length > 1
    ) {
      this.select(this._characters[1]);
    }
  }

  private setFront(character: CharacterView | undefined) {
    this.frontCharacter = character;
    this.frontCharacterChange.emit(character);
  }

  private setBack(character: CharacterView | undefined) {
    this.backCharacter = character;
    this.backCharacterChange.emit(character);
  }
}
