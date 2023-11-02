import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  catchError,
  from,
  map,
  Observable,
  of,
  switchMap,
  take,
  throwError,
} from 'rxjs';
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

        return this.getReason(err).pipe(
          take(1),
          map((reason) => {
            if (reason) {
              this.alertsService.danger(reason);
            }
          }),
          switchMap((_) => throwError(() => err)),
        );
      }),
    );
  }

  private getReason(err: any): Observable<string | undefined> {
    if (err instanceof HttpErrorResponse && err.error instanceof Blob) {
      return from(err.error.text()).pipe(
        map((err) => {
          const parsed = JSON.parse(err);
          return this.getReasonFromProblemDetails(parsed);
        }),
      );
    }

    return of(undefined);
  }

  private getReasonFromProblemDetails(err: {
    status: number;
    title?: string;
    detail?: string;
  }): string | undefined {
    if (err.detail) {
      if (err.title) {
        return `${err.title}: ${err.detail}`;
      }

      return err.detail;
    }

    if (err.title) {
      return err.title;
    }

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

    return undefined;
  }
}
