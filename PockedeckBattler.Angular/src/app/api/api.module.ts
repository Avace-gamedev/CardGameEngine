import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { API_BASE_URL, CombatsService } from './pockedeck-battler-api-client';

@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  providers: [
    CombatsService,
    { provide: API_BASE_URL, useValue: 'http://localhost:5295' },
  ],
})
export class ApiModule {}
