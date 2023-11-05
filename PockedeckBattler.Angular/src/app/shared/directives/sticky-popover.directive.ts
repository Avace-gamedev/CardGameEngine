import {
  ApplicationRef,
  ChangeDetectorRef,
  Directive,
  ElementRef,
  Inject,
  Injector,
  Input,
  NgZone,
  OnDestroy,
  OnInit,
  Renderer2,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';

import { DOCUMENT } from '@angular/common';
import { NgbPopover, NgbPopoverConfig } from '@ng-bootstrap/ng-bootstrap';

@Directive({
  selector: '[stickyPopover]',
  exportAs: 'stickyPopover',
})
export class StickyPopoverDirective extends NgbPopover implements OnInit, OnDestroy {
  @Input() stickyPopover: TemplateRef<any> | any;

  private canClosePopover: boolean = false;
  private uniqueClass: string;

  constructor(
    private _elRef: ElementRef,
    private _render: Renderer2,
    injector: Injector,
    viewContainerRef: ViewContainerRef,
    config: NgbPopoverConfig,
    ngZone: NgZone,
    _changeDetectorRef: ChangeDetectorRef,
    _appRef: ApplicationRef,
    @Inject(DOCUMENT) _document: any
  ) {
    super(_elRef, _render, injector, viewContainerRef, config, ngZone, _document, _changeDetectorRef, _appRef);
    this.triggers = 'manual';
    this.container = 'body';
    this.placement = 'auto';

    this.uniqueClass = 'popover-' + (Math.random() + 1).toString(36).substring(7);
    this.popoverClass = this.uniqueClass;
  }

  override ngOnInit(): void {
    super.ngOnInit();

    this.ngbPopover = this.stickyPopover;

    this._render.listen(this._elRef.nativeElement, 'mouseenter', () => {
      this.canClosePopover = true;
      this.open();
    });

    this._render.listen(this._elRef.nativeElement, 'mouseleave', (event: Event) => {
      setTimeout(() => {
        if (this.canClosePopover) {
          this.close();
        }
      }, 100);
    });

    this._render.listen(this._elRef.nativeElement, 'click', () => {
      this.close();
    });
  }

  override open() {
    super.open();

    setTimeout(() => {
      const popover = window.document.querySelector(`.${this.uniqueClass}`);
      this._render.listen(popover, 'mouseover', () => {
        this.canClosePopover = false;
      });

      this._render.listen(popover, 'mouseout', () => {
        this.canClosePopover = true;
        setTimeout(() => {
          if (this.canClosePopover) {
            this.close();
          }
        }, 0);
      });
    }, 0);
  }
}
