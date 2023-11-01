import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ApiModule } from '../api/api.module';
import { CharacterImgViewComponent } from './character-img-view/character-img-view.component';

@NgModule({
  declarations: [CharacterImgViewComponent],
  imports: [CommonModule, ApiModule],
  exports: [ApiModule, CharacterImgViewComponent],
})
export class CoreModule {}
