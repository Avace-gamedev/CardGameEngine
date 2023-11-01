import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ActionPointsComponent } from './action-points/action-points.component';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { SignPipe } from './pipes/sign.pipe';

@NgModule({
  declarations: [ElementIconComponent, ActionPointsComponent, SignPipe],
  imports: [CommonModule],
  exports: [ElementIconComponent, ActionPointsComponent, SignPipe],
})
export class CoreCommonModule {}
