import { Component, OnInit } from '@angular/core';
import { IdentityService } from './core/authentication/services/identity.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  protected playerName: string | undefined;

  constructor(private identityService: IdentityService) {}

  ngOnInit() {
    this.playerName = this.identityService.isAuthenticated()
      ? this.identityService.getIdentity()
      : undefined;
  }
}
