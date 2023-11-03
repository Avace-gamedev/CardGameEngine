import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-alert-modal',
  templateUrl: './alert-modal.component.html',
})
export class AlertModalComponent {
  @Input()
  public title: string | undefined;

  @Input()
  public content: string | undefined;

  @Input()
  public closeLabel: string | undefined;

  protected alertString: string = oneOf(['Alert!', 'Wait a minute...', 'Is this for real?!']);

  constructor(protected modal: NgbActiveModal) {}
}

const oneOf = (arr: string[]) => {
  return arr[Math.floor(Math.random() * arr.length)];
};
