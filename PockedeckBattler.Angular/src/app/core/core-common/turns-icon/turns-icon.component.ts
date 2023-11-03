import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-turns-icon',
  templateUrl: './turns-icon.component.html',
})
export class TurnsIconComponent {
  @Input()
  public value: number | undefined;

  @Input()
  public enableTooltip: boolean = false;
}
