import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HealthBarComponent } from './health-bar/health-bar.component';

@NgModule({
  declarations: [HealthBarComponent],
  exports: [HealthBarComponent],
  imports: [CommonModule],
})
export class GenericUiModule {}
