import { Component, Input } from '@angular/core';
import { AssetIconSize } from '../../../../core/icons/asset-icon/asset-icon.component';
import { AssetIcon } from '../../../../core/icons/asset-icon/asset-icons';
import { CharacterInCombatView } from '../../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-effect',
  templateUrl: './combat-effect.component.html',
})
export class CombatEffectComponent {
  @Input()
  public icon: AssetIcon | undefined;

  @Input()
  get name(): string {
    return this._name;
  }

  set name(value: string) {
    this._name = value;
    this.tooltip = this.getTooltip(this._name, this._duration);
  }

  private _name: string = '';

  @Input()
  get duration(): number | undefined {
    return this._duration;
  }

  set duration(value: number | undefined) {
    this._duration = value;
    this.tooltip = this.getTooltip(this._name, this._duration);
  }

  private _duration: number | undefined = 0;

  @Input()
  get size(): CombatEffectSize {
    return this._size;
  }

  set size(value: CombatEffectSize) {
    this._size = value;
    this.sizeInPx = this.getSize(value);
  }

  @Input()
  public source: CharacterInCombatView | undefined;

  private _size: CombatEffectSize = 'md';

  protected sizeInPx: number = this.getSize(this._size);
  protected iconSize: AssetIconSize = this.getIconSize(this._size);
  protected tooltip: string | undefined;
  protected popoverVisible: boolean = false;

  private getSize(size: CombatEffectSize) {
    switch (size) {
      case 'sm':
        return 16;
      case 'md':
        return 32;
      case 'lg':
        return 64;
      case 'xl':
        return 64;
    }
  }

  private getIconSize(size: CombatEffectSize) {
    switch (size) {
      case 'sm':
        return 'xs';
      case 'md':
        return 'sm';
      case 'lg':
        return 'md';
      case 'xl':
        return 'lg';
    }
  }

  private getTooltip(name: string, duration: number | undefined) {
    if (duration) {
      return `${name} (${duration} turns)`;
    } else {
      return name;
    }
  }
}

export type CombatEffectSize = 'sm' | 'md' | 'lg' | 'xl';
