import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatsPassiveEffectLineComponent } from './stats-passive-effect-line.component';

describe('StatsPassiveEffectLineComponent', () => {
  let component: StatsPassiveEffectLineComponent;
  let fixture: ComponentFixture<StatsPassiveEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StatsPassiveEffectLineComponent]
    });
    fixture = TestBed.createComponent(StatsPassiveEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
