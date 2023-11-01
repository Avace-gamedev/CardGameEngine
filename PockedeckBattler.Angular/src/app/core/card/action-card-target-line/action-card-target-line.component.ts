import { Component, Input } from '@angular/core';
import { ActionCardTarget } from '../../../api/pockedeck-battler-api-client';

@Component({
  selector: 'app-action-card-target-line',
  templateUrl: './action-card-target-line.component.html',
  styleUrls: ['./action-card-target-line.component.css'],
})
export class ActionCardTargetLineComponent {
  @Input()
  get target(): ActionCardTarget | undefined {
    return this._target;
  }
  set target(value: ActionCardTarget | undefined) {
    this._target = value;
    this.update();
  }
  private _target: ActionCardTarget | undefined;

  protected targetString: string | undefined;

  private update() {
    switch (this.target) {
      case ActionCardTarget.None:
        this.targetString = undefined;
        break;
      case ActionCardTarget.Self:
        this.targetString = 'self';
        break;
      case ActionCardTarget.OtherAlly:
        this.targetString = 'other ally';
        break;
      case ActionCardTarget.FrontOpponent:
        this.targetString = 'first enemy';
        break;
      case ActionCardTarget.BackOpponent:
        this.targetString = 'last enemy';
        break;
      case ActionCardTarget.AllOpponents:
        this.targetString = 'all enemies';
        break;
      case ActionCardTarget.FrontAlly:
        this.targetString = 'first ally';
        break;
      case ActionCardTarget.BackAlly:
        this.targetString = 'last ally';
        break;
      case ActionCardTarget.AllAllies:
        this.targetString = 'all allies';
        break;
      default:
        this.targetString = undefined;
        break;
    }
  }
}
