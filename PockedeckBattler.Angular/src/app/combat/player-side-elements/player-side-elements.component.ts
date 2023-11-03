import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  CharactersService,
  CharacterView,
  CombatSide,
  PlayerCombatView,
} from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-player-side-elements',
  templateUrl: './player-side-elements.component.html',
  styleUrls: ['./player-side-elements.component.css'],
})
export class PlayerSideElementsComponent implements OnInit {
  @Input()
  public combat: PlayerCombatView | undefined;

  @Input()
  public disabled: boolean = false;

  @Output()
  public play: EventEmitter<number> = new EventEmitter<number>();

  @Output()
  public endTurn: EventEmitter<void> = new EventEmitter<void>();

  protected characters: CharacterView[] = [];

  constructor(private charactersService: CharactersService) {}

  ngOnInit() {
    this.charactersService
      .getAll()
      .subscribe((characters) => (this.characters = characters));
  }

  protected readonly CombatSide = CombatSide;
}
