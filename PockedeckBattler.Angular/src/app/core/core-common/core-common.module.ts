import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { ActionPointsComponent } from './action-points/action-points.component';
import { ElementIconComponent } from './element-icon/element-icon.component';
import { SignPipe } from './pipes/sign.pipe';
import { TurnsIconComponent } from './turns-icon/turns-icon.component';

@NgModule({
  declarations: [
    ElementIconComponent,
    ActionPointsComponent,
    SignPipe,
    TurnsIconComponent,
  ],
  imports: [CommonModule, NgbTooltip],
  exports: [
    ElementIconComponent,
    ActionPointsComponent,
    SignPipe,
    TurnsIconComponent,
  ],
})
export class CoreCommonModule {}
