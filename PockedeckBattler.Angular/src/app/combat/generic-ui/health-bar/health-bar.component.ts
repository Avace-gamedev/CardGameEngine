import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-health-bar',
  templateUrl: './health-bar.component.html',
  styleUrls: ['./health-bar.component.css'],
})
export class HealthBarComponent {
  @Input()
  get value(): number {
    return this._value;
  }
  set value(value: number) {
    this._value = value;
    this.update();
  }
  private _value: number = 0;

  @Input()
  get maxValue(): number {
    return this._maxValue;
  }
  set maxValue(value: number) {
    this._maxValue = value;
    this.update();
  }
  private _maxValue: number = 0;

  public get ratio(): number {
    return this._ratio;
  }
  private _ratio: number = 0;

  protected percent: number = 0;

  private update() {
    if (this._maxValue === 0) {
      this._ratio = 0;
    }

    this._ratio = this._value / this._maxValue;
    this.percent = this.ratio * 100;
  }
}
