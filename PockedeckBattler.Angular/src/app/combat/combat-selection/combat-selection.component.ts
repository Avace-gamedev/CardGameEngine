import { Component, OnInit } from '@angular/core';
import { finalize, map, Observable } from 'rxjs';
import {
  CombatsService,
  CombatView,
} from '../../api/pockedeck-battler-api-client';

@Component({
  templateUrl: './combat-selection.component.html',
  styleUrls: ['./combat-selection.component.css'],
})
export class CombatSelectionComponent implements OnInit {
  protected combatIds: string[] = [];
  protected refreshing: boolean = false;

  constructor(private combatController: CombatsService) {}

  ngOnInit() {
    this.loadCombats().subscribe();
  }

  public refresh() {
    this.loadCombats().subscribe();
  }

  private loadCombats(): Observable<void> {
    this.refreshing = true;
    return this.combatController.getAll().pipe(
      map((combatIds) => {
        this.combatIds = combatIds;
      }),
      finalize(() => (this.refreshing = false)),
    );
  }
}
