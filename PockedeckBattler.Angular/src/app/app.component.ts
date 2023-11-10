import { Component } from '@angular/core';
import { IdentityService } from './core/authentication/services/identity.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  protected topbarMenus = [
    {
      name: 'Home',
      routerLink: ['/', 'home'],
      url: '/home',
    },
    {
      name: 'Combats',
      routerLink: ['/', 'combat'],
      url: '/combat',
    },
  ];

  constructor(protected identityService: IdentityService) {}

  protected logout() {
    this.identityService.clearIdentity();
    location.reload();
  }
}
