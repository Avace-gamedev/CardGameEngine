import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { AssetIcon } from './asset-icons';

@Component({
  selector: 'app-asset-icon',
  templateUrl: './asset-icon.component.html',
  styles: [':host { display: inline-flex; align-items: baseline; justify-content: center }'],
})
export class AssetIconComponent implements AfterViewInit {
  @ViewChild('container', { static: true })
  get container(): ElementRef<HTMLDivElement> | undefined {
    return this._container;
  }

  set container(value: ElementRef<HTMLDivElement> | undefined) {
    this._container = value;
    this.update();
  }

  private _container: ElementRef<HTMLDivElement> | undefined;

  @Input()
  get icon(): AssetIcon | undefined {
    return this._icon;
  }

  set icon(value: AssetIcon | undefined) {
    this._icon = value;
    this.url = this.getUrl(value);
  }

  private _icon: AssetIcon | undefined;

  @Input()
  get size(): AssetIconSize {
    return this._size;
  }

  set size(value: AssetIconSize) {
    this._size = value;
    this.sizeInPx = this.getSizeInPx(value);
  }

  private _size: AssetIconSize = 'md';

  @Input()
  get color(): AssetIconColor | undefined {
    return this._color;
  }

  set color(value: AssetIconColor | undefined) {
    this._color = value;
    this.cssColor = this.getCssColor(value);
  }

  private _color: AssetIconColor | undefined;

  @Input()
  get pxOffsetX(): number {
    return this._pxOffsetX;
  }

  set pxOffsetX(value: number) {
    this._pxOffsetX = value;
    this.cssTransform = this.getCssTransform(value, this._pxOffsetY);
  }

  private _pxOffsetX: number = 0;

  @Input()
  get pxOffsetY(): number {
    return this._pxOffsetY;
  }

  set pxOffsetY(value: number) {
    this._pxOffsetY = value;
    this.cssTransform = this.getCssTransform(this._pxOffsetX, value);
  }

  private _pxOffsetY: number = 0;

  protected url: string | undefined;
  protected sizeInPx: number = this.getSizeInPx('md');
  protected cssColor: string | undefined = this.getCssColor(this.color);
  protected cssTransform: string | undefined;

  constructor(private changeDetectorRef: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.update();
    this.changeDetectorRef.detectChanges();
  }

  private update() {
    this.url = this.getUrl(this._icon);
    this.sizeInPx = this.getSizeInPx(this._size);
    this.cssColor = this.getCssColor(this._color);
    this.cssTransform = this.getCssTransform(this._pxOffsetX, this._pxOffsetY);
  }

  private getUrl(value: AssetIcon | undefined) {
    if (!value) {
      return undefined;
    }

    return `assets/icons/${value}.svg`;
  }

  private getSizeInPx(value: AssetIconSize) {
    switch (value) {
      case 'sm':
        return 16;
      case 'md':
        return 32;
      case 'lg':
        return 64;
    }
  }

  private getCssColor(color: AssetIconColor | undefined): string | undefined {
    if (!color) {
      return this.getColorProperty();
    }

    if (isCssVarColor(color)) {
      return `var(${color[1]})`;
    } else {
      return color;
    }
  }

  private getCssTransform(x: number, y: number) {
    if (x === 0 && y === 0) {
      return undefined;
    }

    return `translate(${x}px, ${y}px)`;
  }

  private getColorProperty(): string | undefined {
    if (!this._container) {
      return undefined;
    }

    const style = window.getComputedStyle(this._container.nativeElement);
    return style.color;
  }
}

export type AssetIconSize = 'sm' | 'md' | 'lg';

type KnownColor = 'white' | 'black';
type CssVarColor = `--${string}`;
type HexColor = `#${string}`;
type RgbColor = `rgb(${number}, ${number}, ${number})`;
type RgbaColor = `rgba(${number}, ${number}, ${number}, ${number})`;

export type AssetIconColor = KnownColor | CssVarColor | HexColor | RgbColor | RgbaColor;

const isCssVarColor = (color: AssetIconColor): color is CssVarColor => color.startsWith('--');
const isHexColor = (color: AssetIconColor): color is HexColor => color[0] === '#';
const isRgbColor = (color: AssetIconColor): color is RgbColor => color.startsWith('rgb');
const isRgbaColor = (color: AssetIconColor): color is RgbaColor => color.startsWith('rgba');
const isKnownColor = (color: AssetIconColor): color is KnownColor => !isCssVarColor(color);
