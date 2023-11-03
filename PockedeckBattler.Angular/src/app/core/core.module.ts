import { NgModule } from '@angular/core';
import { ApiModule } from '../api/api.module';
import { CardModule } from './card/card.module';
import { CharacterModule } from './character/character.module';
import { EffectModule } from './effect/effect.module';
import { ModalsModule } from './modals/modals.module';
import { IconsModule } from './icons/icons.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [SharedModule, ApiModule, ModalsModule, CharacterModule, CardModule, EffectModule],
  exports: [ApiModule, SharedModule, ModalsModule, IconsModule, CharacterModule, CardModule, EffectModule],
})
export class CoreModule {}
