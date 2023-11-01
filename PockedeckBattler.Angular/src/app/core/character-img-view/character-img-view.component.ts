import { Component, Input } from '@angular/core';
import { CharacterView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-character-img-view',
  templateUrl: './character-img-view.component.html',
  styleUrls: ['./character-img-view.component.css'],
})
export class CharacterImgViewComponent {
  @Input()
  public character: CharacterView | undefined;
}
