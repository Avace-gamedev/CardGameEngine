import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  CardInstanceWithModifiersView,
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
  get disabled(): boolean {
    return this._disabled;
  }
  set disabled(value: boolean) {
    this._disabled = value;
    this.clearMouseHover();
  }
  private _disabled: boolean = false;

  @Output()
  public play: EventEmitter<number> = new EventEmitter<number>();

  @Output()
  public endTurn: EventEmitter<void> = new EventEmitter<void>();

  @Output()
  public hover: EventEmitter<CardInstanceWithModifiersView | undefined> =
    new EventEmitter<CardInstanceWithModifiersView | undefined>();

  protected characters: CharacterView[] = [];
  protected hoveredCharacter: string | undefined;

  constructor(private charactersService: CharactersService) {}

  ngOnInit() {
    this.charactersService
      .getAll()
      .subscribe((characters) => (this.characters = characters));
  }

  protected mouseEnter(card: CardInstanceWithModifiersView) {
    this.hoveredCharacter = card.character;
    this.hover.emit(card);
  }

  protected mouseLeave(card: CardInstanceWithModifiersView) {
    if (this.hoveredCharacter === card.character) {
      this.clearMouseHover();
    }
  }

  private clearMouseHover() {
    this.hoveredCharacter = undefined;
    this.hover.emit(undefined);
  }

  protected readonly CombatSide = CombatSide;
}
