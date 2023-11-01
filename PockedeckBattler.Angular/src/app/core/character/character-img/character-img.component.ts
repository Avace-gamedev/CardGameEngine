import { Component, EventEmitter, Input, Output } from '@angular/core';
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
  public cardPopover: boolean = false;

  @Output()
  public dropped: EventEmitter<DragEvent> = new EventEmitter<DragEvent>();

  @Output()
  public pressed: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  protected characterBeingDragged: boolean = false;

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
