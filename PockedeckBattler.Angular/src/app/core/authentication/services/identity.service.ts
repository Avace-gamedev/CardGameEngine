import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private readonly localStorageKey = 'identity';

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
  }

  private getIdentityOrUndefined(): string | undefined {
    return localStorage.getItem(this.localStorageKey) ?? undefined;
  }
}
