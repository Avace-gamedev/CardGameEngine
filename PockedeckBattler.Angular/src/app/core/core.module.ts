import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ApiModule } from '../api/api.module';
import { CardModule } from './card/card.module';
import { CharacterModule } from './character/character.module';
import { EffectModule } from './effect/effect.module';
import { ModalsModule } from './modals/modals.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ApiModule,
    ModalsModule,
    CharacterModule,
    CardModule,
    EffectModule,
  ],
  exports: [ApiModule, ModalsModule, CharacterModule, CardModule, EffectModule],
})
export class CoreModule {}
