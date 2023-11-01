import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DamageEffectLineComponent } from './damage-effect-line.component';

describe('DamageEffectLineComponent', () => {
  let component: DamageEffectLineComponent;
  let fixture: ComponentFixture<DamageEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DamageEffectLineComponent]
    });
    fixture = TestBed.createComponent(DamageEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
