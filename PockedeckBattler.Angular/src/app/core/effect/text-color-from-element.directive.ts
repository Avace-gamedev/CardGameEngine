import { Directive, HostBinding, Input } from '@angular/core';
import { Element } from '../../api/pockedeck-battler-api-client';

@Directive({
  selector: '[textColorFromElement]',
})
export class TextColorFromElementDirective {
  @Input('textColorFromElement')
  get element(): Element | undefined {
    return this._element;
  }
  set element(value: Element | undefined) {
    this._element = value;
    this.update();
  }
  private _element: Element | undefined;

  @HostBinding('class')
  private elementClass: string | undefined;

  private update() {
    if (!this._element) {
      this.elementClass = undefined;
      return;
    }

    switch (this._element) {
      case Element.Neutral:
        this.elementClass = 'neutral-color';
        break;
      case Element.Fire:
        this.elementClass = 'fire-color';
        break;
      case Element.Earth:
        this.elementClass = 'earth-color';
        break;
      case Element.Water:
        this.elementClass = 'water-color';
        break;
      case Element.Wind:
        this.elementClass = 'wind-color';
        break;
    }
  }
}
