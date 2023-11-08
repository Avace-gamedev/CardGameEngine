import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignPipe } from './pipes/sign.pipe';

@NgModule({
  declarations: [SignPipe],
  imports: [CommonModule],
  exports: [CommonModule, SignPipe],
})
export class SharedModule {}
