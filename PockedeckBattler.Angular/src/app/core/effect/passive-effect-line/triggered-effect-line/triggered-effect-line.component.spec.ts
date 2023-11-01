import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TriggeredEffectLineComponent } from './triggered-effect-line.component';

describe('TriggeredPassiveEffectLineComponent', () => {
  let component: TriggeredEffectLineComponent;
  let fixture: ComponentFixture<TriggeredEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TriggeredEffectLineComponent],
    });
    fixture = TestBed.createComponent(TriggeredEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
