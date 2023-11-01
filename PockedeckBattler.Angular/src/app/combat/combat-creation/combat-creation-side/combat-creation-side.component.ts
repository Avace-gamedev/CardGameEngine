import { Component, Input, OnInit } from '@angular/core';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-creation-side',
  templateUrl: './combat-creation-side.component.html',
  styleUrls: ['./combat-creation-side.component.css'],
})
export class CombatCreationSideComponent {
  @Input()
  public name: string = '';

  @Input()
  public disableNameEdition: boolean = false;

  @Input()
  public characters: CharacterView[] = [];

  @Input()
  public placeFrontCharacterOnLeftSide: boolean = false;

  protected character1: CharacterView | undefined;
  protected character2: CharacterView | undefined;
}
