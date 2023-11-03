import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
})
export class NotFoundComponent {
  protected url: string | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    this.activatedRoute.queryParamMap.subscribe((queryParams) => {
      this.url = queryParams.get('url') ?? this.router.url;
    });
  }
}

declare module '@angular/router' {
  interface Router {
    to404(): Promise<boolean>;
  }
}

Router.prototype.to404 = function (): Promise<boolean> {
  const url = this.url;
  return this.navigate(['/', '404'], { queryParams: { url } });
};
