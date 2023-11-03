import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private readonly localStorageKey = 'identity';

  private identitySubject: BehaviorSubject<string | undefined>;
  public get identity$() {
    return this.identitySubject;
  }
  public get authenticated$() {
    return this.identitySubject.pipe(map(Boolean));
  }

  constructor() {
    const currentIdentity = this.getIdentityOrUndefined();
    this.identitySubject = new BehaviorSubject<string | undefined>(currentIdentity);
  }

  public isAuthenticated(): boolean {
    return !!this.getIdentityOrUndefined();
  }

  public getIdentity(): string {
    const identity = this.getIdentityOrUndefined();
    if (identity) {
      return identity;
    }

    throw new Error('User not authenticated');
  }

  public setIdentity(identity: string) {
    localStorage.setItem(this.localStorageKey, identity);
    this.identitySubject.next(identity);
  }

  public clearIdentity() {
    localStorage.removeItem(this.localStorageKey);
    this.identitySubject.next(undefined);
  }

  private getIdentityOrUndefined(): string | undefined {
    return localStorage.getItem(this.localStorageKey) ?? undefined;
  }
}
