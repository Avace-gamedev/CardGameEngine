import { Component } from '@angular/core';

@Component({
  selector: 'app-combat-empty-side',
  templateUrl: './combat-empty-side.component.html',
  styleUrls: ['./combat-empty-side.component.css'],
})
export class CombatEmptySideComponent {
  protected get url(): string {
    return window.location.href;
  }

  protected copyUrl() {
    navigator.clipboard.writeText(this.url).then();
  }
}
