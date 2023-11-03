import { APP_BASE_HREF } from '@angular/common';
import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { CombatInPreparationView } from '../../../api/pockedeck-battler-api-client';
import '../../../core/extensions/string-extensions';

@Component({
  selector: 'app-combat-empty-side',
  templateUrl: './combat-empty-side.component.html',
})
export class CombatEmptySideComponent {
  @Input()
  get combat(): CombatInPreparationView | undefined {
    return this._combat;
  }
  set combat(value: CombatInPreparationView | undefined) {
    this._combat = value;
    this.updateUrl();
  }
  private _combat: CombatInPreparationView | undefined;

  @Output()
  public requestAi: EventEmitter<void> = new EventEmitter<void>();

  protected url: string = '???';

  constructor(@Inject(APP_BASE_HREF) private baseHref: string) {}

  protected copyUrl() {
    navigator.clipboard.writeText(this.url).then();
  }

  private updateUrl() {
    this.url = new URL(
      'combat/preparation/join/' + this._combat?.id ?? '???',
      new URL(this.baseHref, window.location.origin)
    ).href;
  }
}
