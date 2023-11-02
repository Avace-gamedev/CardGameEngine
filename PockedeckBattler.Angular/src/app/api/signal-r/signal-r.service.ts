import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection } from '@microsoft/signalr';
import { filter, map, Observable, Subject } from 'rxjs';
import { IdentityService } from '../../core/authentication/services/identity.service';
import { API_BASE_URL } from '../pockedeck-battler-api-client';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private connections: { [hub: string]: signalR.HubConnection } = {};
  private methods: { [hub: string]: { [method: string]: Subject<any[]> } } = {};

  constructor(
    @Inject(API_BASE_URL) private apiBaseUrl: string,
    private identityService: IdentityService,
  ) {}

  public listenRaw(hub: string, method: string): Observable<any[]> {
    if (!this.methods[hub]) {
      this.methods[hub] = {};
    }

    if (!this.methods[hub][method]) {
      this.methods[hub][method] = new Subject<any[]>();

      const connection = this.connect(hub);
      connection.on(method, (...args: any[]) =>
        this.methods[hub][method].next(args),
      );
    }

    return this.methods[hub][method];
  }

  public listen<T>(hub: string, method: string, parser: (arg: any) => T) {
    return this.listenRaw(hub, method).pipe(
      filter((...args: any[]) => args && args.length > 0),
      map((...args: any[]) => parser(args[0])),
    );
  }

  private connect(hub: string): HubConnection {
    if (this.connections[hub]) {
      return this.connections['hub'];
    }

    const url = new URL('signalr/' + hub, this.apiBaseUrl);
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(url.href, { withCredentials: false })
      .withAutomaticReconnect()
      .build();

    connection
      .start()
      .then(() =>
        connection.send('GetIdentity', this.identityService.getIdentity()),
      );

    this.connections[hub] = connection;
    return connection;
  }
}
