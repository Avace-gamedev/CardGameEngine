import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HealthBarComponent } from './health-bar/health-bar.component';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [HealthBarComponent],
  exports: [HealthBarComponent],
  imports: [CommonModule, NgbTooltip],
})
export class GenericUiModule {}
