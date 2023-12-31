import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  CardInstanceWithModificationsView,
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
  public hover: EventEmitter<CardInstanceWithModificationsView | undefined> = new EventEmitter<
    CardInstanceWithModificationsView | undefined
  >();

  protected characters: CharacterView[] = [];
  protected hoveredCharacter: string | undefined;

  constructor(private charactersService: CharactersService) {}

  ngOnInit() {
    this.charactersService.getAll().subscribe((characters) => (this.characters = characters));
  }

  protected sendPlay(index: number) {
    if (this.disabled) {
      return;
    }

    this.play.emit(index);
  }

  protected sendEndTurn() {
    if (this.disabled) {
      return;
    }

    this.endTurn.emit();
  }

  protected mouseEnter(card: CardInstanceWithModificationsView) {
    this.hoveredCharacter = card.character;
    this.hover.emit(card);
  }

  protected mouseLeave(card: CardInstanceWithModificationsView) {
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
