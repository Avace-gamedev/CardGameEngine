import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignPipe } from './pipes/sign.pipe';
import { StickyPopoverDirective } from './directives/sticky-popover.directive';

@NgModule({
  declarations: [SignPipe, StickyPopoverDirective],
  imports: [CommonModule],
  exports: [CommonModule, SignPipe, StickyPopoverDirective],
})
export class SharedModule {}
