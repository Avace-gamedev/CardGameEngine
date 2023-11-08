import { Component, Input } from '@angular/core';
import { Element } from '../../../api/pockedeck-battler-api-client';
import { AssetIcon } from '../asset-icon/asset-icons';
import { ElementIconsUtils } from './element-icons-utils';

@Component({
  selector: 'app-element-icon',
  templateUrl: './element-icon.component.html',
})
export class ElementIconComponent {
  @Input()
  get element(): Element | undefined {
    return this._element;
  }
  set element(value: Element | undefined) {
    this._element = value;
    this.update();
  }
  private _element: Element | undefined;

  @Input()
  public noColor: boolean = false;

  @Input()
  public pxOffsetX: number = 0;

  @Input()
  public pxOffsetY: number = 0;

  protected icon: AssetIcon | undefined;
  protected tooltip: string | undefined;
  protected cssColorClass: string | undefined;

  private update() {
    if (!this._element) {
      this.icon = undefined;
      this.tooltip = undefined;
      return;
    }

    this.icon = ElementIconsUtils.getIcon(this._element);

    switch (this._element) {
      case Element.Neutral:
        this.tooltip = 'Neutral';
        break;
      case Element.Fire:
        this.tooltip = 'Fire';
        break;
      case Element.Earth:
        this.tooltip = 'Earth';
        break;
      case Element.Water:
        this.tooltip = 'Water';
        break;
      case Element.Wind:
        this.tooltip = 'Wind';
        break;
    }

    switch (this._element) {
      case Element.Neutral:
        this.cssColorClass = 'neutral-color';
        break;
      case Element.Fire:
        this.cssColorClass = 'fire-color';
        break;
      case Element.Earth:
        this.cssColorClass = 'earth-color';
        break;
      case Element.Water:
        this.cssColorClass = 'water-color';
        break;
      case Element.Wind:
        this.cssColorClass = 'wind-color';
        break;
    }
  }

  protected readonly Element = Element;
}
