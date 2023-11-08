import { CommonModule, NgOptimizedImage } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbPopover } from '@ng-bootstrap/ng-bootstrap';
import { CardModule } from '../card/card.module';
import { CharacterCardComponent } from './character-card/character-card.component';
import { CharacterImgComponent } from './character-img/character-img.component';
import { SharedModule } from '../../shared/shared.module';
import { StickyPopoverDirective } from '../../shared/directives/sticky-popover.directive';

@NgModule({
  declarations: [CharacterImgComponent, CharacterCardComponent],
  imports: [CommonModule, NgbPopover, CardModule, NgOptimizedImage, SharedModule, StickyPopoverDirective],
  exports: [CharacterImgComponent, CharacterCardComponent],
})
export class CharacterModule {}
