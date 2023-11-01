import { Router } from '@angular/router';

declare module '@angular/router' {
  interface Router {
    to404(): Promise<boolean>;
  }
}

Router.prototype.to404 = function (): Promise<boolean> {
  const url = this.url;
  return this.navigate(['/', '404'], { queryParams: { url } });
};
