import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-health-bar',
  templateUrl: './health-bar.component.html',
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

  @Input()
  get shield(): number {
    return this._shield;
  }
  set shield(value: number) {
    this._shield = value;
    this.update();
  }
  private _shield: number = 0;

  protected healthPercent: number = 0;
  protected shieldPercent: number = 0;

  private update() {
    if (this._maxValue === 0) {
      this.healthPercent = 0;
      this.shieldPercent = 0;
    }

    const healthRatio = this._value / this._maxValue;
    this.healthPercent = Math.max(0, Math.min(healthRatio, 1)) * 100;

    const shieldRatio = this._shield / this._maxValue;
    this.shieldPercent = Math.max(0, Math.min(shieldRatio, 1)) * 100;
  }
}
