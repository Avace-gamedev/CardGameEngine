import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ApiModule } from '../api/api.module';
import { CharacterModule } from './character/character.module';

@NgModule({
  declarations: [],
  imports: [CommonModule, ApiModule, CharacterModule],
  exports: [ApiModule, CharacterModule],
})
export class CoreModule {}
