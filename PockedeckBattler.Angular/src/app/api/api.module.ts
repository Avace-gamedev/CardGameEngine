import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ExceptionInterceptor } from './exception-interceptor.service';
import { API_BASE_URL, CombatsService } from './pockedeck-battler-api-client';
import { environment } from '../../environments/environment';

@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  providers: [
    CombatsService,
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    { provide: HTTP_INTERCEPTORS, useClass: ExceptionInterceptor, multi: true },
  ],
})
export class ApiModule {}
