import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertsContainerComponent } from './alerts-container.component';

describe('AlertComponent', () => {
  let component: AlertsContainerComponent;
  let fixture: ComponentFixture<AlertsContainerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AlertsContainerComponent],
    });
    fixture = TestBed.createComponent(AlertsContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
