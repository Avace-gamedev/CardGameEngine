import { Component, OnInit } from '@angular/core';
import {
  CharactersService,
  CharacterView,
} from '../../api/pockedeck-battler-api-client';

@Component({
  templateUrl: './combat-creation.component.html',
  styleUrls: ['./combat-creation.component.css'],
})
export class CombatCreationComponent implements OnInit {
  protected characters: CharacterView[] = [];

  constructor(private charactersService: CharactersService) {}

  ngOnInit() {
    this.charactersService
      .getAll()
      .subscribe((characters) => (this.characters = characters));
  }
}
