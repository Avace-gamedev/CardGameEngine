import { Component, Input } from '@angular/core';
import { ActionCardView } from '../../../api/pockedeck-battler-api-client';
import { PlacementArray } from '@ng-bootstrap/ng-bootstrap/util/positioning';

@Component({
  selector: 'app-with-card-popover',
  templateUrl: './with-card-popover.component.html',
})
export class WithCardPopoverComponent {
  @Input()
  public card: ActionCardView | undefined;

  @Input()
  public placement: PlacementArray = 'auto';

  @Input()
  public disabled: boolean = false;
}
