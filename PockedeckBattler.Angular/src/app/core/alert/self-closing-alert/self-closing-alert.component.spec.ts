import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelfClosingAlertComponent } from './self-closing-alert.component';

describe('SelfClosingAlertComponent', () => {
  let component: SelfClosingAlertComponent;
  let fixture: ComponentFixture<SelfClosingAlertComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelfClosingAlertComponent]
    });
    fixture = TestBed.createComponent(SelfClosingAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
