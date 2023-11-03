import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { ActionPointsComponent } from './action-points/action-points.component';
import { TurnsIconComponent } from './turns-icon/turns-icon.component';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    ElementIconComponent,
    ActionPointsComponent,
    TurnsIconComponent,
  ],
  imports: [CommonModule, NgbTooltip],
  exports: [ElementIconComponent, ActionPointsComponent, TurnsIconComponent],
})
export class IconsModule {}
