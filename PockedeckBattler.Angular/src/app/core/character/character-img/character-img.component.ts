import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Placement } from '@ng-bootstrap/ng-bootstrap';
import { CharacterView } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-img',
  templateUrl: './character-img.component.html',
  styleUrls: ['./character-img.component.css'],
})
export class CharacterImgComponent {
  @Input()
  public character: CharacterView | undefined;

  @Input()
  public size: CombatCharacterImageSize = 'md';

  @Input()
  public enablePopoverDetails: boolean = false;

  @Input()
  public popoverPlacement: Placement = 'auto';

  @Output()
  public dropped: EventEmitter<DragEvent> = new EventEmitter<DragEvent>();

  @Output()
  public pressed: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  protected characterBeingDragged: boolean = false;

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
}

export type CombatCharacterImageSize = 'sm' | 'md' | 'lg' | 'xl';