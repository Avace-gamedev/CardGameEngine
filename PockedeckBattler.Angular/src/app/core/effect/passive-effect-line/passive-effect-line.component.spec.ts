import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PassiveEffectLineComponent } from './passive-effect-line.component';

describe('PassiveEffectLineComponent', () => {
  let component: PassiveEffectLineComponent;
  let fixture: ComponentFixture<PassiveEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PassiveEffectLineComponent]
    });
    fixture = TestBed.createComponent(PassiveEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
