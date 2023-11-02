import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { CombatInPreparationView } from '../../api/pockedeck-battler-api-client';
import { SignalRService } from '../../api/signal-r/signal-r.service';

@UntilDestroy()
@Component({
  selector: 'app-combat-preparation',
  templateUrl: './combat-preparation.component.html',
  styleUrls: ['./combat-preparation.component.css'],
})
export class CombatPreparationComponent implements OnInit {
  constructor(private signalRService: SignalRService) {}

  ngOnInit() {
    this.signalRService
      .listen<CombatInPreparationView>(
        'combats',
        'CombatInPreparationChange',
        CombatInPreparationView.fromJS,
      )
      .pipe(untilDestroyed(this))
      .subscribe((msg) => console.log(msg));
  }
}
