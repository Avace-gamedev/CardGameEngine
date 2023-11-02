import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { AlertsService } from '../core/alert/alerts.service';

@Injectable()
export class ExceptionInterceptor implements HttpInterceptor {
  constructor(private alertsService: AlertsService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler,
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        console.error('Error in HTTP request', err);
        this.alertsService.danger(
          this.getReason(err),
          'Error while performing HTTP request.',
        );
        return throwError(() => err);
      }),
    );
  }

  private getReason(err: any): string {
    const status = err.status;
    if (status !== undefined) {
      if (status === 0) {
        return 'Server not found';
      } else if (status == 404) {
        return `Not found (${status})`;
      } else {
        const statusStr = status.toString();
        if (statusStr.startsWith('4')) {
          return `Bad request (${status})`;
        } else if (statusStr.startsWith('5')) {
          return `Server error (${status})`;
        }
      }

      return `Unknown error (${status})`;
    }

    return err.statusText ?? err.message;
  }
}
