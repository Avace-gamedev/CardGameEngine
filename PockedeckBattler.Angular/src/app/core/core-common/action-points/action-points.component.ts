import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-action-points',
  templateUrl: './action-points.component.html',
  styleUrls: ['./action-points.component.css'],
})
export class ActionPointsComponent {
  @Input()
  public value: number | undefined;
}
