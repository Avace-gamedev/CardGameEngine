import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Placement } from '@ng-bootstrap/ng-bootstrap';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-img',
  templateUrl: './character-img.component.html',
})
export class CharacterImgComponent {
  @Input()
  get character(): CharacterView | undefined {
    return this._character;
  }
  set character(value: CharacterView | undefined) {
    this._character = value;
    this.update();
  }
  private _character: CharacterView | undefined;

  @Input()
  public size: CombatCharacterImageSize = 'md';

  @Input()
  public enablePopoverDetails: boolean = false;

  @Input()
  public popoverPlacement: Placement = 'auto';

  @Input()
  public footer: string | undefined;

  @Output()
  public dropped: EventEmitter<DragEvent> = new EventEmitter<DragEvent>();

  @Output()
  public pressed: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  protected characterBeingDragged: boolean = false;
  protected imgPath: string | undefined;
  protected bgMode: 'light' | 'dark' = 'dark';

  protected get imgSize(): number {
    switch (this.size) {
      case 'sm':
        return 30;
      case 'md':
        return 50;
      case 'lg':
        return 100;
      case 'xl':
        return 150;
    }
  }
  protected get fontSizeClass(): string {
    switch (this.size) {
      case 'sm':
        return 'fs-6';
      case 'md':
        return 'fs-4';
      case 'lg':
        return 'fs-1';
      case 'xl':
        return 'fs-1';
    }
  }

  protected dragenter($event: DragEvent) {
    if ($event.dataTransfer) {
      this.characterBeingDragged =
        $event.dataTransfer.types.includes('text/plain');
    }
  }

  protected dragleave() {
    this.characterBeingDragged = false;
  }

  protected ondrop($event: DragEvent) {
    this.characterBeingDragged = false;
    this.dropped.emit($event);
  }

  private update() {
    this.updateImgPath();
    this.updateBgMode();
  }

  private updateImgPath() {
    if (!this._character) {
      this.imgPath = undefined;
      return;
    }

    this.imgPath = '/assets/imgs/' + this._character.identity.name + '-256.png';
  }

  private updateBgMode() {
    if (!this._character) {
      return;
    }

    switch (this._character.identity.name) {
      case 'bulbasaur':
      case 'squirtle':
        this.bgMode = 'dark';
        break;
      case 'charmander':
        this.bgMode = 'light';
        break;
    }
  }
}

export type CombatCharacterImageSize = 'sm' | 'md' | 'lg' | 'xl';
