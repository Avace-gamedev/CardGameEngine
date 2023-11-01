import { Component, Input } from '@angular/core';
import { Element } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-element-icon',
  templateUrl: './element-icon.component.html',
  styleUrls: ['./element-icon.component.css'],
})
export class ElementIconComponent {
  @Input()
  public element: Element | undefined;
  protected readonly Element = Element;
}
