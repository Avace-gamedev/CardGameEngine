import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveEffectLine } from './active-effect-line.component';

describe('EffectLineComponent', () => {
  let component: ActiveEffectLine;
  let fixture: ComponentFixture<ActiveEffectLine>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActiveEffectLine],
    });
    fixture = TestBed.createComponent(ActiveEffectLine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
