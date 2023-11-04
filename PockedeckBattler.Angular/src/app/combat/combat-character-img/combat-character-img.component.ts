import { Component, Input } from '@angular/core';
import { Placement } from '@ng-bootstrap/ng-bootstrap';
import { CharacterCombatView } from '../../api/pockedeck-battler-api-client';
import { CombatCharacterImageSize } from '../../core/character/character-img/character-img.component';
import { Color, getCssColor } from '../../shared/utils/colors';

@Component({
  selector: 'app-combat-character-img',
  templateUrl: './combat-character-img.component.html',
})
export class CombatCharacterImgComponent {
  @Input()
  public character: CharacterCombatView | undefined;

  @Input()
  public size: CombatCharacterImageSize = 'md';

  @Input()
  public popoverPlacement: Placement = 'auto';

  @Input()
  public footer: string | undefined;

  @Input()
  public invert: boolean = false;

  @Input()
  get innerShadow(): CombatCharacterShadow | undefined {
    return this._innerShadow;
  }
  set innerShadow(value: CombatCharacterShadow | undefined) {
    this._innerShadow = value;
    this.innerShadowCssValue = this.getShadowCssValue(value, 'inset ');
  }
  private _innerShadow: CombatCharacterShadow | undefined;

  @Input()
  get outerShadow(): CombatCharacterShadow | undefined {
    return this._outerShadow;
  }
  set outerShadow(value: CombatCharacterShadow | undefined) {
    this._outerShadow = value;
    this.outerShadowCssValue = this.getShadowCssValue(value);
  }
  private _outerShadow: CombatCharacterShadow | undefined;

  protected innerShadowCssValue: string | undefined = this.getShadowCssValue(this.innerShadow, 'inset ');
  protected outerShadowCssValue: string | undefined = this.getShadowCssValue(this.outerShadow);

  private getShadowCssValue(shadow: CombatCharacterShadow | undefined, prefix?: string) {
    if (!shadow) {
      return undefined;
    }

    const color = getCssColor(shadow.color);
    return (prefix ?? '') + `0 0 ${shadow.blurInRem}rem ${color}`;
  }
}

export type CombatCharacterShadow = {
  blurInRem: number;
  color: Color;
};
