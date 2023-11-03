import { APP_BASE_HREF, PlatformLocation } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './common-pages/login/login.component';
import { NotFoundComponent } from './common-pages/not-found/not-found.component';
import { AlertModule } from './core/alert/alert.module';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [AppComponent, NotFoundComponent, LoginComponent],
  imports: [BrowserModule, CoreModule, AppRoutingModule, NgbModule, FormsModule, AlertModule],
  providers: [
    {
      provide: APP_BASE_HREF,
      useFactory: (s: PlatformLocation) => s.getBaseHrefFromDOM(),
      deps: [PlatformLocation],
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
