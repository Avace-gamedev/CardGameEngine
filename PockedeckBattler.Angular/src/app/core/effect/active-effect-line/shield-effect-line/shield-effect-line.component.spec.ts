import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealEffectLineComponent } from './heal-effect-line.component';

describe('HealEffectLineComponent', () => {
  let component: HealEffectLineComponent;
  let fixture: ComponentFixture<HealEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HealEffectLineComponent]
    });
    fixture = TestBed.createComponent(HealEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
