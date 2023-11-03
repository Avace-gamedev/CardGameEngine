import { CommonModule, NgOptimizedImage } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbPopover } from '@ng-bootstrap/ng-bootstrap';
import { CardModule } from '../card/card.module';
import { CharacterCardComponent } from './character-card/character-card.component';
import { CharacterImgComponent } from './character-img/character-img.component';

@NgModule({
  declarations: [CharacterImgComponent, CharacterCardComponent],
  imports: [CommonModule, NgbPopover, CardModule, NgOptimizedImage],
  exports: [CharacterImgComponent, CharacterCardComponent],
})
export class CharacterModule {}
