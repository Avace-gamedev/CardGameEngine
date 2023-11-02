import { Component } from '@angular/core';
import { Alert, AlertsService } from './alerts.service';

@Component({
  selector: 'app-alerts-container',
  templateUrl: './alerts-container.component.html',
  styleUrls: ['./alerts-container.component.css'],
})
export class AlertsContainerComponent {
  constructor(protected alertsService: AlertsService) {}

  protected alertTrackByFn(index: number, alert: Alert) {
    return alert.id;
  }
}
