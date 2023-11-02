import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { from, ignoreElements, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ModalsService {
  constructor(private modalService: NgbModal) {}

  public alert(content: string, title?: string): Observable<void> {
    return from(this.modalService.open(content, {}).result).pipe(
      ignoreElements(),
    );
  }
}
