import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlertsService {
  private counter: number = 1;

  get alerts(): readonly Alert[] {
    return this._alerts;
  }
  private _alerts: Alert[] = [];

  get alert$(): Observable<Alert> {
    return this.alertSubject;
  }
  private alertSubject: Subject<Alert> = new Subject<Alert>();

  public success(message: string, details?: string) {
    this.alert(AlertType.Success, message, details);
  }

  public info(message: string, details?: string) {
    this.alert(AlertType.Info, message, details);
  }

  public warning(message: string, details?: string) {
    this.alert(AlertType.Warning, message, details);
  }

  public danger(message: string, details?: string) {
    this.alert(AlertType.Danger, message, details);
  }

  public dismiss(alert: Alert) {
    this._alerts = this.alerts.filter((a) => a.id != alert.id);
  }

  private alert(type: AlertType, message: string, details?: string) {
    const alert = {
      id: this.counter++,
      type,
      prefix: this.getPrefix(type),
      message,
      details,
    };
    this._alerts.push(alert);

    this.alertSubject.next(alert);
  }

  private getPrefix(type: AlertType) {
    switch (type) {
      case AlertType.Success:
        return this.oneOf('Awesome!', 'Great!', 'So good!');
      case AlertType.Info:
        return this.oneOf('Hey there!', 'FYI');
      case AlertType.Warning:
        return this.oneOf('Watch out', 'Warning!');
      case AlertType.Danger:
        return this.oneOf('Bro!!!', 'Whoa!');
    }
  }

  private oneOf<T>(...choices: T[]): T {
    return choices[Math.floor(Math.random() * choices.length)];
  }
}

export enum AlertType {
  Success = 'success',
  Info = 'info',
  Warning = 'warning',
  Danger = 'danger',
}

export interface Alert {
  readonly id: number;
  readonly type: AlertType;
  readonly prefix?: string;
  readonly message: string;
  readonly details?: string;
}
