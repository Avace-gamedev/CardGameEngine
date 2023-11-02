import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TurnsIconComponent } from './turns-icon.component';

describe('TurnsIconComponent', () => {
  let component: TurnsIconComponent;
  let fixture: ComponentFixture<TurnsIconComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TurnsIconComponent]
    });
    fixture = TestBed.createComponent(TurnsIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
