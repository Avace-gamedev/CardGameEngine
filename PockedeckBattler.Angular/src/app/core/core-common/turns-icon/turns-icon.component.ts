import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-turns-icon',
  templateUrl: './turns-icon.component.html',
  styleUrls: ['./turns-icon.component.css'],
})
export class TurnsIconComponent {
  @Input()
  public value: number | undefined;

  @Input()
  public enableTooltip: boolean = false;
}
