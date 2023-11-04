import { Component, Input } from '@angular/core';
import { CardInstanceWithModificationsView, CharacterView } from '../../api/pockedeck-battler-api-client';
import { ActionCardSize } from '../../core/card/action-card/action-card.component';

@Component({
  selector: 'app-action-card-instance',
  templateUrl: './action-card-instance.component.html',
})
export class ActionCardInstanceComponent {
  @Input()
  get card(): CardInstanceWithModificationsView | undefined {
    return this._card;
  }
  set card(value: CardInstanceWithModificationsView | undefined) {
    this._card = value;
    this.update();
  }
  private _card: CardInstanceWithModificationsView | undefined;

  @Input()
  public size: ActionCardSize = 'md';

  @Input()
  get characters(): CharacterView[] {
    return this._characters;
  }
  set characters(value: CharacterView[]) {
    this._characters = value;
    this.update();
  }
  private _characters: CharacterView[] = [];

  protected character: CharacterView | undefined;

  private update() {
    if (!this._card) {
      this.character = undefined;
      return;
    }

    this.character = this._characters.find((c) => c.identity.name === this._card!.character);
  }
}
