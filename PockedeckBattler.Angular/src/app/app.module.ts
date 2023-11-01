import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NotFoundComponent } from './common-pages/not-found/not-found.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [AppComponent, NotFoundComponent],
  imports: [BrowserModule, CoreModule, AppRoutingModule, NgbModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
