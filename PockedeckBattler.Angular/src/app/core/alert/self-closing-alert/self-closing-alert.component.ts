import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { AlertType } from '../alerts.service';

@Component({
  selector: 'app-self-closing-alert',
  templateUrl: './self-closing-alert.component.html',
  styleUrls: ['./self-closing-alert.component.css'],
})
export class SelfClosingAlertComponent implements OnInit {
  @ViewChild('alert', { static: false })
  private staticAlert: NgbAlert | undefined;

  @Input()
  public type: AlertType = AlertType.Info;

  @Input()
  public delay: number = 1;

  @Output()
  public closed: EventEmitter<void> = new EventEmitter<void>();

  ngOnInit() {
    setTimeout(() => this.close(), this.delay);
  }

  private close() {
    this.staticAlert?.close();
  }
}
