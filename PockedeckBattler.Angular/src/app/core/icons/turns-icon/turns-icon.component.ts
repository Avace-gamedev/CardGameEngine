import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-turns-icon',
  templateUrl: './turns-icon.component.html',
})
export class TurnsIconComponent {
  @Input()
  get value(): number | undefined {
    return this._value;
  }
  set value(value: number | undefined) {
    this._value = value;
    this.tooltipString = this._value + ' turns';
  }
  private _value: number | undefined;

  @Input()
  get size(): TurnIconSize {
    return this._size;
  }
  set size(value: TurnIconSize) {
    this._size = value;
    this.sizeInPx = this.getSize(value);
    this.textSizeInRem = this.getTextSizeInRem(value);
  }
  private _size: TurnIconSize = 'md';

  @Input()
  public enableTooltip: boolean = false;

  protected sizeInPx: number = this.getSize(this._size);
  protected textSizeInRem: number = this.getTextSizeInRem(this._size);
  protected tooltipString: string | undefined;

  private getSize(size: TurnIconSize) {
    switch (size) {
      case 'sm':
        return 16;
      case 'md':
        return 32;
      case 'lg':
        return 64;
    }
  }

  private getTextSizeInRem(size: TurnIconSize) {
    switch (size) {
      case 'sm':
        return 0.5;
      case 'md':
        return 1;
      case 'lg':
        return 2;
    }
  }
}

export type TurnIconSize = 'sm' | 'md' | 'lg';
