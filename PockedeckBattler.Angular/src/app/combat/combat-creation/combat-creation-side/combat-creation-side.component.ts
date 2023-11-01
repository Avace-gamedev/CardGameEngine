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
  public placeFrontCharacterOnLeftSide: boolean = false;

  protected character1: CharacterView | undefined;
  protected character2: CharacterView | undefined;

  swap() {
    if (this.character1 && this.character2) {
      const tmp = this.character2;
      this.character2 = this.character1;
      this.character1 = tmp;
    }
  }

  select(character: CharacterView) {
    if (this.isSelected(character)) {
      return;
    }

    if (!this.character1 || (this.character1 && this.character2)) {
      this.character1 = character;
    } else if (!this.character2) {
      this.character2 = character;
    }
  }

  isSelected(character: CharacterView) {
    return (
      (this.character1 &&
        character.identity.name === this.character1.identity.name) ||
      (this.character2 &&
        character.identity.name === this.character2.identity.name)
    );
  }
}
