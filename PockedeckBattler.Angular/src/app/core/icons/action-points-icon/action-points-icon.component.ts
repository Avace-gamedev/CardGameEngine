import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-action-points-icon',
  templateUrl: './action-points-icon.component.html',
})
export class ActionPointsIconComponent {
  @Input()
  public size: ActionPointsSize = 'md';
}

export type ActionPointsSize = 'sm' | 'md' | 'lg';
