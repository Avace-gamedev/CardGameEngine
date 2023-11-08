import { Component } from '@angular/core';
import { IdentityService } from './core/authentication/services/identity.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  constructor(protected identityService: IdentityService) {}

  protected logout() {
    this.identityService.clearIdentity();
    location.reload();
  }
}