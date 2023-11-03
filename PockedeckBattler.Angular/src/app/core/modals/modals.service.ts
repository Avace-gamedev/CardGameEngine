import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, from, Observable, of } from 'rxjs';
import { AlertModalComponent } from './alert-modal/alert-modal.component';

@Injectable({
  providedIn: 'root',
})
export class ModalsService {
  constructor(private modalService: NgbModal) {}

  public alert(config: AlertModalConfig): Observable<void> {
    const modalRef = this.modalService.open(AlertModalComponent, {
      centered: true,
    });

    modalRef.componentInstance.title = config.title;
    modalRef.componentInstance.content = config.content;
    modalRef.componentInstance.closeLabel = config.closeLabel;

    return from(modalRef.result).pipe(
      catchError((_) => {
        // dismissed
        return of(void 0);
      })
    );
  }
}

export interface AlertModalConfig {
  readonly title?: string;
  readonly content: string;
  readonly closeLabel?: string;
}
