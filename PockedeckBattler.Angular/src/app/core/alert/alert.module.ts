import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { AlertsContainerComponent } from './alerts-container.component';
import { SelfClosingAlertComponent } from './self-closing-alert/self-closing-alert.component';

@NgModule({
  declarations: [AlertsContainerComponent, SelfClosingAlertComponent],
  imports: [CommonModule, NgbAlert],
  exports: [AlertsContainerComponent],
})
export class AlertModule {}
