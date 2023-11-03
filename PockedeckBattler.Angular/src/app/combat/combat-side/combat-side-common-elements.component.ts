import { Component, Input } from '@angular/core';
import { BaseCombatView, CombatSideView } from '../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-combat-side-common-elements',
  templateUrl: './combat-side-common-elements.component.html',
  styleUrls: ['./combat-side-common-elements.component.css'],
})
export class CombatSideCommonElementsComponent {
  @Input()
  get combat(): BaseCombatView | undefined {
    return this._combat;
  }
  set combat(value: BaseCombatView | undefined) {
    this._combat = value;
    this.update();
  }
  private _combat: BaseCombatView | undefined;

  @Input()
  get side(): CombatSideView | undefined {
    return this._side;
  }
  set side(value: CombatSideView | undefined) {
    this._side = value;
    this.update();
  }
  private _side: CombatSideView | undefined;

  @Input()
  public isAi: boolean = false;

  @Input()
  public highlightSource: string | undefined;

  @Input()
  public highlightEnemies: string[] = [];

  @Input()
  public highlightAllies: string[] = [];

  @Input()
  public invert: boolean = false;

  protected aps: ApState[] = [];

  private update() {
    this.updateAps();
  }

  private updateAps() {
    this.aps = [];

    if (!this._combat || !this._side) {
      return;
    }

    for (let i = 0; i < this._combat.maxAp; i++) {
      this.aps.push(i < this._side.ap ? 'filled' : 'empty');
    }
  }
}

type ApState = 'empty' | 'filled';
