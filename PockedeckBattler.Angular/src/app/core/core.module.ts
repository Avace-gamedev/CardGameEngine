import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ApiModule } from '../api/api.module';
import { CardModule } from './card/card.module';
import { CharacterModule } from './character/character.module';
import { EffectModule } from './effect/effect.module';

@NgModule({
  declarations: [],
  imports: [CommonModule, ApiModule, CharacterModule, CardModule, EffectModule],
  exports: [ApiModule, CharacterModule, CardModule, EffectModule],
})
export class CoreModule {}
