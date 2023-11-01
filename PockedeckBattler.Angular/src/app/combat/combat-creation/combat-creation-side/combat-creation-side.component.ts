import { Component, Input } from '@angular/core';
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
  public characters: CharacterView[] = [];

  @Input()
  public invertSlotPositions: boolean = false;

  protected frontCharacter: CharacterView | undefined;
  protected backCharacter: CharacterView | undefined;

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
      const tmp = this.backCharacter;
      this.backCharacter = this.frontCharacter;
      this.frontCharacter = tmp;
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
        break;
      case 'back':
        this.backCharacter = character;
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
        break;
      case 'back':
        this.backCharacter = undefined;
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
}
